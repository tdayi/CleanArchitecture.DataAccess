using Core.Constant;
using Core.QueryExpression;

namespace Core.Contract;

public class RequestPaginationBase : RequestBase
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    public OrderByType? OrderByType { get; set; } = Constant.OrderByType.Asc;
    public string? OrderColumn { get; set; } = "Id";
    public List<QueryParameter> Parameters { get; set; } = new();
}