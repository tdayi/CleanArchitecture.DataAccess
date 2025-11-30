namespace Core.Contract;

public class ResponseBase
{
    public bool HasError { get; set; }
    public string[] Messages { get; set; }
}