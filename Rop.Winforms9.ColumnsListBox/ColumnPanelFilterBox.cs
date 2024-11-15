using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Rop.Winforms9.DuotoneIcons;
using Rop.Winforms9.DuotoneIcons.Controls;
using Rop.Winforms9.Helper;
using Timer = System.Windows.Forms.Timer;

namespace Rop.Winforms9.ColumnsListBox
{
    [DesignerCategory("Code")]
    public class ColumnPanelFilterBox : Form
    {
        private SlimControlDialogDecorator _slimControlDialogDecorator;
        private ListBox _listBox;
        private CheckedListBox? _checkedListBox =>_listBox as CheckedListBox;
        private readonly SoloIconButton buttonok;
        private readonly SoloIconButton buttondelete;
        public ColumnPanelFilterBox(Control parent, Rectangle columnbounds, IEnumerable<string> items,bool selectmultiple,IEnumerable<string>? selecteditems=null)
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
            _listBox =(selectmultiple)? new CheckedListBox() : new ListBox();
            _listBox.Dock= DockStyle.Fill;
            _listBox.IntegralHeight = false;
            if (_checkedListBox is not null)
            {
                _checkedListBox.CheckOnClick = true;
            }
            Controls.Add(_listBox);
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
            _listBox.Items.Clear();
            _listBox.Items.AddRange(items.ToArray<object>());
            if (selecteditems != null)
            {
                foreach (var selecteditem in selecteditems)
                {
                    var i= _listBox.Items.IndexOf(selecteditem);
                    if (i>=0)
                    {
                        if (_listBox is CheckedListBox ls)
                        {
                            ls.SetItemChecked(i,true);
                        }
                        else
                        {
                            _listBox.SelectedIndex = i;
                        }
                    }
                }
            }
            buttonok.Click += _buttonokClick;
            buttondelete.Click += _buttondeleteClick;
        }

        private void _buttondeleteClick(object? sender, EventArgs e)
        {
            if (_checkedListBox is not null)
            {
                for (int i = 0; i < _checkedListBox.Items.Count; i++)
                {
                    _checkedListBox.SetItemChecked(i, false);
                }
            }
            else
            {
                _listBox.SelectedIndex = -1;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void _buttonokClick(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public string[] SelectedItems()
        {
            if (_checkedListBox!=null)
            {
                return _checkedListBox.CheckedItems.Cast<string>().ToArray();
            }
            else
            {
                return new[] {_listBox.SelectedItem?.ToString() ?? ""};
            }
        }
    }


}
