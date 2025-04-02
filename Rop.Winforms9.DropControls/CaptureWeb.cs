using System.Drawing.Imaging;

namespace Rop.Winforms9.DropControls;

public static class CaptureWeb
{
    public static async Task<byte[]?> GetInternetFile(string url)
    {
        // refactorizar con httpclient
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }
        return null;
    }
    public static async Task<string?> GetInternetText(string url)
    {
        // refactorizar con httpclient
        using var client = new HttpClient();
        var responseContent = await client.GetAsync(url);
        if (responseContent.IsSuccessStatusCode)
        {
            return await responseContent.Content.ReadAsStringAsync();
        }
        return null;
    }
    public static async Task<byte[]?> GetChromeUrl(string url)
    {
        Form? w = null;
        Clipboard.Clear();
        w = new Form() { Size = new Size(0, 0) };
        var dr =Task.Run(()=>MessageBox.Show(w, "Copiar imagen en el portapapeles", "Copiar imagen en el portapapeles"));
        Bitmap? hay = null;
        while ((hay == null) && (dr.Status==TaskStatus.Running))
        {
            hay = Clipboard.ContainsImage() ? Clipboard.GetImage() as Bitmap : null;
            if (hay==null)
                await Task.Delay(250);
        }
        w.Close();
        if (hay== null) return null;
        using var ms=new MemoryStream();
        hay.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }
}