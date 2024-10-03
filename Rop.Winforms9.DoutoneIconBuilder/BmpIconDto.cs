namespace Rop.Winforms8._1.DoutoneIconBuilder;

public record BmpIconDto
{
    public string Code { get; init; } = "";
    public CustomSize Size { get; init; } = new Size(0, 0);
    public int BaseLine { get; init; } = 0;
    public bool IsNew { get; init; }
    public bool IsExtra { get; init; }
    public string[] Alias { get; init; }= Array.Empty<string>();
}