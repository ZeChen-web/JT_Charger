using System.Linq.Expressions;
using HybirdFrameworkCore.Entity;
using SqlSugar;

namespace Repository;

public abstract class BaseRepository<T> where T : class, new()
{
    //private readonly IUnitOfWork _unitOfWork;
    protected BaseRepository(ISqlSugarClient sqlSugar)
    {
        //_unitOfWork = unitOfWork;
        DbBaseClient = sqlSugar;
    }

    public ISqlSugarClient DbBaseClient;

    /// <summary>
    ///     根据主值查询单条数据
    /// </summary>
    /// <param name="pkValue">主键值</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>泛型实体</returns>
    public T QueryById(object pkValue, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .WithNoLockOrNot(blUseNoLock)
            .InSingle(pkValue);
    }

    public ISugarQueryable<T> Queryable(bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>().WithNoLockOrNot(blUseNoLock);
    }

    /// <summary>
    ///     根据主值查询单条数据
    /// </summary>
    /// <param name="objId">Id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>数据实体</returns>
    public async Task<T> QueryByIdAsync(object objId, bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .In(objId)
            .WithNoLockOrNot(blUseNoLock)
            .SingleAsync();
    }

    /// <summary>
    ///     根据主值列表查询单条数据
    /// </summary>
    /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
    /// <returns>数据实体列表</returns>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    public List<T> QueryByIDs(object[] lstIds, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .In(lstIds)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    ///     根据主值列表查询单条数据
    /// </summary>
    /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
    /// <returns>数据实体列表</returns>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    public async Task<List<T>> QueryByIDsAsync(object[] lstIds, bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .In(lstIds)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }

    /// <summary>
    ///     根据主值列表查询单条数据
    /// </summary>
    /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
    /// <returns>数据实体列表</returns>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    public List<T> QueryByIDs(int[] lstIds, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .In(lstIds)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    /// 根据条件查询表单数据(分页)
    /// </summary>
    /// <param name="page"> 页数</param>
    /// <param name="pageSize">每页几条数据</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public IPage<T> QueryIPageByCause(QueryPageModel page, Expression<Func<T, bool>> predicate)
    {
        if (null == predicate)
        {
            return this.QueryIPage(page);
        }
        int totalCount = 0;


        List<T> pageList = DbBaseClient
                .Queryable<T>()
                .Where(predicate)
                .WithNoLockOrNot(false)
                .ToPageList(page.PageNum, page.PageSize, ref totalCount);



        return new IPage<T>(totalCount, page, pageList);
    }

    /// <summary>
    /// 根据条件查询表单数据(分页) 异步
    /// </summary>
    /// <param name="page"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<IPage<T>> QueryIPageByCauseAsync(QueryPageModel page, Expression<Func<T, bool>> predicate)
    {
        if (null == predicate)
        {
            return await this.QueryIPageAsync(page);
        }
        RefAsync<int> totalCount = 0;


        List<T> pageList = await DbBaseClient
                .Queryable<T>()
                .Where(predicate)
                .WithNoLockOrNot(false)
                .ToPageListAsync(page.PageNum, page.PageSize, totalCount);



        return new IPage<T>(totalCount, page, pageList);
    }


    /// <summary>
    /// 查询表单所有数据(分页)
    /// </summary>
    /// <param name="page"> 页数</param>
    /// <param name="pageSize">每页几条数据</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public IPage<T> QueryIPage(QueryPageModel page)
    {
        int totalCount = 0;
        //page.Page = page.Page == 0 ? 1 : page.Page;//默认第一页 10条数据
        //page.PageSize = page.PageSize == 0 ? 10 : page.PageSize;

        List<T> pageList = DbBaseClient
                .Queryable<T>()
                .WithNoLockOrNot(false)
                .ToPageList(page.PageNum, page.PageSize, ref totalCount);

        return new IPage<T>(totalCount, page, pageList);
    }

    /// <summary>
    /// 查询表单所有数据(分页) 异步
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public async Task<IPage<T>> QueryIPageAsync(QueryPageModel page)
    {
        RefAsync<int> totalCount = 0;

        List<T> pageList = await DbBaseClient
            .Queryable<T>()
            .WithNoLockOrNot(false)
            .ToPageListAsync(page.PageNum, page.PageSize, totalCount);

        return new IPage<T>(totalCount, page, pageList);
    }

    /// <summary>
    ///     根据主值列表查询单条数据
    /// </summary>
    /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
    /// <returns>数据实体列表</returns>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    public async Task<List<T>> QueryByIDsAsync(int[] lstIds, bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .In(lstIds)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }

