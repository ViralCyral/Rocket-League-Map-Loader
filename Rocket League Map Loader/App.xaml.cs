﻿using System;
using System.Configuration;
using System.Net;
using System.Windows;
using AutoUpdaterDotNET;
using static RL_Map_Loader.Properties.Settings;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace RL_Map_Loader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string AutoUpdateUrl = "https://pastebin.com/raw/tyKGSU4F";

        protected override void OnStartup(StartupEventArgs e)
        {
#if !DEBUG
            try
            {
#endif
                base.OnStartup(e);

                AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
                AutoUpdater.Start(AutoUpdateUrl);
                RunMainApp();
#if !DEBUG
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
#endif
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            switch(args.Error)
            {
                case null when args.IsUpdateAvailable:
                {
                    MessageBoxResult messageBoxResult;

                    if (args.Mandatory.Value)
                    {
                        messageBoxResult =
                            MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. This is required update. Press Ok to begin updating the application.", @"Update Available",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                    }
                    else
                    {
                        messageBoxResult =
                            MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {
                                        args.InstalledVersion
                                    }. Do you want to update the application now?", @"Update available",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Information);
                    }

                    if (messageBoxResult.Equals(MessageBoxResult.Yes) || messageBoxResult.Equals(MessageBoxResult.OK))
                    {
                        try
                        {
                            if (AutoUpdater.DownloadUpdate(args)) 
                                Current.Shutdown();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }

                    break;
                }

                case null:
                    break;

                case WebException _:
                    MessageBox.Show(
                        @"There is a problem reaching update server. Please check your internet connection and try again later.",
                        @"Update check failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                default:
                    MessageBox.Show(args.Error.Message,
                        args.Error.GetType().ToString(), MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    break;
            }
        }

        private void TryLoadPreviousSettings()
        {
            AppState.Settings.Upgrade();

            foreach (SettingsProperty property in AppState.Settings.Properties)
            {
                try
                {
                    var previousValue = Default.GetPreviousVersion(property.Name);

                    if (previousValue != null)
                        AppState.Settings[property.Name] = previousValue;
                }
                catch
                {
                    //Ignore - user may not have previous settings
                }
            }
        }

        private void RunMainApp()
        {
            TryLoadPreviousSettings();
            AppState.Settings.Save();

            var isFirstTimeRun = Default.IsFirstTimeRun;

            StartupUri = isFirstTimeRun
                ? new Uri("Setup.xaml", UriKind.Relative)
                : new Uri("MainWindow.xaml", UriKind.Relative);

            if (!isFirstTimeRun)
                LoadMaps();
        }

        public static void LoadMaps()
        {
            AppState.RefreshDownloadedMaps();
            AppState.RefreshLethsMaps();

            if (AppState.RocketLeagueDirectoryIsSteam)
                AppState.RefreshWorkshopMaps();

            AppState.RefreshCommunityMaps();
            AppState.UpdateCurrentlyLoadedMap();
        }
    }
}
