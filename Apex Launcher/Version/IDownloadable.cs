namespace Apex_Launcher {
    public interface IDownloadable {
        string Location { get; }
        double Number { get; }
        IDownloadable Prerequisite { get; }

        bool GreaterThan(IDownloadable other);

        bool Equals(object other);

        string ToString();

        bool NewerThanDownloaded();
    }
}
