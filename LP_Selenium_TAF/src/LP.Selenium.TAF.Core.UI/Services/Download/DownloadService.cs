namespace LP.Selenium.TAF.Core.UI.Services.Download;

public sealed class DownloadService : IDownloadService
{
    private static readonly string[] _partialExtensions = ["*.crdownload"];

    public DownloadService()
    {
        Directory = Path.Combine(Path.GetTempPath(), "downloads", Guid.NewGuid().ToString("N"));
        System.IO.Directory.CreateDirectory(Directory);
    }

    public string Directory { get; }

    public bool IsInProgress()
    {
        return _partialExtensions.Any(pattern => System.IO.Directory.EnumerateFiles(Directory, pattern).Any());
    }

    public bool Exists(string fileName)
    {
        return File.Exists(GetPath(fileName));
    }

    public IReadOnlyList<string> GetFileNames()
    {
        return [.. System.IO.Directory.EnumerateFiles(Directory).Select(path => Path.GetFileName(path))];
    }

    public string GetPath(string fileName)
    {
        return Path.Combine(Directory, fileName);
    }

    public void Delete()
    {
        if (System.IO.Directory.Exists(Directory))
        {
            System.IO.Directory.Delete(Directory, recursive: true);
        }
    }
}
