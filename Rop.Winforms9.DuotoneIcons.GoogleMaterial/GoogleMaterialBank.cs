using System.ComponentModel;

namespace Rop.Winforms9.DuotoneIcons.GoogleMaterial;

internal partial class Dummy
{
}

public class GoogleMaterialBank : Component, IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public GoogleMaterialBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<GoogleMaterialIcons>();
    }
}