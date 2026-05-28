using System.ComponentModel;
using System.Reflection;

namespace Entity.Constant;

public class StationConstant
{
    /// <summary>
    ///  换电站状态： 1：营运中 2：歇业中 3：设备维护状态 4：暂停营业
    /// </summary>
    public enum StationStatus
    {
      
        [Description("营运中")]
        Run=1,
        [Description("歇业中")]
        Stop,
        [Description("设备维护")]
        Repair,
        [Description("暂停营业")]
        Suspend,
        
        
    }
    /// <summary>
    ///  换电方式：1:自动换电 2：手动换电
    /// </summary>
    public enum StationWay
    {
       
        [Description("自动换电")]
        Auto=1,
        [Description("手动换电")]
        Manual,
       
    }
    /// <summary>
    ///   换电模式：1：本地换电 2：远程换电
    /// </summary>
    public enum StationModel
    {
       
        [Description("本地换电")]
        Local=1,
        [Description("远程换电、云平台参与")]
        Remote,
       
    }
    
    
       
      
}