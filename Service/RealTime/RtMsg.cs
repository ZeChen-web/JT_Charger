namespace Service.RealTime;

public class RtMsg
{
    public int Id { get; set; }
    public string FromUser { get; set; }
    public string ToUser { get; set; }
    public DateTime Datetime { get; set; }
    public string Cmd { get; set; }
    public string Msg { get; set; }
}
