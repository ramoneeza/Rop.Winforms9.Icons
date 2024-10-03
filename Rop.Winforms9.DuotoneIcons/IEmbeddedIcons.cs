namespace Rop.Winforms9.DuotoneIcons;

public interface IEmbeddedIcons
{
    string FontName { get; }
    IReadOnlyList<string> Codes { get; }
    IReadOnlyList<string> Aliases { get; }
    Size BaseSize { get; }
    int Count { get; }
    DuoToneIcon? GetIcon(string name);
    float DrawIcon(Graphics gr, string code, DuoToneColor iconcolor, float x, float y, float height);
    float DrawIconBaseLine(Graphics gr, string code, DuoToneColor iconcolor, float x, float baseline, float height);
    void DrawIconFit(Graphics gr, string code, DuoToneColor iconcolor, float x, float y, float width);
}
