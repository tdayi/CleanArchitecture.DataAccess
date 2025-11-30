using System.ComponentModel;
using Core.Constant;
using Core.QueryExpression;

namespace Core.Contract;

public class RequestPaginationBase : RequestBase
{
    [DefaultValue(0)]
    public int Skip { get; set; } = 0;
    [DefaultValue(10)]
    public int Take { get; set; } = 10;
    [DefaultValue(Constant.OrderByType.Asc)]
    public OrderByType? OrderByType { get; set; } = Constant.OrderByType.Asc;
    [DefaultValue("Id")]
    public string? OrderColumn { get; set; } = "Id";
    [DefaultValue(null)]
    public List<QueryParameter>? Parameters { get; set; }
}