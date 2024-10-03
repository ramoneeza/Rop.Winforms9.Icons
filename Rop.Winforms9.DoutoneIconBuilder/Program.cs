using System.Text.Json;

namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    internal static class Program
    {
        private static readonly string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DuotoneIconBuilder9");
        private static readonly string ConfigFilePath = Path.Combine(AppDataPath, "config.json");

        public static IbConfiguration Configuration { get; set; } = new();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            CargarConfiguracion();
            Application.Run(new FormSelect());
        }
        public static void CargarConfiguracion()
        {
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }
            if (!File.Exists(ConfigFilePath)) return;
            string jsonString = File.ReadAllText(ConfigFilePath);
            Configuration=JsonSerializer.Deserialize<IbConfiguration>(jsonString)??throw new Exception("Can't deserialize config data");
        }

        public static void GuardarConfiguracion()
        {
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }
            string jsonString = JsonSerializer.Serialize(Configuration, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFilePath, jsonString);
        }
    }
}