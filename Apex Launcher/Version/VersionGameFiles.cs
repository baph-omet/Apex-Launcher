﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace Apex_Launcher {
    public enum Channel {
        NONE,
        ALPHA,
        BETA,
        RELEASE
    };

    public class VersionGameFiles : IDownloadable {
        public Channel Channel { get; }
        public double Number { get; }
        public string Location { get; }
        public bool IsPatch { get; }

        public VersionAudio MinimumAudioVersion { get; }

        private VersionGameFiles prereq = null;
        public IDownloadable Prerequisite {
            get {
                if (!IsPatch) return null;
                if (prereq == null) prereq = GetPreviousVersion(true);
                return prereq;
            }
        }

        public VersionGameFiles(Channel channel, double number, string location, VersionAudio audioVersion, bool ispatch = false) {
            Channel = channel;
            Number = number;
            Location = location;
            IsPatch = ispatch;
            MinimumAudioVersion = audioVersion;
        }

        public bool GreaterThan(IDownloadable other) {
            if (other == null) return true;
            VersionGameFiles v = other as VersionGameFiles;
            if (Channel == v.Channel) return Number > v.Number;
            else return Channel > v.Channel;
        }

        public VersionGameFiles GetPreviousVersion() {
            return GetPreviousVersion(false);
        }
        public VersionGameFiles GetPreviousVersion(bool FullVersionOnly) {
            VersionGameFiles mostRecentPrevious = null;
            foreach (VersionGameFiles v in GetAllVersions())
                if (GreaterThan(v)
                    && (FullVersionOnly && !v.IsPatch) || !FullVersionOnly
                    && (mostRecentPrevious == null || v.GreaterThan(mostRecentPrevious)))
                    mostRecentPrevious = v;
            return mostRecentPrevious;
        }

        public override string ToString() {
            return Channel.ToString() + " " + Number.ToString() + (IsPatch ? "p" : "");
        }

        public bool NewerThanDownloaded() {
            return GreaterThan(Config.CurrentVersion);
        }

        public static Channel GetChannelFromString(string name) {
            switch (name.ToLower()[0]) {
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

        public static VersionGameFiles FromString(string str) {
            foreach (VersionGameFiles vgf in GetAllVersions()) {
                if (vgf.ToString() == str) return vgf;
            }
            return null;
        }

        public static List<VersionGameFiles> GetAllVersions() {
            List<VersionGameFiles> versions = new List<VersionGameFiles>();

            XmlDocument doc = new XmlDocument();
            doc.Load(Config.InstallPath + "\\Versions\\VersionManifest.xml");

            foreach (XmlNode node in doc.GetElementsByTagName("version")) {
                Channel channel = Channel.NONE;
                double number = 0.0;
                string location = "";
                bool patch = false;
                VersionAudio audioversion = null;

                foreach (XmlNode prop in node.ChildNodes) {
                    switch (prop.Name.ToLower()) {
                        case "channel":
                            channel = (prop.InnerText[0]) switch
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

        public static VersionGameFiles GetMostRecentVersion() {
            VersionGameFiles mostRecent = Config.CurrentVersion;
            foreach (VersionGameFiles v in GetAllVersions()) if (mostRecent == null || v.GreaterThan(mostRecent) || v.Equals(mostRecent)) mostRecent = v;
            return mostRecent;
        }

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
