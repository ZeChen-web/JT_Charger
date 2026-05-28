using System.Linq.Expressions;

namespace Common.Util
{
    public class LamadaUtil<Dto> where Dto : new()
    {
        private List<Expression> m_lstExpression = null;
        private ParameterExpression m_Parameter = null;

        public LamadaUtil()
        {
            m_lstExpression = new List<Expression>();
            m_Parameter = Expression.Parameter(typeof(Dto), "x");
        }

        //构造表达式，存放到m_lstExpression集合里面
        public void GetExpression(string strPropertyName, object strValue, LamadaExpressionType expressType)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            if (expressType == LamadaExpressionType.Contains)
            {
                expRes = Expression.Call(member, typeof(string).GetMethods().Where(x => x.Name == "Contains").FirstOrDefault(), Expression.Constant(strValue));
            }
            else if (expressType == LamadaExpressionType.Equal)
            {
                expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == LamadaExpressionType.LessThan)
            {
                expRes = Expression.LessThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == LamadaExpressionType.LessThanOrEqual)
            {
                expRes = Expression.LessThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == LamadaExpressionType.GreaterThan)
            {
                expRes = Expression.GreaterThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == LamadaExpressionType.GreaterThanOrEqual)
            {
                expRes = Expression.GreaterThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }

            //else if (expressType == LamadaExpressionType.ParaseIn)
            //{
            //    expRes = Expression.GreaterThanOrEqual(member, Expression.Constant(strValue, member.Type));
            //    public List<TruckiHesapEkstre> ConvertToDesiredType(object list) { return ((IEnumerable<dynamic>)list).Select(item => new TruckiHesapEkstre { MutabakatDetayId = item.MutabakatDetayId, MutabakatVar = item.MutabakatVar, }).ToList(); }

            //    foreach (var itemVal in valueArr)
            //    {
            //        Expression value = Expression.Constant(itemVal);
            //        Expression right = Expression.Equal(key, Expression.Convert(value, key.Type));

            //        expression = Expression.Or(expression, right);
            //    }

            //}







            //return expRes;
            m_lstExpression.Add(expRes);
        }


        //针对Or条件的表达式
        public void GetExpression(string strPropertyName, List<object> lstValue)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            foreach (var oValue in lstValue)
            {
                if (expRes == null)
                {
                    expRes = Expression.Equal(member, Expression.Constant(oValue, member.Type));
                }
                else
                {
                    expRes = Expression.Or(expRes, Expression.Equal(member, Expression.Constant(oValue, member.Type)));
                }
            }


            m_lstExpression.Add(expRes);
        }

        //得到Lamada表达式的Expression对象
        public Expression<Func<Dto, bool>> GetLambda()
        {
            Expression whereExpr = null;
            foreach (var expr in this.m_lstExpression)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.And(whereExpr, expr);
            }
            if (whereExpr == null)
                return null;
            return Expression.Lambda<Func<Dto, Boolean>>(whereExpr, m_Parameter);
        }
    }
    //用于区分操作的枚举
    public enum LamadaExpressionType
    {
        Contains,//like
        Equal,//等于
        LessThan,//小于
        LessThanOrEqual,//小于等于
        GreaterThan,//大于
        GreaterThanOrEqual,//大于等于
        ParaseIn
    }
}
