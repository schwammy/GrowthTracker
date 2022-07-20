namespace IngenuityNow.Common.Result;

/// <summary>
/// A message to be shared from one method to another.
/// </summary>
public class Message
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    public Message()
    {
        Details = new List<KeyValuePair<string, string>>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class with the given text.
    /// </summary>
    /// <param name="text">Text of the message</param>
    public Message(string text)
    {
        Text = text;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class with the given text and identifier.
    /// </summary>
    /// <param name="text">Text of the message</param>
    /// <param name="identifier">a string to identify the field or object for which the message is related.</param>
    public Message(string text, string identifier)
    {
        Text = text;
        Identifier = identifier;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class with the given text, identifier and details.
    /// </summary>
    /// <param name="text">Text of the message</param>
    /// <param name="identifier">a string to identify the field or object for which the message is related.</param>
    /// <param name="details">A list of KeyValuePairs that provides additional details about the message.</param>
    public Message(string text, string identifier, List<KeyValuePair<string, string>> details)
    {
        Text = text;
        Identifier = identifier;
        Details = details;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class with the given text, identifier and details.
    /// </summary>
    /// <param name="text">Text of the message</param>
    /// <param name="identifier">a string to identify the field or object for which the message is related.</param>
    /// <param name="details">A list of strings that provides additional details about the message.</param>
    public Message(string text, string identifier, List<string> details)
    {
        Text = text;
        Identifier = identifier;
        Details = new List<KeyValuePair<string, string>>();
        foreach (string d in details)
        {
            Details.Add(new KeyValuePair<string, string>(PlainTextKey, d));
        }
    }

    /// <summary>
    /// The Text of the message.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Optionally used to identify a specific portion of the data that the message applies to.
    /// For instance, for validation error for a Person, the identifier could be "FirstName".
    /// It could also be used to reference an item in list by name or id.
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Optional details to provide more information expanding on the Text.
    /// </summary>
    public List<KeyValuePair<string, string>> Details { get; set; }

    /// <summary>
    /// This key is used for Details where the List of KeyValuePair is meant be treated like a List of string
    /// </summary>
    public static string PlainTextKey => "_";
}
