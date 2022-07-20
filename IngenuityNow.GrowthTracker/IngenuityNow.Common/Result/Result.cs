using System.Diagnostics.CodeAnalysis;

namespace IngenuityNow.Common.Result;

/// <summary>
/// An object used to represent the result of an operation.
/// </summary>
[ExcludeFromCodeCoverage]
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    public Result()
    {
        Messages = new List<Message>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with the provided status and (optionally) messages.
    /// </summary>
    /// <param name="status">The status to set.</param>
    /// <param name="messages">The messages.</param>
    public Result(StatusType status, params Message[] messages)
    {
        Messages = new List<Message>(messages ?? Enumerable.Empty<Message>());
        Status = status;
    }

    /// <summary>
    /// A value that provides the overall status of the operation.
    /// </summary>
    public StatusType Status { get; set; }

    /// <summary>
    /// A list of messages supporting the result status.
    /// </summary>
    public List<Message> Messages { get; set; }

    /// <summary>
    /// Whether this result indicates success.
    /// </summary>
    public bool IsSuccess => Status == StatusType.Success;

    /// <summary>
    /// A helper method for validation failed situations.
    /// </summary>
    /// <param name="messageText">the message text</param>
    public void SetValidationFailed(string messageText)
    {
        Status = StatusType.ValidationFailed;
        Messages.Add(new Message
        {
            Text = messageText
        });
    }

    /// <summary>
    /// A helper method for validation failed situations.
    /// </summary>
    /// <param name="identifier">a string to identify the field or object for which the validation failed.</param>
    /// <param name="messageText">the message text</param>
    public void SetValidationFailed(string identifier, string messageText)
    {
        Status = StatusType.ValidationFailed;
        Messages.Add(new Message
        {
            Identifier = identifier,
            Text = messageText
        });
    }

    /// <summary>
    /// A helper method for validation failed situations.
    /// </summary>
    /// <param name="messageText">the message text</param>
    /// <param name="details">A list of strings that further explains the validation issue(s).</param>
    public void SetValidationFailed(string messageText, List<string> details)
    {
        Status = StatusType.ValidationFailed;
        var message = new Message { Text = messageText };
        foreach (var detail in details)
        {
            message.Details.Add(new KeyValuePair<string, string>(Message.PlainTextKey, detail));
        }

        Messages.Add(message);
    }

    /// <summary>
    /// A helper method for validation failed situations.
    /// </summary>
    /// <param name="identifier">a string to identify the field or object for which the validation failed.</param>
    /// <param name="messageText">the message text</param>
    /// <param name="details">A list of strings that further explains the validation issue(s).</param>
    public void SetValidationFailed(string identifier, string messageText, List<string> details)
    {
        Status = StatusType.ValidationFailed;
        Messages.Add(new Message
        {
            Identifier = identifier,
            Text = messageText,
            Details = details.Select(d => new KeyValuePair<string, string>(Message.PlainTextKey, d)).ToList()
        });
    }
    /// <summary>
    /// A helper method for validation failed situations.
    /// </summary>
    /// <param name="identifier">a string to identify the field or object for which the validation failed.</param>
    /// <param name="messageText">the message text</param>
    /// <param name="details">A list of KeyValuePairs that further explains the validation issue(s).</param>
    public void SetValidationFailed(string identifier, string messageText, List<KeyValuePair<string, string>> details)
    {
        Status = StatusType.ValidationFailed;
        Messages.Add(new Message
        {
            Identifier = identifier,
            Text = messageText,
            Details = details
        });
    }

    /// <summary>
    /// Create a new <see cref="Result"/> object with status set to <see cref="StatusType.Success"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="Result"/> object with status set to <see cref="StatusType.Success"/>.</returns>
    public static Result Success(params Message[] messages) { return new Result(StatusType.Success, messages); }

    /// <summary>
    /// Create a new <see cref="Result"/> object with status set to <see cref="StatusType.BadRequest"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="Result"/> object with status set to <see cref="StatusType.BadRequest"/>.</returns>
    public static Result BadRequest(params Message[] messages) { return new Result(StatusType.BadRequest, messages); }

    /// <summary>
    /// Create a new <see cref="Result"/> object with status set to <see cref="StatusType.Duplicate"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="Result"/> object with status set to <see cref="StatusType.Duplicate"/>.</returns>
    public static Result Duplicate(params Message[] messages) { return new Result(StatusType.Duplicate, messages); }

    /// <summary>
    /// Create a new <see cref="Result"/> object with status set to <see cref="StatusType.NotFound"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="Result"/> object with status set to <see cref="StatusType.NotFound"/>.</returns>
    public static Result NotFound(params Message[] messages) { return new Result(StatusType.NotFound, messages); }

    /// <summary>
    /// Create a new <see cref="Result"/> object with status set to <see cref="StatusType.ValidationFailed"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="Result"/> object with status set to <see cref="StatusType.ValidationFailed"/>.</returns>
    public static Result ValidationFailed(params Message[] messages) { return new Result(StatusType.ValidationFailed, messages); }

    /// <summary>
    /// Create a new <see cref="Result"/> object with status set to <see cref="StatusType.Unauthorized"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="Result"/> object with status set to <see cref="StatusType.Unauthorized"/>.</returns>
    public static Result Unauthorized(params Message[] messages) { return new Result(StatusType.Unauthorized, messages); }

    /// <summary>
    /// Create a new <see cref="Result"/> object with status set to <see cref="StatusType.Other"/>.
    /// </summary>
    /// <param name="messages">The messages to add to the result.</param>
    /// <returns>A new <see cref="Result"/> object with status set to <see cref="StatusType.Other"/>.</returns>
    public static Result Other(params Message[] messages) { return new Result(StatusType.Other, messages); }
}
