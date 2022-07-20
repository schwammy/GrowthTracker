using IngenuityNow.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace IngenuityNow.Common.Http
{
    public static class HttpResponseExtensions
    {
        public static IActionResult ToActionResult(this Result.Result result)
        {
            return result.Status switch
            {
                StatusType.Success => (IActionResult)new OkResult(),
                StatusType.ValidationFailed => new BadRequestObjectResult(result.Messages),
                StatusType.Duplicate => new ConflictObjectResult(result.Messages),
                StatusType.NotFound => new NotFoundResult(),
                StatusType.Unauthorized => new UnauthorizedResult(),
                StatusType.BadRequest => new BadRequestObjectResult(result.Messages),
                StatusType.Other => new BadRequestObjectResult(result.Messages),
                _ => new BadRequestObjectResult(result.Messages)
            };
        }

        public static IActionResult ToActionResult<T>(this ItemResult<T> result)
        {
            return result.Status switch
            {
                StatusType.Success => new OkObjectResult(result.Value),
                StatusType.ValidationFailed => new BadRequestObjectResult(result.Messages),
                StatusType.Duplicate => new ConflictObjectResult(result.Messages),
                StatusType.NotFound => new NotFoundResult(),
                StatusType.Unauthorized => new UnauthorizedResult(),
                StatusType.BadRequest => new BadRequestObjectResult(result.Messages),
                StatusType.Other => new BadRequestObjectResult(result.Messages),
                _ => new BadRequestObjectResult(result.Messages)
            };
        }

        public static IActionResult ToActionResult<T>(this ListResult<T> result)
        {
            return result.Status switch
            {
                StatusType.Success => new OkObjectResult(result.Values),
                StatusType.ValidationFailed => new BadRequestObjectResult(result.Messages),
                StatusType.Duplicate => new ConflictObjectResult(result.Messages),
                StatusType.NotFound => new NotFoundResult(),
                StatusType.Unauthorized => new UnauthorizedResult(),
                StatusType.BadRequest => new BadRequestObjectResult(result.Messages),
                StatusType.Other => new BadRequestObjectResult(result.Messages),
                _ => new BadRequestObjectResult(result.Messages)
            };
        }

    }

}
