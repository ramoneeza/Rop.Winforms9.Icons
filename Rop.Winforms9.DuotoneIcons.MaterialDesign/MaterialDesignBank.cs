using Rop.Winforms9.DuoToneIcons.MaterialDesign;
using System.ComponentModel;

namespace Rop.Winforms9.DuotoneIcons.MaterialDesign;

internal partial class Dummy
{
}

public class MaterialDesignBank : Component, IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public MaterialDesignBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<MaterialDesignIcons>();
    }
}