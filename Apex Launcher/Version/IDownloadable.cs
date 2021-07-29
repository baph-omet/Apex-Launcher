// <copyright file="IDownloadable.cs" company="Baphomet Media">
// © Copyright by Baphomet Media 2020 CC BY-NC-ND
// </copyright>

namespace ApexLauncher {
    /// <summary>
    /// Interface for downloadable files.
    /// </summary>
    public interface IDownloadable {
        /// <summary>
        /// Gets remote file location.
        /// </summary>
        string Location { get; }

        /// <summary>
        /// Gets version number.
        /// </summary>
        double Number { get; }

        /// <summary>
        /// Gets a file prerequisite, may be null.
        /// </summary>
        IDownloadable Prerequisite { get; }

        /// <summary>
        /// Checks to see if this instance is greater than another instance.
        /// </summary>
        /// <param name="other">Object to compare to.</param>
        /// <returns>True if this instance is more recent than other instance.</returns>
        bool GreaterThan(IDownloadable other);

        /// <summary>
        /// Checks to see if this instance is the same as another instance.
        /// </summary>
        /// <param name="other">Other instance to check.</param>
        /// <returns>True if other instance is the same.</returns>
        bool Equals(object other);

        /// <summary>
        /// Converts this instance to a string representation.
        /// </summary>
        /// <returns>String representation of this instance.</returns>
        string ToString();

        /// <summary>
        /// Quick check to see if this instance is newer than the downloaded version.
        /// </summary>
        /// <returns>True if this instance is newer than the downloaded version.</returns>
        bool NewerThanDownloaded();
    }
}
