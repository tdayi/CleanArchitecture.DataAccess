using System.Runtime.Serialization;

namespace Core.QueryExpression;

public enum QueryOperator
{
    [EnumMember] Contains = 6,
    [EnumMember] EndsWith = 8,
    [EnumMember] Equals = 0,
    [EnumMember] GreaterThan = 2,
    [EnumMember] GreaterThanOrEqual = 4,
    [EnumMember] LessThan = 3,
    [EnumMember] LessThanOrEqual = 5,
    [EnumMember] NotEquals = 1,
    [EnumMember] StartsWith = 7
}