// <copyright file="GithubBridge.cs" company="Baphomet Media">
// © Copyright by Baphomet Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;

namespace ApexLauncher {
    /// <summary>
    /// Static class for managing GitHub lookups.
    /// </summary>
    public static class GithubBridge {
        /// <summary>
        /// Check for a new launcher version on GitHub.
        /// </summary>
        /// <returns>True if a new launcher version is available.</returns>
        public static bool CheckForLauncherUpdate() {
            using HttpClient client = new(new HttpClientHandler() { UseDefaultCredentials = true });
            client.DefaultRequestHeaders.Add("User-Agent", "Other");
            try {
                // client.UseDefaultCredentials = true;
                // client.Headers.Add("User-Agent: Other");
                string text = client.GetStringAsync("https://api.github.com/repos/baph-omet/Apex-Launcher/releases/latest").Result;

                JsonDocument json = JsonDocument.Parse(text);
                string tag = json.RootElement.GetProperty("tag_name").GetString().Replace("v", string.Empty);
                int[] latestVersion = [
                    Convert.ToInt32(tag.Split('.')[0], Program.Culture),
                        Convert.ToInt32(tag.Split('.')[1], Program.Culture),
                        Convert.ToInt32(tag.Split('.')[2], Program.Culture),
                    ];
                Version launcherVersion = Assembly.GetExecutingAssembly().GetName().Version;
                if (latestVersion[0] > launcherVersion.Major ||
                    (latestVersion[0] == launcherVersion.Major && latestVersion[1] > launcherVersion.Minor) ||
                    (latestVersion[0] == launcherVersion.Major && latestVersion[1] == launcherVersion.Minor && latestVersion[2] > launcherVersion.Build)) {
                    if (MessageBox.Show(
                        "New Launcher Version is available: v" + tag + ". Would you like to download it now?",
                        "New Launcher Version",
                        MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        string downloadUrl = json.RootElement.GetProperty("assets")[0].GetProperty("browser_download_url").GetString();
                        using Stream stream = client.GetStreamAsync(downloadUrl).Result;
                        string fileName = $"ApexLauncherv{tag}.exe";
                        using FileStream fileStream = File.Create(fileName);
                        stream.CopyTo(fileStream);
                        fileStream.Close();
                        stream.Close();
                        MessageBox.Show($"New version downloaded to {fileName}. Please replace the current version and restart.");
                        return true;
                    }
                }
            } catch (WebException) {
                MessageBox.Show("Cannot check for new launcher versions. Check your internet connection and try again.");
            }

            return false;
        }
    }
}
