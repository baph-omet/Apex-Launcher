// <copyright file="VersionGameFiles.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ApexLauncher {
    /// <summary>
    /// Enum for encapsulating release channels.
    /// </summary>
    public enum Channel {
        /// <summary>
        /// Invalid channel.
        /// </summary>
        NONE,

        /// <summary>
        /// Alpha channel.
        /// </summary>
        ALPHA,

        /// <summary>
        /// Beta channel.
        /// </summary>
        BETA,

        /// <summary>
        /// Release channel.
        /// </summary>
        RELEASE,
    }

    /// <summary>
    /// Object representing a version of the game files.
    /// </summary>
    public class VersionGameFiles : IDownloadable {
        private VersionGameFiles prereq;

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionGameFiles"/> class.
        /// </summary>
        /// <param name="channel">Release channel.</param>
        /// <param name="number">Version number.</param>
        /// <param name="location">Remote download location.</param>
        /// <param name="audioVersion">Audio version.</param>
        /// <param name="ispatch">Whether this version is a patch only.</param>
        public VersionGameFiles(Channel channel, double number, string location, VersionAudio audioVersion, bool ispatch = false) {
            Channel = channel;
            Number = number;
            Location = location;
            IsPatch = ispatch;
            MinimumAudioVersion = audioVersion;
        }

        /// <summary>
        /// Gets release channel.
        /// </summary>
        public Channel Channel { get; }

        /// <inheritdoc/>
        public double Number { get; }

        /// <inheritdoc/>
        public string Location { get; }

        /// <summary>
        /// Gets a value indicating whether this version is a patch only.
        /// </summary>
        public bool IsPatch { get; }

        /// <summary>
        /// Gets the minimum audio version needed for this game version.
        /// </summary>
        public VersionAudio MinimumAudioVersion { get; }

        /// <inheritdoc/>
        public IDownloadable Prerequisite {
            get {
                if (!IsPatch) return null;
                if (prereq == null) prereq = GetPreviousVersion(true);
                return prereq;
            }
        }

        /// <summary>
        /// Get channel enum from string.
        /// </summary>
        /// <param name="name">The string to get.</param>
        /// <returns>A <see cref="Channel"/> enum if found, else <see cref="Channel.NONE"/>.</returns>
        public static Channel GetChannelFromString(string name) {
            if (name is null) throw new ArgumentNullException(nameof(name));
            switch (name.ToLower(Program.Culture)[0]) {
                case '1':
                case 'a':
                    return Channel.ALPHA;
                case '2':
                case 'b':
                    return Channel.BETA;
                case '3':
                case 'r':
                    return Channel.RELEASE;
                default:
                    return Channel.NONE;
            }
        }

        /// <summary>
        /// Gets a version instance from a string representation. If the Version Manifest is found, gets the full details, otherwise just a basic object.
        /// </summary>
        /// <param name="str">String to parse.</param>
        /// <returns>A version instance parsed from string.</returns>
        public static VersionGameFiles FromString(string str) {
            if (str is null) throw new ArgumentNullException(nameof(str));
            if (!File.Exists(Path.Combine(Config.InstallPath, "Versions", "VersionManifest.xml"))) {
                string[] split = str.Split(' ');
                Channel channel = split[0] switch
                {
                    "ALPHA" => Channel.ALPHA,
                    "BETA" => Channel.BETA,
                    "RELEASE" => Channel.RELEASE,
                    _ => Channel.NONE
                };

                bool patch = false;
                double number = 0.0;
                try {
                    if (str.EndsWith("p")) {
                        patch = true;
                        number = Convert.ToDouble(split[1].Substring(0, split[1].Length - 2));
                    } else number = Convert.ToDouble(split[1]);
                } catch (FormatException) { }

                return new VersionGameFiles(channel, number, string.Empty, null, patch);
            }

            foreach (VersionGameFiles vgf in GetAllVersions()) if (vgf.ToString() == str) return vgf;
            return null;
        }

        /// <summary>
        /// Gets all versions from the Version Manifest.
        /// </summary>
        /// <returns>List of all version objects.</returns>
        public static List<VersionGameFiles> GetAllVersions() {
            List<VersionGameFiles> versions = new List<VersionGameFiles>();

            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(Config.InstallPath, "Versions", "VersionManifest.xml"));

            foreach (XmlNode node in doc.GetElementsByTagName("version")) {
                Channel channel = Channel.NONE;
                double number = 0.0;
                string location = string.Empty;
                bool patch = false;
                VersionAudio audioversion = null;

                foreach (XmlNode prop in node.ChildNodes) {
                    switch (prop.Name.ToLower(Program.Culture)) {
                        case "channel":
                            channel = prop.InnerText[0] switch
                            {
                                'a' => Channel.ALPHA,
                                'b' => Channel.BETA,
                                'r' => Channel.RELEASE,
                                _ => Channel.NONE,
                            };
                            break;
                        case "number":
                            try {
                                number = Convert.ToDouble(prop.InnerText, Program.Culture);
                            } catch (FormatException) {
                                number = 0.0;
                            }

                            break;
                        case "location":
                            location = prop.InnerText;
                            break;
                        case "patch":
                            patch = Convert.ToBoolean(prop.InnerText, Program.Culture);
                            break;
                        case "audioversion":
                            audioversion = VersionAudio.FromNumber(Convert.ToInt32(prop.InnerText));
                            break;
                    }
                }

                versions.Add(new VersionGameFiles(channel, number, location, audioversion, patch));
            }

            return versions;
        }

        /// <summary>
        /// Gets most recent version of game files.
        /// </summary>
        /// <returns>The most recent version object.</returns>
        public static VersionGameFiles GetMostRecentVersion() {
            VersionGameFiles mostRecent = Config.CurrentVersion;
            foreach (VersionGameFiles v in GetAllVersions()) if (mostRecent == null || v.GreaterThan(mostRecent) || v.Equals(mostRecent)) mostRecent = v;
            return mostRecent;
        }

        /// <inheritdoc/>
        public bool GreaterThan(IDownloadable other) {
            if (other == null) return true;
            VersionGameFiles v = other as VersionGameFiles;
            if (Channel == v.Channel) return Number > v.Number;
            else return Channel > v.Channel;
        }

        /// <summary>
        /// Gets the version before this one.
        /// </summary>
        /// <param name="fullVersionOnly">If true, only checks full versions and not patches.</param>
        /// <returns>A version of the game before this one, if found, else null.</returns>
        public VersionGameFiles GetPreviousVersion(bool fullVersionOnly = false) {
            VersionGameFiles mostRecentPrevious = null;
            foreach (VersionGameFiles v in GetAllVersions()) {
                if ((GreaterThan(v) && fullVersionOnly && !v.IsPatch) || (!fullVersionOnly && (mostRecentPrevious == null || v.GreaterThan(mostRecentPrevious)))) {
                    mostRecentPrevious = v;
                }
            }

            return mostRecentPrevious;
        }

        /// <inheritdoc/>
        public override string ToString() {
            return Channel.ToString() + " " + Number.ToString() + (IsPatch ? "p" : string.Empty);
        }

        /// <inheritdoc/>
        public bool NewerThanDownloaded() {
            return GreaterThan(Config.CurrentVersion);
        }

        /// <inheritdoc/>
        public new bool Equals(object obj) {
            if (this == null) return false;
            if (obj == null) return false;
            if (!GetType().Equals(obj.GetType())) return false;
            if (obj == this) return true;
            if (!(obj is VersionGameFiles)) return false;
            if (((VersionGameFiles)obj).ToString().Equals(ToString())) return true;
            return false;
        }
    }
}
