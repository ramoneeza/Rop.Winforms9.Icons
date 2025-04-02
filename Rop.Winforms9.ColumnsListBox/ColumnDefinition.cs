using System.ComponentModel;
using System.Runtime.CompilerServices;
using Rop.Winforms9.Helper;

namespace Rop.Winforms9.ColumnsListBox;

[TypeConverter(typeof(ColumnDefinitionConverter))]
public class ColumnDefinition:INotifyPropertyChanged,IEquatable<ColumnDefinition>,ITypeConvertible<ColumnDefinition>
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;


    public ContentAlignment TextAlign
    {
        get => _textAlign;
        set => SetProperty(ref _textAlign, value);
    }

    private int _width = 100;


    public int Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }

    private int _minWidth = 0;
    public int MinWidth
    {
        get => _minWidth;
        set => SetProperty(ref _minWidth, value);
    }

    private string _text = "";


    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    private bool _selectable = true;

    public bool Selectable
    {
        get => _selectable;
        set => SetProperty(ref _selectable, value);
    }

    private bool _resizable = false;

    public bool Resizable
    {
        get => _resizable;
        set => SetProperty(ref _resizable, value);
    }

    private string _toolTipText = "";


    public string ToolTipText
    {
        get => _toolTipText;
        set => SetProperty(ref _toolTipText, value);
    }

    private bool _filterable = false;

    public bool Filterable
    {
        get=> _filterable;
        set => SetProperty(ref _filterable, value);
    }

    public ColumnDefinition(ContentAlignment textAlign, int width, string text, bool selectable,string tooltiptext,bool filterable,bool resizable,int minWidth)
    {
        _textAlign = textAlign;
        _width = width;
        _text = text;
        _selectable = selectable;
        _toolTipText = tooltiptext;
        _filterable = filterable;
        _resizable = resizable;
        _minWidth = minWidth;
    }
    public ColumnDefinition()
    {
    }

    public override string ToString() => ToParsableString();
        
    public static ColumnDefinition Empty { get; }= new ColumnDefinition();
    public bool Equals(ColumnDefinition? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _textAlign == other._textAlign && 
               _width == other._width && 
               _text == other._text && 
               _selectable == other._selectable && 
               _toolTipText==other._toolTipText && 
               _filterable==other._filterable  &&
               _resizable==other._resizable &&
               _minWidth==other._minWidth;
    }
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ColumnDefinition)obj);
    }
    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        return HashCode.Combine((int)_textAlign, _width, _text, _selectable,_toolTipText,_filterable,_resizable,_minWidth);
    }

    #region IPaseabeConvertible

    public static string[] SortedProperties() => new[]
    {
        nameof(TextAlign), nameof(Width), nameof(Text), nameof(Selectable), nameof(ToolTipText), nameof(Filterable), nameof(Resizable),nameof(MinWidth)
    };

    public string ToParsableString()
    {
        return
            $"{TextAlign}|{Width}|{Text}|{Selectable}|{ToolTipText}|{Filterable}|{Resizable}|{MinWidth}";
    }
    public static ColumnDefinition? Parse(string oritext)
    {
        try
        {
            var q =new Queue<string>(oritext.Split('|'));
            var textAlign = Enum.Parse<ContentAlignment>(q.Dequeue());
            var width = int.Parse(q.Dequeue());
            var text= q.Dequeue();
            var selectable = bool.Parse(q.Dequeue());
            var toolTipText = q.Dequeue();
            var filterable = bool.Parse(q.Dequeue());
            var resizable = bool.Parse(q.Dequeue());
            var minWidth = int.Parse(q.Dequeue());
            return new ColumnDefinition(textAlign, width, text, selectable, toolTipText, filterable, resizable,minWidth);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion
}