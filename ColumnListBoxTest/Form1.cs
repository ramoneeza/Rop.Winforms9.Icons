using Rop.Winforms9.ListComboBox;

namespace ColumnListBoxTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            columnListBox1.Items.AddRange(new MiRecord(0, "Juan", "Perez"), new MiRecord(1, "Ana", "López"), new MiRecord(2, "Tomás", "Rodríguez"), new MiRecord(3, "Ángel", "Zamorano"));
            columnListBox1.DrawColumns += ColumnListBox1_DrawColumns;
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
            var preitems = e.Items.Cast<MiRecord>();
            var items = e.SelectedColumn switch
            {
                0 => preitems.OrderBy(a => a.Id),
                1 => preitems.OrderBy(a => a.Nombre, StringComparer.OrdinalIgnoreCase).ThenBy(a=>a.Apellidos),
                2 => preitems.OrderBy(a => a.Apellidos, StringComparer.OrdinalIgnoreCase).ThenBy(a=>a.Nombre),
                _ => preitems
            };
            e.Items = ((e.SelectedOrder == SortOrder.Descending) ? items.Reverse() : items).ToList<object>();
        }
    }

    public record MiRecord(int Id, string Nombre, string Apellidos);

}
