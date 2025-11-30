using Core.Constant;
using Core.QueryExpression;

namespace Core.Repository;

public class PagingRequest
{
    public int? Skip { get; set; }
    public int? Take { get; set; }
    public OrderByType? OrderByType { get; set; }
    public string? OrderColumn { get; set; }
    public List<QueryParameter>? Parameters { get; set; }
}