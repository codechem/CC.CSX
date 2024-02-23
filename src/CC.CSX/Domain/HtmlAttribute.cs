namespace CC.CSX;

using System.IO;
using System.Text;

/// <summary>
/// Represents an attribute of a node.
/// It is a <see cref="HtmlItem"/> that has a name and a value.
/// and is rendered as <c>name="value"</c> or just <c>name</c> if the value is null.
/// </summary>
public class HtmlAttribute : HtmlItem
{
    const char CharQuote = '"';
    const char CharSingleQuote = '\'';
    const char CharEqual = '=';

    /// <summary>
    /// An empty attribute that does not render anything.
    /// It is useful for creating conditional attributes with ternary operators or similar.
    /// </summary>
    public static readonly HtmlAttribute Empty = new HtmlAttribute(string.Empty);

    /// <summary>
    /// Creates a new instance of <see cref="HtmlAttribute"/> with the given name and value.
    /// </summary>
    public HtmlAttribute(in string name, in string? value) : base(name, value) { }
    /// <summary>
    /// Creates a new instance of <see cref="HtmlAttribute"/> with the given name.
    /// </summary>
    public HtmlAttribute(in string name) : base(name) { }
    /// <summary>
    /// Renders the attribute to HTML by taking into account the indentation.
    /// </summary>
    public override string ToString(int indent = 0) => Value is null ? Name : $"{Name}=\"{Value}\"";
    /// <summary>
    /// Renders the attribute to HTML by taking into account the indentation.
    /// </summary>
    public override string ToString() => ToString(0);

    /// <summary>
    /// Renders the attribute to HTML by taking into account the indentation, but uses a <see cref="StringBuilder"/> instead of returning a <see cref="string"/>.
    /// </summary>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        if(string.IsNullOrEmpty(Name)) return;

        if (Value is null)
        {
            sb.Append(Name);
        }
        else
        {
            sb.Append(Name).Append('=').Append(Value);
        }
    }

    /// <summary>
    /// Renders the attribute to HTML by taking into account the indentation, but uses a <see cref="TextWriter"/> instead of returning a <see cref="string"/>.
    /// </summary>
    public override void WriteTo(ref TextWriter sb, int indent = 0)
    {
        if(string.IsNullOrEmpty(Name)) return;

        if (Value is null)
        {
            sb.Write(Name);
        }
        else
        {
            sb.Write(Name);
            sb.Write(CharEqual);
            sb.Write(CharQuote);
            sb.Write(Value);
            sb.Write(CharQuote);
        }
    }

    /// <summary>
    /// Implicit conversion from <see cref="string"/> to <see cref="HtmlAttribute"/>.
    /// </summary>
    public static implicit operator HtmlAttribute(in (string key, string value) tuple) => new HtmlAttribute(tuple.key, tuple.value);
}