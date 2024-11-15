using System.ComponentModel;
using Rop.Winforms9.ColumnsListBox;
using Rop.Winforms9.ListComboBox;

namespace ColumnListBoxTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            columnListBox1.SetItems(new MiRecord(0, "Juan", "Perez"), new MiRecord(1, "Ana", "López"), new MiRecord(2, "Tomás", "Rodríguez"), new MiRecord(3, "Ángel", "Zamorano"),new MiRecord(4,"Ana","Rodríguez"),new MiRecord(5,"Tomás","López"));
            columnListBox1.DrawColumns += ColumnListBox1_DrawColumns;
            columnListBox1.ColumnFilterClick += ColumnListBox1_ColumnFilterClick;
        }

        private void ColumnListBox1_ColumnFilterClick(object? sender, ColumnFilterClickArgs e)
        {
            var apellidos=e.Items.OfType<MiRecord>().Select(a => a.Apellidos).Distinct().ToArray();
            var active = e.ActiveFilter.Split(',');
            var res=columnListBox1.ShowFilterDialog(e.Column.ColumnIndex, true, apellidos, active);
            e.ActiveFilter = string.Join(',', res);
        }

        private void ColumnListBox1_DrawColumns(object? sender, Rop.Winforms9.ColumnsListBox.DrawColumnsEventArgs e)
        {
            var item = e.Item as MiRecord;
            if (item is null) return;
            var txt = e.ColumnIndex switch
            {
                0 => item.Id.ToString(),
                1 => item.Nombre,
                2 => item.Apellidos,
                _ => ""
            };
            var a = e.ColumnIndex switch
            {
                0 => HorizontalAlignment.Right,
                1 => HorizontalAlignment.Left,
                2 => HorizontalAlignment.Left,
                _ => HorizontalAlignment.Left
            };
            e.DrawText(txt, a);
        }

        private void columnListBox1_SortItems(object sender, Rop.Winforms9.ColumnsListBox.SortItemsArg e)
        {
            var preitems = e.Items.OfType<MiRecord>();
            var items = (e.SelectedColumn switch
            {
                0 => preitems.OrderBy(a => a.Id).ToList(),
                1 => preitems.OrderBy(a => a.Nombre, StringComparer.CurrentCultureIgnoreCase).ThenBy(a=>a.Apellidos,StringComparer.CurrentCultureIgnoreCase).ToList(),
                2 => preitems.OrderBy(a => a.Apellidos, StringComparer.CurrentCultureIgnoreCase).ThenBy(a=>a.Nombre,StringComparer.CurrentCultureIgnoreCase).ToList(),
                _ => preitems
            }).ToList();
            if (e.SelectedOrder == SortOrder.Descending) items.Reverse();
            var activefilters=columnListBox1.ActiveFilters.Select(a => a.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToArray();
            if (activefilters[2].Any())
            {
                items = items.Where(a => activefilters[2].Contains(a.Apellidos)).ToList();
            }
            e.Items = items.ToList<object>();
        }
    }

    public record MiRecord(int Id, string Nombre, string Apellidos);

}
