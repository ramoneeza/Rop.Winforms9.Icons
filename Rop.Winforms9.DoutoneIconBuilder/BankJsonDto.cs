using System.Text.Json;

namespace Rop.Winforms8._1.DoutoneIconBuilder;

public record BankJsonDto
{
    public const string Jsonfilename= "_bank.json";
    public string Name { get; init; } = "";
    public string Initials { get; init; } = "";
    public CustomSize BaseSize { get; init; } = new Size(0, 0);
    public List<BmpIconDto> Icons { get; init; } = [];
}