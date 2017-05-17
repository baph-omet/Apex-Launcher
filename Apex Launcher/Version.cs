using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Apex_Launcher {
    public enum Channel {
        NONE,
        ALPHA,
        BETA,
        RELEASE
    };

    public class Version {
        public Channel Channel { get; set; }
        public double Number { get; set; }
        public string Location { get; set; }
        private bool ispatch;
        public bool IsPatch { get { return ispatch; } }
        private Version prereq = null;
        public Version Prerequisite {
            get {
                if (!ispatch) return null;
                if (prereq == null) prereq = GetPreviousVersion(true);
                return prereq;
            }
        }

        public Version(Channel channel, double number, string location) : this(channel,number,location,false) { }
        public Version(Channel channel, double number, string location, bool ispatch) {
            Channel = channel;
            Number = number;
            Location = location;
            this.ispatch = ispatch;
        }

        public bool GreaterThan(Version v) {
            if (Channel == v.Channel) return Number > v.Number;
            else return Channel > v.Channel;
        }

        public Version GetPreviousVersion() {
            return GetPreviousVersion(false);
        }
        public Version GetPreviousVersion(bool FullVersionOnly) {
            Version mostRecentPrevious = null;
            foreach (Version v in GetAllVersions())
                if (GreaterThan(v) 
                    && (FullVersionOnly && !v.IsPatch) || !FullVersionOnly  
                    && (mostRecentPrevious == null || v.GreaterThan(mostRecentPrevious)))
                    mostRecentPrevious = v;
            return mostRecentPrevious;
        }

        public override string ToString() {
            return Channel.ToString() + " " + Number.ToString() + (ispatch ? "p" : "");
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

        public static Version FromString(string str) {
            return new Version(
                GetChannelFromString(str.Split(' ')[0]),
                str.Last() != 'p' ? Convert.ToDouble(str.Split(' ')[1],Program.Culture) : Convert.ToDouble(str.Split(' ')[1].Replace('p','\0'), Program.Culture),
                "",
                str[str.Length - 1] == 'p');
        }

        public static List<Version> GetAllVersions() {
            List<Version> versions = new List<Version>();

            XmlDocument doc = new XmlDocument();
            doc.Load(Program.GetInstallPath() + "\\Versions\\VersionManifest.xml");

            foreach (XmlNode node in doc.GetElementsByTagName("version")) {
                Channel channel = Channel.NONE;
                double number = 0.0;
                string location = "";
                bool patch = false;

                foreach (XmlNode prop in node.ChildNodes) {
                    switch (prop.Name.ToLower()) {
                        case "channel":
                            switch (prop.InnerText[0]) {
                                case 'a':
                                    channel = Channel.ALPHA;
                                    break;
                                case 'b':
                                    channel = Channel.BETA;
                                    break;
                                case 'r':
                                    channel = Channel.RELEASE;
                                    break;
                                default:
                                    channel = Channel.NONE;
                                    break;
                            }
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
                    }
                }
                versions.Add(new Version(channel, number, location, patch));
            }
            return versions;
        }

        public static Version GetMostRecentVersion() {
            Version mostRecent = Program.GetCurrentVersion();
            foreach (Version v in GetAllVersions()) if (mostRecent == null || v.GreaterThan(mostRecent) || v.Equals(mostRecent)) mostRecent = v;
            return mostRecent;
        }

        public new bool Equals(object obj) {
            if (this == null) return false;
            if (obj == null) return false;
            if (!GetType().Equals(obj.GetType())) return false;
            if (obj == this) return true;
            if (!(obj is Version)) return false;
            if (((Version)obj).ToString().Equals(ToString())) return true;
            return false;
        }
    }
}
