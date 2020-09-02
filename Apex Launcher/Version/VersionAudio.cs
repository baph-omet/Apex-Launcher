﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Apex_Launcher {
    public class VersionAudio : IDownloadable {
        public string Location { get; }

        public double Number { get; }

        public IDownloadable Prerequisite { get; } = null;

        public VersionAudio(double number, string location) {
            Location = location;
            Number = number;
        }

        public bool GreaterThan(IDownloadable other) {
            return Number > other.Number;
        }

        public bool NewerThanDownloaded() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "Audio v" + Number.ToString();
        }

        public static List<VersionAudio> GetAllVersions() {
            List<VersionAudio> versions = new List<VersionAudio>();
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(Program.GetInstallPath(), "Versions\\VersionManifestAudio.xml"));
            foreach (XmlNode node in doc.GetElementsByTagName("version")) {
                string location = string.Empty;
                double number = 0;

                foreach (XmlNode prop in node.ChildNodes) {
                    switch (prop.Name.ToLower()) {
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

        public static VersionAudio FromNumber(int number) {
            foreach (VersionAudio va in GetAllVersions()) {
                if (va.Number == (double)number) return va;
            }
            return null;
        }
    }
}
