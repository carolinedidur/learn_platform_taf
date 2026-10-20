namespace LP.Selenium.TAF.Core.UI.Services.Download;

public interface IDownloadService
{
    public string Directory { get; }

    public bool IsInProgress();

    public bool Exists(string fileName);

    public IReadOnlyList<string> GetFileNames();

    public string GetPath(string fileName);

    public void Delete();
}
