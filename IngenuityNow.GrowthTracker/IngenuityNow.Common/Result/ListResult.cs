using System.Diagnostics.CodeAnalysis;

namespace IngenuityNow.Common.Result;

/// <summary>
/// An object used to represent the result of an operation and return a list of values.
/// </summary>
[ExcludeFromCodeCoverage]
public class ListResult<TValue> : Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListResult{TValue}"/> class.
    /// </summary>
    public ListResult()
    {
        Values = new List<TValue>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListResult{TValue}"/> class with the provided status and messages.
    /// </summary>
    /// <param name="status">The status to set.</param>
    /// <param name="messages">The messages.</param>
    public ListResult(StatusType status, params Message[] messages)
        : base(status, messages)
    {
        Status = status;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListResult{TValue}"/> class with the provided status and values.
    /// </summary>
    /// <param name="status">The status to set.</param>
    /// <param name="values">The values.</param>
    /// <param name="messages">The messages.</param>
    public ListResult(StatusType status, IEnumerable<TValue> values, params Message[] messages)
        : base(status, messages)
    {
        Values = values.ToList();
    }

    /// <summary>
    /// The values returned from the operation.
    /// </summary>
    public List<TValue> Values { get; set; }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Success"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Success"/>.</returns>
    public static new ListResult<TValue> Success(params Message[] messages) { return new ListResult<TValue>(StatusType.Success, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Success"/>.
    /// </summary>
    /// <param name="values">The values for the result.</param>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Success"/>.</returns>
    public static ListResult<TValue> Success(IEnumerable<TValue> values, params Message[] messages) { return new ListResult<TValue>(StatusType.Success, values, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.BadRequest"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.BadRequest"/>.</returns>
    public static new ListResult<TValue> BadRequest(params Message[] messages) { return new ListResult<TValue>(StatusType.BadRequest, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Duplicate"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Duplicate"/>.</returns>
    public static new ListResult<TValue> Duplicate(params Message[] messages) { return new ListResult<TValue>(StatusType.Duplicate, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.NotFound"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.NotFound"/>.</returns>
    public static new ListResult<TValue> NotFound(params Message[] messages) { return new ListResult<TValue>(StatusType.NotFound, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.ValidationFailed"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.ValidationFailed"/>.</returns>
    public static new ListResult<TValue> ValidationFailed(params Message[] messages) { return new ListResult<TValue>(StatusType.ValidationFailed, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Unauthorized"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Unauthorized"/>.</returns>
    public static new ListResult<TValue> Unauthorized(params Message[] messages) { return new ListResult<TValue>(StatusType.Unauthorized, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Other"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Other"/>.</returns>
    public static new ListResult<TValue> Other(params Message[] messages) { return new ListResult<TValue>(StatusType.Other, messages); }
}
