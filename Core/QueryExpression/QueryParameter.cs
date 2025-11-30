namespace Core.QueryExpression;

public class QueryParameter
{
    public QueryOperator QueryOperator { get; set; }

    public string PropertyName { get; set; }

    public string Value { get; set; }
}