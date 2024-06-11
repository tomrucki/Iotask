namespace Iotask.Models;

public class RequestMessage
{
    public int Id { get; set; }
    public DateTimeOffset RequestTime { get; set; }
    public string Request { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string User { get; set; }
}