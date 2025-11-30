using Core.Constant;
using Core.QueryExpression;

namespace Core.Repository;

public class PagingRequest
{
    public int? SkipCount { get; set; }
    public int? TakeCount { get; set; }
    public OrderByType? OrderByType { get; set; }
    public string OrderColumn { get; set; }
    public List<QueryParameter> QueryParameters { get; set; }
}