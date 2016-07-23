﻿using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HourBoostr
{
    public class Config
    {
        /// <summary>
        /// Class for accounts
        /// </summary>
        public class Settings
        {
            /// <summary>
            /// If we should automatically hide the program to tray bar
            /// I fucking hate this so I want to disable it
            /// </summary>
            public bool HideToTrayAutomatically { get; set; } = true;


            /// <summary>
            /// List of accounts
            /// </summary>
            public List<AccountSettings> Account { get; set; } = new List<AccountSettings>();
        }

        
        /// <summary>
        /// Class for account settings
        /// </summary>
        public class AccountSettings
        {
            /// <summary>
            /// Username of steam account
            /// </summary>
            public string Username { get; set; } = "";


            /// <summary>
            /// Password of the steam account
            /// This will be set manually by user from console
            /// This will not be printed or read from json file
            /// </summary>
            public string Password { get; set; } = "";


            /// <summary>
            /// Login key for steam account
            /// Saving this means we don't have to enter code twice
            /// </summary>
            public string LoginKey { get; set; } = "";


            /// <summary>
            /// If we should set steam status to online
            /// </summary>
            public bool ShowOnlineStatus { get; set; }


            /// <summary>
            /// If we should restart games every 3 hours
            /// This is to prevent steam to no longer get hours if we idle for a long period of time
            /// </summary>
            public bool RestartGamesEveryThreeHours { get; set; } = true;


            /// <summary>
            /// What the bot should reply if someone sends it a message
            /// </summary>
            public string ChatResponse { get; set; } = "";


            /// <summary>
            /// List of games we should set as playing
            /// </summary>
            public List<int> Games { get; set; } = new List<int>();
        }
    }
}
