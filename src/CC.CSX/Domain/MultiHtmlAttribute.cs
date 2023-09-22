namespace CC.CSX;

using System.Collections;
using System.Text;

public class MultiHtmlAttribute : HtmlAttribute, IList<HtmlAttribute>
{
    public List<HtmlAttribute> Attributes { get; set; } = new();
    public MultiHtmlAttribute(string name = "#multi", IEnumerable<HtmlAttribute>? attributes = null) : base(name)
    {
        if (attributes is not null) Attributes = attributes.ToList();
    }

    public HtmlAttribute this[int index]
    {
        get => Attributes[index];
        set => Attributes[index] = value;
    }

    public int Count => Attributes.Count;

    public bool IsReadOnly => false;

    public void Add(HtmlAttribute item) => Attributes.Add(item);

    public void Clear() => Attributes.Clear();

    public bool Contains(HtmlAttribute item) => Attributes.Contains(item);

    public void CopyTo(HtmlAttribute[] array, int arrayIndex) => Attributes.CopyTo(array, arrayIndex);

    public IEnumerator<HtmlAttribute> GetEnumerator() => Attributes.GetEnumerator();

    public int IndexOf(HtmlAttribute item) => Attributes.IndexOf(item);

    public void Insert(int index, HtmlAttribute item) => Attributes.Insert(index, item);

    public bool Remove(HtmlAttribute item) => Attributes.Remove(item);

    public void RemoveAt(int index) => Attributes.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator() => Attributes.GetEnumerator();

    public override string ToString(int indent = 0) => string.Join(" ", Attributes.Select(a => a.ToString(indent)));
    public override string ToString() => ToString(0);
    private const char space = ' ';
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