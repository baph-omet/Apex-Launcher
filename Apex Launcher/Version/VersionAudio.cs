// <copyright file="VersionAudio.cs" company="Baphomet Media">
// © Copyright by Baphomet Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ApexLauncher {
    /// <summary>
    /// Object that represents a version of the game's audio files.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="VersionAudio"/> class.
    /// </remarks>
    /// <param name="number">Version number.</param>
    /// <param name="location">Remote download location.</param>
    public class VersionAudio(double number, string location) : IDownloadable {
        /// <inheritdoc/>
        public string Location { get; } = location;

        /// <inheritdoc/>
        public double Number { get; } = number;

        /// <inheritdoc/>
        public IDownloadable Prerequisite { get; }

        /// <summary>
        /// Get audio version from string name.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns>A cooresponding audio version based on the string, if it exists.</returns>
        /// <exception cref="ArgumentNullException">Thrown if argument is null.</exception>
        public static VersionAudio FromString(string str) {
            ArgumentNullException.ThrowIfNull(str);

            int number = Convert.ToInt32(str.Replace("Audio v", string.Empty));

            if (!File.Exists(Path.Combine(Config.InstallPath, "Versions", "VersionManifestAudio.xml"))) return new VersionAudio(number, string.Empty);
            else return FromNumber(Convert.ToInt32(str.Replace("Audio v", string.Empty)));
        }

        /// <summary>
        /// Gets all audio versions from version manifest.
        /// </summary>
        /// <returns>List of all <see cref="VersionAudio"/> objects.</returns>
        public static List<VersionAudio> GetAllVersions() {
            List<VersionAudio> versions = [];
            XmlDocument doc = new();
            doc.Load(Path.Combine(Config.InstallPath, "Versions", "VersionManifestAudio.xml"));
            foreach (XmlNode node in doc.GetElementsByTagName("version")) {
                string location = string.Empty;
                double number = 0;

                foreach (XmlNode prop in node.ChildNodes) {
                    switch (prop.Name.ToLower(Program.Culture)) {
                        case "location":
                            location = prop.InnerText;
                            break;
                        case "number":
                            number = Convert.ToDouble(prop.InnerText);
                            break;
                    }
                }

                versions.Add(new VersionAudio(number, location));
            }

            return versions;
        }

        /// <summary>
        /// Gets a version object from its number.
        /// </summary>
        /// <param name="number">The number to parse.</param>
        /// <returns>A version object cooresponding to the specified number if it exists, else null.</returns>
        public static VersionAudio FromNumber(int number) {
            foreach (VersionAudio va in GetAllVersions()) {
                if (va.Number == (double)number) return va;
            }

            return null;
        }

        /// <summary>
        /// Gets the most recent version from the version manifest.
        /// </summary>
        /// <returns>Most recent version object.</returns>
        public static VersionAudio GetMostRecentVersion() {
            VersionAudio mostRecent = null;
            foreach (VersionAudio va in GetAllVersions()) {
                if (mostRecent == null || va.GreaterThan(mostRecent)) mostRecent = va;
            }

            return mostRecent;
        }

        /// <inheritdoc/>
        public bool GreaterThan(IDownloadable other) {
            if (other == null) return true;
            return Number > other.Number;
        }

        /// <inheritdoc/>
        public bool NewerThanDownloaded() {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override string ToString() {
            return $"Audio v{Number}";
        }

        /// <inheritdoc/>
        public override bool Equals(object other) {
            if (other is not VersionAudio) return false;
            if (other is null) return false;
            VersionAudio o = other as VersionAudio;
            if (this == o) return true;
            if (o.ToString().Equals(ToString())) return true;
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
