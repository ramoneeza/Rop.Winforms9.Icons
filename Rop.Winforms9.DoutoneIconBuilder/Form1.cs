using Rop.Winforms8._1.DoutoneIconBuilder.Controller;

namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    public partial class Form1 : Form
    {
        public Form1Controller Controller { get; }
        public Form1TopController TopController { get; }
        public Form1ListadoController ListadoController { get; }
        public Form1PanelMainController PanelMainController { get; }
        public Form1(BankJson bankJson)
        {
            InitializeComponent();
            Controller = new Form1Controller(this,bankJson);
            TopController = new Form1TopController(this);
            ListadoController = new Form1ListadoController(this);
            PanelMainController = new Form1PanelMainController(this);
        }
    }
}
