using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Version(Channel channel, double number, string location) {
            Channel = channel;
            Number = number;
            Location = location;
        }

        public bool GreaterThan(Version v) {
            if (Channel == v.Channel) return Number > v.Number;
            else return Channel > v.Channel;
        }

        public override string ToString() {
            return Channel.ToString() + " " + Number.ToString();
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
            return new Version(GetChannelFromString(str.Split(' ')[0]), Convert.ToDouble(str.Split(' ')[1]), "");
        }
    }
}
