using System.Diagnostics.CodeAnalysis;

namespace IngenuityNow.Common.Result;

/// <summary>
/// An object used to represent the result of an operation and return a single value.
/// </summary>
[ExcludeFromCodeCoverage]
public class ItemResult<TValue> : Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemResult{TValue}"/> class.
    /// </summary>
    public ItemResult()
    {
        Value = default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemResult{TValue}"/> class with the provided status.
    /// </summary>
    /// <param name="status">The status to set.</param>
    /// <param name="messages">The messages.</param>
    public ItemResult(StatusType status, params Message[] messages)
        : base(status, messages)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemResult{TValue}"/> class with the provided status and value.
    /// </summary>
    /// <param name="status">The status to set.</param>
    /// <param name="value">The value.</param>
    /// <param name="messages">The messages.</param>
    public ItemResult(StatusType status, TValue value, params Message[] messages)
        : base(status, messages)
    {
        Value = value;
    }

    /// <summary>
    /// The value returned from the operation.
    /// </summary>
    public TValue Value { get; set; }

    /// <summary>
    /// Create a new <see cref="ItemResult{TValue}"/> object with status set to <see cref="StatusType.Success"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ItemResult{TValue}"/> object with status set to <see cref="StatusType.Success"/>.</returns>
    public static new ItemResult<TValue> Success(params Message[] messages) { return new ItemResult<TValue>(StatusType.Success, messages); }

    /// <summary>
    /// Create a new <see cref="ItemResult{TValue}"/> object with status set to <see cref="StatusType.Success"/>.
    /// </summary>
    /// <param name="value">The value for the result.</param>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ItemResult{TValue}"/> object with status set to <see cref="StatusType.Success"/>.</returns>
    public static ItemResult<TValue> Success(TValue value, params Message[] messages) { return new ItemResult<TValue>(StatusType.Success, value, messages); }

    /// <summary>
    /// Create a new <see cref="ItemResult{TValue}"/> object with status set to <see cref="StatusType.BadRequest"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ItemResult{TValue}"/> object with status set to <see cref="StatusType.BadRequest"/>.</returns>
    public static new ItemResult<TValue> BadRequest(params Message[] messages) { return new ItemResult<TValue>(StatusType.BadRequest, messages); }

    /// <summary>
    /// Create a new <see cref="ItemResult{TValue}"/> object with status set to <see cref="StatusType.Duplicate"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Duplicate"/>.</returns>
    public static new ItemResult<TValue> Duplicate(params Message[] messages) { return new ItemResult<TValue>(StatusType.Duplicate, messages); }

    /// <summary>
    /// Create a new <see cref="ItemResult{TValue}"/> object with status set to <see cref="StatusType.NotFound"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ItemResult{TValue}"/> object with status set to <see cref="StatusType.NotFound"/>.</returns>
    public static new ItemResult<TValue> NotFound(params Message[] messages) { return new ItemResult<TValue>(StatusType.NotFound, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.ValidationFailed"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.ValidationFailed"/>.</returns>
    public static new ItemResult<TValue> ValidationFailed(params Message[] messages) { return new ItemResult<TValue>(StatusType.ValidationFailed, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Unauthorized"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Unauthorized"/>.</returns>
    public static new ItemResult<TValue> Unauthorized(params Message[] messages) { return new ItemResult<TValue>(StatusType.Unauthorized, messages); }

    /// <summary>
    /// Create a new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Other"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="ListResult{TValue}"/> object with status set to <see cref="StatusType.Other"/>.</returns>
    public static new ItemResult<TValue> Other(params Message[] messages) { return new ItemResult<TValue>(StatusType.Other, messages); }
}

