using System.Linq.Expressions;
using System.Reflection;
using Core.Constant;

namespace Core.QueryExpression;

public static class QueryExpressionBuilder
{
    private static object ConvertToPropertyType(PropertyInfo property, object value)
    {
        if (property is null)
        {
            return null;
        }

        var propertyType = property.PropertyType;
        var underlyingType = Nullable.GetUnderlyingType(propertyType);
        var isNullable = underlyingType is not null;

        var targetType = underlyingType ?? propertyType;

        if (value is null || Convert.IsDBNull(value))
        {
            if (!isNullable && targetType.IsValueType)
            {
                throw new Exception("Non-nullable property cannot be assigned null.");
            }

            return null;
        }

        if (!targetType.IsEnum)
        {
            return Convert.ChangeType(value, targetType);
        }

        if (!Enum.TryParse(targetType, value.ToString(), true, out var enumValue))
        {
            throw new Exception($"Invalid enum value: {value}");
        }

        return enumValue;
    }

    private static Expression BuildFilterExpression<TEntity>(
        ParameterExpression parameterExpression,
        QueryParameter queryParameter)
    {
        var property = typeof(TEntity).GetProperty(queryParameter.PropertyName);

        if (property is null)
        {
            throw new Exception($"Property not found: {queryParameter.PropertyName}");
        }

        if (!IsOperatorValidForType(queryParameter.QueryOperator, property.PropertyType))
        {
            throw new Exception(
                $"'{queryParameter.QueryOperator}' operator is not valid for type '{property.PropertyType.Name}'");
        }

        var convertedValue = ConvertToPropertyType(property, queryParameter.Value);

        var left = Expression.Property(parameterExpression, property);
        var right = Expression.Constant(convertedValue, property.PropertyType);

        return queryParameter.QueryOperator switch
        {
            QueryOperator.Equals => Expression.Equal(left, right),
            QueryOperator.NotEquals => Expression.NotEqual(left, right),
            QueryOperator.GreaterThan => Expression.GreaterThan(left, right),
            QueryOperator.LessThan => Expression.LessThan(left, right),
            QueryOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, right),
            QueryOperator.LessThanOrEqual => Expression.LessThanOrEqual(left, right),

            QueryOperator.Contains => Expression.Call(
                left,
                typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!,
                right),

            QueryOperator.StartsWith => Expression.Call(
                left,
                typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) })!,
                right),

            QueryOperator.EndsWith => Expression.Call(
                left,
                typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) })!,
                right),

            _ => throw new NotSupportedException($"Operator '{queryParameter.QueryOperator}' is not supported")
        };
    }

    private static bool IsOperatorValidForType(QueryOperator op, Type type)
    {
        var isString = type == typeof(string);
        var isNumeric =
            type == typeof(int) ||
            type == typeof(long) ||
            type == typeof(float) ||
            type == typeof(double) ||
            type == typeof(decimal) ||
            type == typeof(short);

        var isDateTime = type == typeof(DateTime);

        return op switch
        {
            QueryOperator.Equals or QueryOperator.NotEquals => true,

            QueryOperator.GreaterThan or QueryOperator.LessThan
                or QueryOperator.GreaterThanOrEqual
                or QueryOperator.LessThanOrEqual => isNumeric || isDateTime,

            QueryOperator.Contains or QueryOperator.StartsWith or QueryOperator.EndsWith => isString,

            _ => false
        };
    }

    public static Expression<Func<TEntity, bool>> ToFilterExpression<TEntity>(
        this List<QueryParameter> parameters)
        where TEntity : class
    {
        if (parameters is null || parameters.Count == 0)
        {
            return null;
        }

        var parameterExpression = Expression.Parameter(typeof(TEntity), "x");

        Expression combinedExpression = null;

        foreach (var filter in parameters)
        {
            var exp = BuildFilterExpression<TEntity>(parameterExpression, filter);

            combinedExpression = combinedExpression is null
                ? exp
                : Expression.AndAlso(combinedExpression, exp);
        }

        return Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameterExpression);
    }

    public static IQueryable<TEntity> ApplyOrdering<TEntity>(
        this IQueryable<TEntity> source,
        string orderByProperty,
        OrderByType orderByType)
    {
        var property = typeof(TEntity).GetProperty(orderByProperty);

        if (property is null)
        {
            throw new Exception($"Property not found: {orderByProperty}");
        }

        var methodName = orderByType == OrderByType.Desc
            ? nameof(Queryable.OrderByDescending)
            : nameof(Queryable.OrderBy);

        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new[] { typeof(TEntity), property.PropertyType },
            source.Expression,
            Expression.Quote(orderByExpression));

        return source.Provider.CreateQuery<TEntity>(resultExpression);
    }
}