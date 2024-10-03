namespace Rop.Winforms8._1.DoutoneIconBuilder;

public record CustomSize
{
    public int Width { get; init; }
    public int Height { get; init; }
    public CustomSize() { }
    public CustomSize(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public CustomSize(Size size)
    {
        Width = size.Width;
        Height = size.Height;
    }
    public Size ToSize()
    {
        return new Size(Width, Height);
    }
    public static implicit operator Size(CustomSize other)=>other.ToSize();
    public static implicit operator CustomSize(Size other) => new CustomSize(other);
}