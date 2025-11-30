using Core.Constant;
using Core.QueryExpression;

namespace Core.Contract;

public class RequestPaginationBase : RequestBase
{
    public int SkipCount { get; set; } = 0;
    public int TakeCount { get; set; } = 10;
    public OrderByType? OrderByType { get; set; } = Constant.OrderByType.Asc;
    public string? OrderColumn { get; set; } = "Id";
    public List<QueryParameter> QueryParameters { get; set; } = new();
}