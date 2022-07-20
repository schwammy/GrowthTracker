using IngenuityNow.Common.Result;

namespace IngenuityNow.GrowthTracker.Api;

public static class HttpResponseExtensions
{
    public static bool IsItemResult(Result result, out object? value)
    {
        value = null;
        var resultType = result.GetType();
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(ItemResult<>))
        {
            value = resultType.GetProperty(nameof(ItemResult<int>.Value))?.GetValue(result);
            return true;
        }

        return false;
    }

    public static bool IsListResult(Result result, out object? listValue)
    {
        listValue = null;
        var resultType = result.GetType();
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(ListResult<>))
        {
            listValue = resultType.GetProperty(nameof(ListResult<int>.Values))?.GetValue(result);
            return true;
        }

        return false;
    }

    public static IResult ToHttpResult(this Result result)
    {
        return result.Status switch
        {
            StatusType.Success => IsItemResult(result, out object? value) || IsListResult(result, out value) ? Results.Ok(value) : Results.Ok(),
            StatusType.ValidationFailed => Results.BadRequest(result.Messages),
            StatusType.Duplicate => Results.Conflict(result.Messages),
            StatusType.NotFound => Results.NotFound(),
            StatusType.Unauthorized => Results.Unauthorized(),
            StatusType.BadRequest => Results.BadRequest(result.Messages),
            StatusType.Other => Results.BadRequest(result.Messages),
            _ => Results.BadRequest(result.Messages)
        };
    }
}
