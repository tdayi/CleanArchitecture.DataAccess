namespace Core.Contract;

public class ResponsePaginationBase : ResponseBase
{
    public int TotalCount { get; set; }
}