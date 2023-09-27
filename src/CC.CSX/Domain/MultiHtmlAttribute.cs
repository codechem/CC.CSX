namespace CC.CSX;

using System.Collections;
using System.Text;

/// <summary>
/// A class that represents an HTML attribute that can have multiple values.
/// It is a <see cref="HtmlAttribute"/> that has a name and a value but the value is a list of <see cref="HtmlAttribute"/>.
/// It is rendered as a space separated list of attributes.
/// </summary>
public class MultiHtmlAttribute : HtmlAttribute, IList<HtmlAttribute>
{
    private const char space = ' ';

    /// <summary>
    /// The list of attributes.
    /// </summary>
    public List<HtmlAttribute> Attributes { get; set; } = new();
    /// <summary>
    /// Creates a new instance of <see cref="MultiHtmlAttribute"/> with the given name and attributes.
    /// note: the name is set to <c>#multi</c> by default, and it is not rendered
    /// </summary>
    public MultiHtmlAttribute(string name = "#multi", IEnumerable<HtmlAttribute>? attributes = null) : base(name)
    {
        if (attributes is not null) Attributes = attributes.ToList();
    }

    /// <summary>
    /// Indexer for the <see cref="Attributes"/> list.
    /// </summary>
    public HtmlAttribute this[int index]
    {
        get => Attributes[index];
        set => Attributes[index] = value;
    }

    /// <inheritdoc/>
    public int Count => Attributes.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    public void Add(HtmlAttribute item) => Attributes.Add(item);

    /// <inheritdoc/>
    public void Clear() => Attributes.Clear();

    /// <inheritdoc/>
    public bool Contains(HtmlAttribute item) => Attributes.Contains(item);

    /// <inheritdoc/>
    public void CopyTo(HtmlAttribute[] array, int arrayIndex) => Attributes.CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    public IEnumerator<HtmlAttribute> GetEnumerator() => Attributes.GetEnumerator();

    /// <inheritdoc/>
    public int IndexOf(HtmlAttribute item) => Attributes.IndexOf(item);

    /// <inheritdoc/>
    public void Insert(int index, HtmlAttribute item) => Attributes.Insert(index, item);

    /// <inheritdoc/>
    public bool Remove(HtmlAttribute item) => Attributes.Remove(item);

    /// <inheritdoc/>
    public void RemoveAt(int index) => Attributes.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator() => Attributes.GetEnumerator();

    /// <inheritdoc/>
    public override string ToString(int indent = 0) => string.Join(" ", Attributes.Select(a => a.ToString(indent)));
    /// <inheritdoc/>
    public override string ToString() => ToString(0);
    /// <inheritdoc/>
    public override void AppendTo(ref StringBuilder sb, int indent = 0)
    {
        foreach (var attr in Attributes)
        {
            attr.AppendTo(ref sb, indent);
            sb.Append(space);
        }
        sb.Remove(sb.Length - 1, 1);
    }
}