    /// <summary>
    ///     查询表单所有数据(无分页,请慎用)
    /// </summary>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public List<T> Query(bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    ///     查询表单所有数据(无分页,请慎用)
    /// </summary>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<List<T>> QueryAsync(bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }

    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="strWhere">条件</param>
    /// <param name="orderBy">排序字段，如name asc,age desc</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>泛型实体集合</returns>
    public List<T> QueryListByClause(string strWhere, string orderBy = "", bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .OrderByIF(!string.IsNullOrEmpty(orderBy), orderBy)
            .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }


    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="strWhere">条件</param>
    /// <param name="orderBy">排序字段，如name asc,age desc</param>
    /// <returns>泛型实体集合</returns>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    public async Task<List<T>> QueryListByClauseAsync(string strWhere, string orderBy = "",
        bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderByIF(!string.IsNullOrEmpty(orderBy), orderBy)
            .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }
    public async Task<List<TResult>> QueryListByClauseAsync<TResult>(
        bool isWhere, Expression<Func<T, bool>> expression,
        Expression<Func<T, object>> orderBy,
        bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderBy(orderBy, OrderByType.Asc)
            .WhereIF(isWhere, expression)
            .WithNoLockOrNot(blUseNoLock)
            .Select<TResult>()
            .ToListAsync();
    }

    public async Task<List<TResult>> QueryListByClauseAsync<TResult>(
        Expression<Func<T, bool>> expression, Expression<Func<T, TResult>> expression1
        )
    {
        return await DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .ToListAsync(expression1);
    }

    public async Task<List<TResult>> QueryListBySelectClauseAsync<TResult>(Expression<Func<T, TResult>> selectExpression)
    {
        return await DbBaseClient
            .Queryable<T>()
            .Select(selectExpression)
            .ToListAsync();
    }

    public async Task<List<T>> QueryListByOrderClauseAsync(Expression<Func<T, object>> expression)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderBy(expression, OrderByType.Asc)
            .ToListAsync();
    }
    public async Task<List<T>> QueryListByClauseAsync<TReturn1>(
        Expression<Func<T, List<TReturn1>>> include1,
        Expression<Func<T, object>> expression)
    {
        return await DbBaseClient
            .Queryable<T>()
            .Includes(include1)
            .OrderBy(expression, OrderByType.Asc)
            .ToListAsync();
    }
    public async Task<List<T>> QueryListByInludeClauseAsync<TReturn1>(
        Expression<Func<T, TReturn1>> include1, Expression<Func<T, bool>> expression, Expression<Func<T, object>> expression2
        )
    {
        return await DbBaseClient
            .Queryable<T>()
            .Includes(include1)
            .Where(expression)
            .OrderBy(expression2, OrderByType.Asc)
            .ToListAsync();
    }

    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="orderBy">排序字段，如name asc,age desc</param>
    /// <returns>泛型实体集合</returns>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    public List<T> QueryListByClause(Expression<Func<T, bool>> predicate, string orderBy = "",
        bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .OrderByIF(!string.IsNullOrEmpty(orderBy), orderBy)
            .WhereIF(predicate != null, predicate)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="orderBy">排序字段，如name asc,age desc</param>
    /// <returns>泛型实体集合</returns>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    public async Task<List<T>> QueryListByClauseAsync(Expression<Func<T, bool>> predicate, string orderBy = "",
        bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderByIF(!string.IsNullOrEmpty(orderBy), orderBy)
            .WhereIF(predicate != null, predicate)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }
    public async Task<List<T>> QueryListByClauseAsync(
        bool isWhere, Expression<Func<T, bool>> expression,
        bool isWhere1, Expression<Func<T, bool>> expression1,
        bool isWhere2, Expression<Func<T, bool>> expression2,
        Expression<Func<T, object>> expression3,
        bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderBy(expression3, OrderByType.Asc)
            .WhereIF(isWhere, expression)
            .WhereIF(isWhere1, expression1)
            .WhereIF(isWhere2, expression2)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }
    public async Task<List<T>> QueryListByClauseAsync(
        Expression<Func<T, bool>> expression,
        bool isWhere1, Expression<Func<T, bool>> expression1,
        bool isWhere2, Expression<Func<T, bool>> expression2,
        Expression<Func<T, object>> expression3,
        int pageNumber, int pageSize, RefAsync<int> totalNumber,
        bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderBy(expression3, OrderByType.Asc)
            .Where(expression)
            .WhereIF(isWhere1, expression1)
            .WhereIF(isWhere2, expression2)
            .WithNoLockOrNot(blUseNoLock)
            .ToPageListAsync(pageNumber, pageSize, totalNumber);
    }
    public async Task<List<T>> QueryTreeByClauseAsync(
        Expression<Func<T, object>> expression,
        Expression<Func<T, IEnumerable<object>>> childListExpression, Expression<Func<T, object>> parentIdExpression, object rootValue)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderBy(expression, OrderByType.Asc)
            .ToTreeAsync(childListExpression, parentIdExpression, rootValue);
    }
    public async Task<List<T>> QueryTreeByClauseAsync(
        Expression<Func<T, object>> expression,
        Expression<Func<T, IEnumerable<object>>> childListExpression, Expression<Func<T, object>> parentIdExpression, object rootValue, object[] childIds)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderBy(expression, OrderByType.Asc)
            .ToTreeAsync(childListExpression, parentIdExpression, rootValue, childIds);
    }
    public async Task<List<T>> QueryTreeByClauseAsync(
        Expression<Func<T, object>> parentIdExpression, object primaryKeyValue, bool isContainOneself = true)
    {
        return await DbBaseClient
            .Queryable<T>()
            .ToChildListAsync(parentIdExpression, primaryKeyValue, isContainOneself);
    }
    public async Task<List<T>> QueryTreeByClauseAsync(
        Expression<Func<T, bool>> expression,
        Expression<Func<T, object>> expression1,
        Expression<Func<T, IEnumerable<object>>> childListExpression, Expression<Func<T, object>> parentIdExpression, object rootValue
        )
    {
        return await DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .OrderBy(expression1, OrderByType.Asc)
            .ToTreeAsync(childListExpression, parentIdExpression, rootValue);
    }
    public async Task<List<T>> QueryTreeByClauseAsync(
        Expression<Func<T, bool>> expression,
        Expression<Func<T, object>> expression1,
        Expression<Func<T, IEnumerable<object>>> childListExpression, Expression<Func<T, object>> parentIdExpression, object rootValue, object[] childIds
        )
    {
        return await DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .OrderBy(expression1, OrderByType.Asc)
            .ToTreeAsync(childListExpression, parentIdExpression, rootValue, childIds);
    }


    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="orderByPredicate">排序字段</param>
    /// <param name="orderByType">排序顺序</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>泛型实体集合</returns>
    public List<T> QueryListByClause(Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>> orderByPredicate, OrderByType orderByType, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .OrderByIF(orderByPredicate != null, orderByPredicate, orderByType)
            .WhereIF(predicate != null, predicate)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="orderByPredicate">排序字段</param>
    /// <param name="orderByType">排序顺序</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>泛型实体集合</returns>
    public async Task<List<T>> QueryListByClauseAsync(Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>> orderByPredicate, OrderByType orderByType, bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderByIF(orderByPredicate != null, orderByPredicate, orderByType)
            .WhereIF(predicate != null, predicate)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }
    public async Task<List<TResult>> QueryListByClauseAsync<TResult>(
        Expression<Func<T, bool>> expression,
        bool isWhere, Expression<Func<T, bool>> whereIfExpression,
        Expression<Func<T, TResult>> selectExpression
        )
    {
        return await DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .WhereIF(isWhere, whereIfExpression)
            .Select(selectExpression)
            .ToListAsync();
    }

    /// <summary>
    ///     根据条件查询一定数量数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="take">获取数量</param>
    /// <param name="orderByPredicate">排序字段</param>
    /// <param name="orderByType">排序顺序</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public List<T> QueryListByClause(Expression<Func<T, bool>> predicate, int take,
        Expression<Func<T, object>> orderByPredicate, OrderByType orderByType, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .OrderByIF(orderByPredicate != null, orderByPredicate, orderByType)
            .WhereIF(predicate != null, predicate)
            .Take(take)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    ///     根据条件查询一定数量数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="take">获取数量</param>
    /// <param name="orderByPredicate">排序字段</param>
    /// <param name="orderByType">排序顺序</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<List<T>> QueryListByClauseAsync(Expression<Func<T, bool>> predicate, int take,
        Expression<Func<T, object>> orderByPredicate, OrderByType orderByType, bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderByIF(orderByPredicate != null, orderByPredicate, orderByType)
            .WhereIF(predicate != null, predicate)
            .Take(take)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }

    /// <summary>
    ///     根据条件查询一定数量数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="take">获取数量</param>
    /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public List<T> QueryListByClause(Expression<Func<T, bool>> predicate, int take, string strOrderByFileds = "",
        bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .Where(predicate)
            .Take(take)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    ///     根据条件查询一定数量数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="take">获取数量</param>
    /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<List<T>> QueryListByClauseAsync(Expression<Func<T, bool>> predicate, int take,
        string strOrderByFileds = "", bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .Where(predicate)
            .Take(take)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }

    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public T QueryByClause(Expression<Func<T, bool>> predicate, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .WithNoLockOrNot(blUseNoLock)
            .First(predicate);
    }

    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<T> QueryByClauseAsync(Expression<Func<T, bool>> predicate, bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .WithNoLockOrNot(blUseNoLock)
            .FirstAsync(predicate);
    }
    public async Task<List<TResult>> QueryByGroupByAsync<TResult>(
        Expression<Func<T, object>> expression,
        Expression<Func<T, TResult>> expression2
        )
    {
        return await DbBaseClient
            .Queryable<T>()
            .GroupBy(expression)
            .Select(expression2)
            .ToListAsync();
    }
    public async Task<List<long>> QueryByClauseAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> selectExpression, bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .Where(predicate)
            .Select(selectExpression)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }

    public async Task<int> UpdateColumnsAsync(
        T updateObj,
        Expression<Func<T, object>> columns
        )
    {
        return await DbBaseClient
            .Updateable(updateObj)
            .UpdateColumns(columns)
            .ExecuteCommandAsync();
    }


    public async Task<List<T>> QueryListByClauseAsync(
        Expression<Func<T, bool>> expression)
    {
        return await DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .ToListAsync();
    }

    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="orderByPredicate">排序字段</param>
    /// <param name="orderByType">排序顺序</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public T QueryByClause(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByPredicate,
        OrderByType orderByType, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .OrderBy(orderByPredicate, orderByType)
            .WithNoLockOrNot(blUseNoLock)
            .First(predicate);
    }

    /// <summary>
    ///     根据条件查询数据
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="orderByPredicate">排序字段</param>
    /// <param name="orderByType">排序顺序</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<T> QueryByClauseAsync(Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>> orderByPredicate, OrderByType orderByType, bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .OrderBy(orderByPredicate, orderByType)
            .WithNoLockOrNot(blUseNoLock)
            .FirstAsync(predicate);
    }
    public async Task<List<T>> QueryByOrderByClauseAsync
        (
        Expression<Func<T, bool>> expression,
        Expression<Func<T, object>> expression1,
        bool blUseNoLock = false)
    {
        return await DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .OrderBy(expression1, OrderByType.Asc)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }
    public List<T> QueryByClauseToList(
        Expression<Func<T, bool>> expression, Expression<Func<T, bool>> expression2,
        Expression<Func<T, object>> expression1, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .Where(expression2)
            .OrderBy(expression1, OrderByType.Asc)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }
    public List<T> QueryByClauseToList(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> expression2, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .Where(expression2)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }
    public List<T> QueryByClauseToList(Expression<Func<T, bool>> expression, Expression<Func<T, object>> expression1, bool blUseNoLock = false)
    {
        return DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .OrderBy(expression1, OrderByType.Asc)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }
    public List<T> QueryByClauseToList(Expression<Func<T, bool>> expression)
    {
        return DbBaseClient
            .Queryable<T>()
            .Where(expression)
            .ToList();
    }
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="joinExpression"></param>
    /// <param name="whereExpression"></param>
    /// <param name="orderExpression"></param>
    /// <param name="selectExpression"></param>
    /// <returns></returns>
    public async Task<List<TResult>> QueryByClauseAsync<T, T2, TResult>(
        Expression<Func<T, T2, bool>> joinExpression,
        Expression<Func<T, T2, bool>> whereExpression,
        Expression<Func<T, T2, object>> orderExpression,
        Expression<Func<T, T2, TResult>> selectExpression
        )
    {
        return await DbBaseClient
            .Queryable<T>()
            .LeftJoin<T2>(joinExpression)
            .Where(whereExpression)
            .OrderBy(orderExpression, OrderByType.Asc)
            .Select(selectExpression)
            .ToListAsync();
    }
    public async Task<List<TResult>> QueryByClauseAsync<T, T2, TResult>(
        Expression<Func<T, T2, bool>> joinExpression,
        Expression<Func<T, T2, bool>> whereExpression,
        bool isWhere, Expression<Func<T, T2, bool>> whereifExpression,
        Expression<Func<T, T2, object>> orderExpression,
        Expression<Func<T, T2, TResult>> selectExpression
        )
    {
        return await DbBaseClient
            .Queryable<T>()
            .LeftJoin<T2>(joinExpression)
            .Where(whereExpression)
            .WhereIF(isWhere, whereifExpression)
            .OrderBy(orderExpression, OrderByType.Asc)
            .Select(selectExpression)
            .ToListAsync();
    }

    /// <summary>
    ///     写入实体数据
    /// </summary>
    /// <param name="entity">实体数据</param>
    /// <returns></returns>
    public T Insert(T entity)
    {
        return DbBaseClient
            .Insertable(entity)
            .ExecuteReturnEntity();
    }
    public bool InsertT(T entity)
    {
        var result = DbBaseClient
            .Insertable(entity)
            .ExecuteCommand(); // 返回受影响的行数
    
        return result > 0;
    }
    /// <summary>
    ///     写入或者更新实体数据
    /// </summary>
    /// <param name="entity">实体数据</param>
    /// <returns></returns>
    public int InsertOrUpdate(T entity)
    {
        return DbBaseClient.Storageable(entity).ExecuteCommand();
    }

    /// <summary>
    ///     写入实体数据
    /// </summary>
    /// <param name="entity">实体数据</param>
    /// <returns></returns>
    public async Task<T> InsertAsync(T entity)
    {
        return await DbBaseClient
            .Insertable(entity)
            .ExecuteReturnEntityAsync();
    }
    public async Task<T> InsertReturnEntityAsync(T entity)
    {
        return await DbBaseClient
            .Insertable(entity)
            .ExecuteReturnEntityAsync();
    }
    /// <summary>
    ///     写入实体数据
    /// </summary>
    /// <param name="entity">实体数据</param>
    /// <param name="insertColumns">插入的列</param>
    /// <returns></returns>
    public T Insert(T entity, Expression<Func<T, object>> insertColumns = null)
    {
        var insert = DbBaseClient.Insertable(entity);
        if (insertColumns == null)
            return insert.ExecuteReturnEntity();
        return insert.InsertColumns(insertColumns).ExecuteReturnEntity();
    }

    /// <summary>
    ///     写入实体数据
    /// </summary>
    /// <param name="entity">实体数据</param>
    /// <param name="insertColumns">插入的列</param>
    /// <returns></returns>
    public async Task<T> InsertAsync(T entity, Expression<Func<T, object>> insertColumns = null)
    {
        var insert = DbBaseClient.Insertable(entity);
        if (insertColumns == null)
            return await insert.ExecuteReturnEntityAsync();
        return await insert.InsertColumns(insertColumns).ExecuteReturnEntityAsync();
    }

    /// <summary>
    ///     写入实体数据
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <param name="insertColumns">需插入的字段</param>
    /// <returns></returns>
    public bool InsertGuid(T entity, Expression<Func<T, object>> insertColumns = null)
    {
        var insert = DbBaseClient.Insertable(entity);
        if (insertColumns == null)
            return insert.ExecuteCommand() > 0;
        return insert.InsertColumns(insertColumns).ExecuteCommand() > 0;
    }

    /// <summary>
    ///     写入实体数据
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <param name="insertColumns">需插入的字段</param>
    /// <returns></returns>
    public async Task<bool> InsertGuidAsync(T entity, Expression<Func<T, object>> insertColumns = null)
    {
        var insert = DbBaseClient.Insertable(entity);
        if (insertColumns == null)
            return await insert.ExecuteCommandAsync() > 0;
        return await insert.InsertColumns(insertColumns).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    ///     批量写入实体数据
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <returns></returns>
    public bool Insert(List<T> entity)
    {
        return DbBaseClient.Insertable(entity.ToArray()).ExecuteCommandIdentityIntoEntity();
         ;
    }

    /// <summary>
    ///     批量写入实体数据
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <returns></returns>
    public async Task<bool> InsertAsync(List<T> entity)
    {
        return await DbBaseClient.Insertable(entity.ToArray()).ExecuteCommandIdentityIntoEntityAsync();
    }

    /// <summary>
    ///     批量写入实体数据
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <returns></returns>
    public async Task<int> InsertCommandAsync(List<T> entity)
    {
        return await DbBaseClient.Insertable(entity.ToArray()).ExecuteCommandAsync();
    }

    /// <summary>
    ///     批量更新实体数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Update(List<T> entity)
    {
        return DbBaseClient.Updateable(entity).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     批量更新实体数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(List<T> entity)
    {
        return await DbBaseClient.Updateable(entity).ExecuteCommandHasChangeAsync();
    }

    public async Task<bool> UpdateAsync(
        Expression<Func<T, bool>> expression
        )
    {
        return await DbBaseClient
            .Queryable<T>()
            .ClearFilter()
            .AnyAsync(expression);
    }

    public int Update(Expression<Func<T, bool>> columns,
        Expression<Func<T, bool>> expression)
    {
        return DbBaseClient.Updateable<T>().SetColumns(columns).Where(expression).ExecuteCommand();
    }


    public async Task<int> UpdateAsync(
        Expression<Func<T, bool>> columns,
        Expression<Func<T, bool>> expression
        )
    {
        return await DbBaseClient
            .Updateable<T>()
            .SetColumns(columns)
            .Where(expression)
            .ExecuteCommandAsync();
    }

    public async Task<int> UpdateAsync(
        Expression<Func<T, object>> columns
        )
    {
        return await DbBaseClient
            .Updateable<T>()
            .IgnoreColumns(columns)
            .ExecuteCommandAsync();
    }
    public async Task<int> UpdateAsync(
        T updateObj,
        bool ignoreAllNullColumns
        )
    {
        return await DbBaseClient
            .Updateable(updateObj)
            .IgnoreColumns(ignoreAllNullColumns)
            .ExecuteCommandAsync();
    }
    public async Task<int> UpdateAsync(
        T updateObj, bool ignoreAllNullColumns, Expression<Func<T, object>> columns
        )
    {
        return await DbBaseClient
            .Updateable<T>(updateObj)
            .IgnoreColumns(ignoreAllNullColumns)
            .IgnoreColumns(columns)
            .ExecuteCommandAsync();
    }
    /// <summary>
    ///     更新实体数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Update(T entity)
    {
        return DbBaseClient.Updateable(entity).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     更新实体数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(T entity)
    {
        return await DbBaseClient.Updateable(entity).ExecuteCommandHasChangeAsync();
    }


    /// <summary>
    ///     根据手写条件更新
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public bool Update(T entity, string strWhere)
    {
        return DbBaseClient.Updateable(entity).Where(strWhere).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     根据手写条件更新
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(T entity, string strWhere)
    {
        return await DbBaseClient.Updateable(entity).Where(strWhere).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     根据手写sql语句更新数据
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public bool Update(string strSql, SugarParameter[] parameters = null)
    {
        return DbBaseClient.Ado.ExecuteCommand(strSql, parameters) > 0;
    }

    /// <summary>
    ///     根据手写sql语句更新数据
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(string strSql, SugarParameter[] parameters = null)
    {
        return await DbBaseClient.Ado.ExecuteCommandAsync(strSql, parameters) > 0;
    }

    /// <summary>
    ///     更新某个字段
    /// </summary>
    /// <param name="columns">lamdba表达式,如it => new Student() { Name = "a", CreateTime = DateTime.Now }</param>
    /// <param name="where">lamdba判断</param>
    /// <returns></returns>
    public bool Update(Expression<Func<T, T>> columns, Expression<Func<T, bool>> where)
    {
        var i = DbBaseClient.Updateable<T>().SetColumns(columns).Where(where).ExecuteCommand();
        return i > 0;
    }

    /// <summary>
    ///     更新某个字段
    /// </summary>
    /// <param name="columns">lamdba表达式,如it => new Student() { Name = "a", CreateTime = DateTime.Now }</param>
    /// <param name="where">lamdba判断</param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> where)
    {
        return await DbBaseClient.Updateable<T>().SetColumns(columns).Where(where).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     根据条件更新
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="lstColumns"></param>
    /// <param name="lstIgnoreColumns"></param>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(T entity, List<string> lstColumns = null,
        List<string> lstIgnoreColumns = null, string strWhere = "")
    {
        var up = DbBaseClient.Updateable(entity);
        if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
        if (lstColumns != null && lstColumns.Count > 0) up = up.UpdateColumns(lstColumns.ToArray());
        if (!string.IsNullOrEmpty(strWhere)) up = up.Where(strWhere);
        return await up.ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     根据条件更新
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="lstColumns"></param>
    /// <param name="lstIgnoreColumns"></param>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public bool Update(T entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null,
        string strWhere = "")
    {
        var up = DbBaseClient.Updateable(entity);
        if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
        if (lstColumns != null && lstColumns.Count > 0) up = up.UpdateColumns(lstColumns.ToArray());
        if (!string.IsNullOrEmpty(strWhere)) up = up.Where(strWhere);
        return up.ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除数据
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <returns></returns>
    public bool Delete(T entity)
    {
        return DbBaseClient.Deleteable(entity).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除数据
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(T entity)
    {
        return await DbBaseClient.Deleteable(entity).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     删除数据
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <returns></returns>
    public bool Delete(IEnumerable<T> entity)
    {
        return DbBaseClient.Deleteable<T>(entity).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除数据
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(IEnumerable<T> entity)
    {
        return await DbBaseClient.Deleteable<T>(entity).ExecuteCommandHasChangeAsync();
    }
    /// <summary>
    /// 新增方法
    /// 该方法用于：根据角色Id删除用户角色
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selectExpression"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public async Task DeleteUserRoleByRoleId(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, long>> selectExpression, Action<long> action)
    {
        await DbBaseClient.Queryable<T>()
             .Where(predicate)
             .Select(selectExpression)
             .ForEachAsync(action);
    }
    /// <summary>
    ///     删除数据
    /// </summary>
    /// <param name="where">过滤条件</param>
    /// <returns></returns>
    public bool Delete(Expression<Func<T, bool>> where)
    {
        return DbBaseClient.Deleteable(where).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除数据
    /// </summary>
    /// <param name="where">过滤条件</param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(Expression<Func<T, bool>> where)
    {
        return await DbBaseClient.Deleteable(where).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     删除指定ID的数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool DeleteById(object id)
    {
        return DbBaseClient.Deleteable<T>(id).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除指定ID的数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdAsync(object id)
    {
        return await DbBaseClient.Deleteable<T>(id).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public bool DeleteByIds(int[] ids)
    {
        return DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdsAsync(int[] ids)
    {
        return await DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public bool DeleteByIds(long[] ids)
    {
        return DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdsAsync(long[] ids)
    {
        return await DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
    }


    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public bool DeleteByIds(Guid[] ids)
    {
        return DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdsAsync(Guid[] ids)
    {
        return await DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
    }


    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public bool DeleteByIds(string[] ids)
    {
        return DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdsAsync(string[] ids)
    {
        return await DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
    }


    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public bool DeleteByIds(List<int> ids)
    {
        return DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdsAsync(List<int> ids)
    {
        return await DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public bool DeleteByIds(List<string> ids)
    {
        return DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdsAsync(List<string> ids)
    {
        return await DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public bool DeleteByIds(List<Guid> ids)
    {
        return DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdsAsync(List<Guid> ids)
    {
        return await DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public bool DeleteByIds(List<long> ids)
    {
        return DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChange();
    }

    /// <summary>
    ///     删除指定ID集合的数据(批量删除)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteByIdsAsync(List<long> ids)
    {
        return await DbBaseClient.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
    }


    /// <summary>
    ///     判断数据是否存在
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public bool Exists(Expression<Func<T, bool>> predicate, bool blUseNoLock = false)
    {
        return DbBaseClient.Queryable<T>().Where(predicate).WithNoLockOrNot(blUseNoLock).Any();
    }

    /// <summary>
    ///     判断数据是否存在
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, bool blUseNoLock = false)
    {
        return await DbBaseClient.Queryable<T>().Where(predicate).WithNoLockOrNot(blUseNoLock).AnyAsync();
    }

    /// <summary>
    ///     获取数据总数
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public int GetCount(Expression<Func<T, bool>> predicate, bool blUseNoLock = false)
    {
        return DbBaseClient.Queryable<T>().WithNoLockOrNot(blUseNoLock).Count(predicate);
    }

    /// <summary>
    ///     获取数据总数
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate, bool blUseNoLock = false)
    {
        return await DbBaseClient.Queryable<T>().WithNoLockOrNot(blUseNoLock).CountAsync(predicate);
    }

    /// <summary>
    ///     获取数据某个字段的合计
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="field">字段</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public int GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> field, bool blUseNoLock = false)
    {
        return DbBaseClient.Queryable<T>().Where(predicate).WithNoLockOrNot(blUseNoLock).Sum(field);
    }

    /// <summary>
    ///     获取数据某个字段的合计
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="field">字段</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<int> GetSumAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> field,
        bool blUseNoLock = false)
    {
        return await DbBaseClient.Queryable<T>().Where(predicate).WithNoLockOrNot(blUseNoLock).SumAsync(field);
    }

    /// <summary>
    ///     获取数据某个字段的合计
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="field">字段</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public decimal GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal>> field,
        bool blUseNoLock = false)
    {
        return DbBaseClient.Queryable<T>().Where(predicate).WithNoLockOrNot(blUseNoLock).Sum(field);
    }

    /// <summary>
    ///     获取数据某个字段的合计
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="field">字段</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<decimal> GetSumAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal>> field,
        bool blUseNoLock = false)
    {
        return await DbBaseClient.Queryable<T>().Where(predicate).WithNoLockOrNot(blUseNoLock).SumAsync(field);
    }

    /// <summary>
    ///     获取数据某个字段的合计
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="field">字段</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public float GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, float>> field,
        bool blUseNoLock = false)
    {
        return DbBaseClient.Queryable<T>().Where(predicate).WithNoLockOrNot(blUseNoLock).Sum(field);
    }

    /// <summary>
    ///     获取数据某个字段的合计
    /// </summary>
    /// <param name="predicate">条件表达式树</param>
    /// <param name="field">字段</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns></returns>
    public async Task<float> GetSumAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, float>> field,
        bool blUseNoLock = false)
    {
        return await DbBaseClient.Queryable<T>().Where(predicate).WithNoLockOrNot(blUseNoLock).SumAsync(field);
    }

    public async Task<List<T>> QueryPageAsync(
    Expression<Func<T, bool>> whereExpression,
    bool isWhere1, Expression<Func<T, bool>> expression1,
    bool isWhere2, Expression<Func<T, bool>> expression2,
    bool isWhere3, Expression<Func<T, bool>> expression3,
    Expression<Func<T, object>> orderBy,
    int pageNumber, int pageSize, RefAsync<int> totalNumber,
    bool blUseNoLock = false)
    {
        var page = await DbBaseClient
            .Queryable<T>()
            .Where(whereExpression)
            .WhereIF(isWhere1, expression1)
            .WhereIF(isWhere2, expression2)
            .WhereIF(isWhere3, expression3)
            .OrderBy(orderBy, OrderByType.Asc)
            .WithNoLockOrNot(blUseNoLock)
            .ToPageListAsync(pageNumber, pageSize, totalNumber);
        return page;
    }

    /// <summary>
    ///     查询-2表查询
    /// </summary>
    /// <typeparam name="T1">实体1</typeparam>
    /// <typeparam name="T2">实体2</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
    /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
    /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>值</returns>
    public List<TResult> QueryMuch<T1, T2, TResult>(
        Expression<Func<T1, T2, object[]>> joinExpression,
        Expression<Func<T1, T2, TResult>> selectExpression,
        Expression<Func<T1, T2, bool>> whereLambda = null,
        bool blUseNoLock = false) where T1 : class, new()
    {
        return DbBaseClient
            .Queryable(joinExpression)
            .WhereIF(whereLambda is not null, whereLambda)
            .Select(selectExpression)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    ///     查询-多表查询
    /// </summary>
    /// <typeparam name="T1">实体1</typeparam>
    /// <typeparam name="T2">实体2</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
    /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
    /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>值</returns>
    public async Task<List<TResult>> QueryMuchAsync<T1, T2, TResult>(
        Expression<Func<T1, T2, object[]>> joinExpression,
        Expression<Func<T1, T2, TResult>> selectExpression,
        Expression<Func<T1, T2, bool>> whereLambda = null,
        bool blUseNoLock = false) where T1 : class, new()
    {
        return await DbBaseClient
            .Queryable(joinExpression)
            .WhereIF(whereLambda is not null, whereLambda)
            .Select(selectExpression)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }

    /// <summary>
    ///     查询-二表查询
    /// </summary>
    /// <typeparam name="T1">实体1</typeparam>
    /// <typeparam name="T2">实体2</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
    /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
    /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>值</returns>
    public TResult QueryMuchFirst<T1, T2, TResult>(
        Expression<Func<T1, T2, object[]>> joinExpression,
        Expression<Func<T1, T2, TResult>> selectExpression,
        Expression<Func<T1, T2, bool>> whereLambda = null,
        bool blUseNoLock = false) where T1 : class, new()
    {
        return DbBaseClient
            .Queryable(joinExpression)
            .WhereIF(whereLambda is not null, whereLambda)
            .Select(selectExpression)
            .WithNoLockOrNot(blUseNoLock)
            .First();
    }

    /// <summary>
    ///     查询-二表查询
    /// </summary>
    /// <typeparam name="T1">实体1</typeparam>
    /// <typeparam name="T2">实体2</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
    /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
    /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>值</returns>
    public async Task<TResult> QueryMuchFirstAsync<T1, T2, TResult>(
        Expression<Func<T1, T2, object[]>> joinExpression,
        Expression<Func<T1, T2, TResult>> selectExpression,
        Expression<Func<T1, T2, bool>> whereLambda = null,
        bool blUseNoLock = false) where T1 : class, new()
    {
        return await DbBaseClient
            .Queryable(joinExpression)
            .WhereIF(whereLambda is not null, whereLambda)
            .Select(selectExpression).WithNoLockOrNot(blUseNoLock)
            .FirstAsync();
    }

    /// <summary>
    ///     查询-三表查询
    /// </summary>
    /// <typeparam name="T">实体1</typeparam>
    /// <typeparam name="T2">实体2</typeparam>
    /// <typeparam name="T3">实体3</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
    /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
    /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>值</returns>
    public List<TResult> QueryMuch<T1, T2, T3, TResult>(
        Expression<Func<T1, T2, T3, object[]>> joinExpression,
        Expression<Func<T1, T2, T3, TResult>> selectExpression,
        Expression<Func<T1, T2, T3, bool>> whereLambda = null,
        bool blUseNoLock = false) where T1 : class, new()
    {
        return DbBaseClient
            .Queryable(joinExpression)
            .WhereIF(whereLambda is not null, whereLambda)
            .Select(selectExpression)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    ///     查询-4表查询
    /// </summary>
    /// <typeparam name="T">实体1</typeparam>
    /// <typeparam name="T2">实体2</typeparam>
    /// <typeparam name="T3">实体3</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
    /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
    /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>值</returns>
    public List<TResult> QueryMuch<T1, T2, T3, T4, TResult>(
        Expression<Func<T1, T2, T3, T4, object[]>> joinExpression,
        Expression<Func<T1, T2, T3, T4, TResult>> selectExpression,
        Expression<Func<T1, T2, T3, T4, bool>> whereLambda = null,
        bool blUseNoLock = false) where T1 : class, new()
    {
        return DbBaseClient
            .Queryable(joinExpression)
            .WhereIF(whereLambda is not null, whereLambda)
            .Select(selectExpression)
            .WithNoLockOrNot(blUseNoLock)
            .ToList();
    }

    /// <summary>
    ///     查询-三表查询
    /// </summary>
    /// <typeparam name="T">实体1</typeparam>
    /// <typeparam name="T2">实体2</typeparam>
    /// <typeparam name="T3">实体3</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
    /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
    /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>
    /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
    /// <returns>值</returns>
    public async Task<List<TResult>> QueryMuchAsync<T1, T2, T3, TResult>(
        Expression<Func<T1, T2, T3, object[]>> joinExpression,
        Expression<Func<T1, T2, T3, TResult>> selectExpression,
        Expression<Func<T1, T2, T3, bool>> whereLambda = null,
        bool blUseNoLock = false) where T1 : class, new()
    {
        return await DbBaseClient
            .Queryable(joinExpression)
            .WhereIF(whereLambda is not null, whereLambda)
            .Select(selectExpression)
            .WithNoLockOrNot(blUseNoLock)
            .ToListAsync();
    }

    /// <summary>
    ///     执行sql语句并返回List<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public List<T> SqlQuery(string sql, List<SugarParameter> parameters)
    {
        var list = DbBaseClient.Ado.SqlQuery<T>(sql, parameters);
        return list;
    }

    /// <summary>
    ///     执行sql语句并返回List<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <returns></returns>
    public async Task<List<T>> SqlQueryable(string sql)
    {
        var list = await DbBaseClient.SqlQueryable<T>(sql).ToListAsync();
        return list;
    }

    /// <summary>
    /// 执行SQL语句并返回List<T> - 同步版本
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <param name="parameters">参数（可选）</param>
    /// <param name="timeoutSeconds">超时时间（秒）</param>
    /// <returns>实体列表</returns>
    public List<T> SqlQueryable2(string sql, 
        SugarParameter[] parameters = null, 
        int timeoutSeconds = 30,
        bool useNoLock = false)
    {
        try
        {
            // 设置超时
            if (timeoutSeconds > 0)
            {
                DbBaseClient.Ado.CommandTimeOut = timeoutSeconds;
            }
        
            // 添加NOLOCK提示
            if (useNoLock && sql.Contains("SELECT"))
            {
                sql = AddNoLockHint(sql);
            }
        
            // 执行查询
            if (parameters != null && parameters.Length > 0)
            {
                return DbBaseClient.Ado.SqlQuery<T>(sql, parameters);
            }
            else
            {
                return DbBaseClient.Ado.SqlQuery<T>(sql);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /// <summary>
    /// 在SQL中添加NOLOCK提示
    /// </summary>
    private string AddNoLockHint(string sql)
    {
        if (sql.IndexOf("WITH(NOLOCK)", StringComparison.OrdinalIgnoreCase) >= 0)
            return sql;
        
        // 简单的NOLOCK添加逻辑（适用于简单查询）
        var lowerSql = sql.ToLower();
        var fromIndex = lowerSql.IndexOf("from ");
        if (fromIndex > 0)
        {
            var fromPart = sql.Substring(fromIndex);
            var parts = fromPart.Split(new[] { "join", "where", "order by", "group by" }, 
                StringSplitOptions.None);
        
            if (parts.Length > 0)
            {
                var tablePart = parts[0];
                var newTablePart = tablePart.TrimEnd() + " WITH(NOLOCK)";
                sql = sql.Replace(tablePart, newTablePart);
            }
        }
    
        return sql;
    }

}
