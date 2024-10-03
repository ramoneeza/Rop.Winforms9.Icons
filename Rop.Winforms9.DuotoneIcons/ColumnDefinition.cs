using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.DuotoneIcons
{
    [TypeConverter(typeof(ColumnDefinitionConverter))]
    public class ColumnDefinition:INotifyPropertyChanged,IEquatable<ColumnDefinition>
    {
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;

        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set
            {
                if (value == _textAlign) return;
                _textAlign = value;
                OnPropertyChanged(nameof(TextAlign));
            }
        }

        private int _width = 100;

        public int Width
        {
            get => _width;
            set
            {
                if (value == _width) return;
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private string _text = "";

        public string Text
        {
            get => _text;
            set
            {
                if (value == _text) return;
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private bool _selectable = true;

        public bool Selectable
        {
            get => _selectable;
            set
            {
                if (value == _selectable) return;
                _selectable = value;
                OnPropertyChanged(nameof(Selectable));
            }
        }

        private string _toolTipText = "";

        public string ToolTipText
        {
            get => _toolTipText;
            set
            {
                if (value == _toolTipText) return;
                _toolTipText = value;
                OnPropertyChanged(nameof(ToolTipText));
            }
        }

        public ColumnDefinition(ContentAlignment textAlign, int width, string text, bool selectable,string tooltiptext)
        {
            _textAlign = textAlign;
            _width = width;
            _text = text;
            _selectable = selectable;
            _toolTipText = tooltiptext;
        }

        public ColumnDefinition()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return $"{TextAlign}|{Width}|{Text}|{Selectable}|{ToolTipText}";
        }

        public static ColumnDefinition? Parse(string text)
        {
            var xs = text.Split('|');
            if (xs.Length!=5) return null;
            if (!Enum.TryParse<ContentAlignment>(xs[0], out var textAlign) 
                || !int.TryParse(xs[1], out var width) 
                || !bool.TryParse(xs[3], out var selectable)
                ) 
                return null;
            return new ColumnDefinition(textAlign, width, xs[2], selectable,xs[4]);
        }
        public static ColumnDefinition Empty { get; }= new ColumnDefinition();

        public bool Equals(ColumnDefinition? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return _textAlign == other._textAlign && _width == other._width && _text == other._text && _selectable == other._selectable && _toolTipText==other._toolTipText;
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
            return HashCode.Combine((int)_textAlign, _width, _text, _selectable,_toolTipText);
        }
    }
}
