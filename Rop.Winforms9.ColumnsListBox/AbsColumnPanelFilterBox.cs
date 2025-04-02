using System.ComponentModel;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.Helper;

namespace Rop.Winforms9.ColumnsListBox;

[DesignerCategory("Code")]
public abstract class AbsColumnPanelFilterBox<T> : Form
{
    private SlimControlDialogDecorator _slimControlDialogDecorator;
    protected ListBox ListBox { get; }
    protected CheckedListBox? CheckedListBox =>ListBox as CheckedListBox;
    private readonly SoloIconButton buttonok;
    private readonly SoloIconButton buttondelete;
    protected abstract int IndexOf(T item);
    [DefaultValue("")]
    public string DisplayMember
    {
        get => ListBox.DisplayMember;
        set=> ListBox.DisplayMember = value;
    }
    protected AbsColumnPanelFilterBox(Control parent, Rectangle columnbounds, IEnumerable<T> items,bool selectmultiple,IEnumerable<T>? selecteditems=null)
    {
        _slimControlDialogDecorator= new SlimControlDialogDecorator(this, parent, columnbounds);
        _slimControlDialogDecorator.FadeOut = true;
        var panel = new Panel()
        {
            Dock = DockStyle.Bottom,
            Text = "Filter",
            Height = 25,
            Padding = new Padding(0)
        };
        buttonok = new SoloIconButton()
        {
            Icons = IconRepository.DefaultBank,
            IconCode = "_Filter"
        };
        buttondelete= new SoloIconButton()
        {
            Icons = IconRepository.DefaultBank,
            IconCode = "_Delete"
        };
        ListBox =(selectmultiple)? new CheckedListBox() : new ListBox();
        ListBox.Dock= DockStyle.Fill;
        ListBox.IntegralHeight = false;
        if (CheckedListBox is not null)
        {
            CheckedListBox.CheckOnClick = true;
        }
        Controls.Add(ListBox);
        Controls.Add(panel);
        var w= panel.Width/2;
        buttondelete.Left = 0;
        buttondelete.Top = 0;
        buttondelete.Height = panel.Height;
        buttondelete.Width = w;
        buttondelete.IconColor = Color.Tomato;
        buttondelete.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        buttonok.Top = 0;
        buttonok.Height = panel.Height;
        buttonok.Width = w;
        buttonok.Left= panel.Width-buttonok.Width;
        buttonok.IconColor = Color.DarkGreen;
        buttonok.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        panel.Controls.Add(buttondelete);
        panel.Controls.Add(buttonok);
        ListBox.Items.Clear();
        ListBox.Items.AddRange(items.Cast<object>().ToArray());
        if (selecteditems != null)
        {
            foreach (var selecteditem in selecteditems)
            {
                if (selecteditem is null) continue;
                // ReSharper disable once VirtualMemberCallInConstructor
                var i= IndexOf(selecteditem);
                if (i>=0)
                {
                    if (ListBox is CheckedListBox ls)
                    {
                        ls.SetItemChecked(i,true);
                    }
                    else
                    {
                        ListBox.SelectedIndex = i;
                    }
                }
            }
        }

        ListBox.DisplayMember = DisplayMember;
        buttonok.Click += _buttonokClick;
        buttondelete.Click += _buttondeleteClick;
    }

    private void _buttondeleteClick(object? sender, EventArgs e)
    {
        if (CheckedListBox is not null)
        {
            for (int i = 0; i < CheckedListBox.Items.Count; i++)
            {
                CheckedListBox.SetItemChecked(i, false);
            }
        }
        else
        {
            ListBox.SelectedIndex = -1;
        }
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void _buttonokClick(object? sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
    public T[] SelectedItems()
    {
        if (CheckedListBox!=null)
        {
            return CheckedListBox.CheckedItems.OfType<T>().ToArray();
        }
        else
        {
            if (ListBox.SelectedItem is not T item) return [];
            return [item];
        }
    }
}