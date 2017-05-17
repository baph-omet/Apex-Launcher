using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Apex_Launcher {
    public class GithubBridge {
        public static bool CheckForLauncherUpdate() {
            using (WebClient wc = new WebClient()) {
                try {
                    wc.UseDefaultCredentials = true;
                    wc.Headers.Add("User-Agent: Other");
                    string text = wc.DownloadString("https://api.github.com/repos/griffenx/Apex-Launcher/releases/latest");

                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Dictionary<string, object> json = jss.Deserialize<Dictionary<string,object>>(text);
                    string tag = json["tag_name"].ToString().Replace("v", "");
                    int[] latestVersion = new[] {
                        Convert.ToInt32(tag.Split('.')[0],Program.Culture),
                        Convert.ToInt32(tag.Split('.')[1],Program.Culture),
                        Convert.ToInt32(tag.Split('.')[2],Program.Culture)
                    };
                    System.Version launcherVersion = Assembly.GetExecutingAssembly().GetName().Version;
                    if (latestVersion[0] > launcherVersion.Major ||
                        (latestVersion[0] == launcherVersion.Major && latestVersion[1] > launcherVersion.Minor) ||
                        (latestVersion[0] == launcherVersion.Major && latestVersion[1] == launcherVersion.Minor && latestVersion[2] > launcherVersion.Revision)
                        ) {
                        if (MessageBox.Show("New Launcher Version is available: v" + tag + ". Would you like to download it now?",
                            "New Launcher Version", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                            Process.Start(json["html_url"].ToString());
                            return true;
                        }
                    }
                } catch(WebException) {
                    MessageBox.Show("Cannot check for new launcher versions. Check your internet connection and try again.");
                }
            }
            return false;
        }
    }
}
