﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Xml;

namespace HourBoostrJsonHelper
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Private variables
        /// </summary>
        private List<Config.AccountInfo> mAccounts = new List<Config.AccountInfo>();


        /// <summary>
        /// Class constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Adds a new entry
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxUsername.Text) && !string.IsNullOrWhiteSpace(richTextBoxGames.Text))
            {
                List<int> setGames = new List<int>();
                for (int i = 0; i < richTextBoxGames.Lines.Length; i++)
                {
                    int gameId = 0;
                    if (int.TryParse(richTextBoxGames.Lines[i], out gameId))
                    {
                        setGames.Add(gameId);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Unable to parse game {0}.\nMake sure it looks like this example: 500", richTextBoxGames.Lines[i]));
                    }
                }

                var account = new Config.AccountInfo()
                {
                    Username = textBoxUsername.Text,
                    ShowOnlineStatus = checkBoxAppearOnline.Checked,
                    Games = setGames
                };

                mAccounts.Add(account);
                listBoxEntries.Items.Add(account.Username);
                buttonSave.Enabled = true;
                textBoxUsername.Text = string.Empty;
                richTextBoxGames.Text = string.Empty;
            }
        }


        /// <summary>
        /// Saves the list of account to Settings.json
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string jsonFile = Path.Combine(Application.StartupPath, "Settings.json");
            var settings = new Config.Settings();
            settings.Account = mAccounts;
            string jsonString = JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented);

            if (File.Exists(jsonFile))
            {
                DialogResult diagResult = MessageBox.Show("Do you want to overwrite the existing Settings.json", "File Exists", MessageBoxButtons.YesNo);
                if (diagResult == DialogResult.No)
                {
                    return;
                }
            }

            File.WriteAllText(jsonFile, jsonString);
            mAccounts.Clear();
            listBoxEntries.Items.Clear();
            MessageBox.Show("Settings.json saved!");
        }


        /// <summary>
        /// Fetch all games from profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCommunityUrl.Text))
                return;

            listBoxGames.Items.Clear();
            string url = textBoxCommunityUrl.Text;
            if (!url.EndsWith("/"))
                url += "/";

            string xml = Website.DownloadString(string.Format("{0}/games?tab=all&xml=1", url));
            if (xml.Length > 10)
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);

                    XmlNodeList nodeList = xmlDoc.SelectNodes("/gamesList/games/game");
                    foreach (XmlNode node in nodeList)
                    {
                        string appId = node.SelectSingleNode("appID").InnerText;
                        string appName = node.SelectSingleNode("name").InnerText;

                        listBoxGames.Items.Add(string.Format("{0} | {1}", appId, appName));
                    }
                }
                catch (XmlException xEx)
                {
                    MessageBox.Show("Invalid XML response: " + xEx.Message);
                }
            }
        }
    }
}
