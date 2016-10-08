using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Hosting;
using System.Numerics;
using Microsoft.Graphics.Canvas.Effects;
using Windows.UI.Composition;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Media.Animation;
using Snapper.Views.Frames;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Snapper.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();

            string family = AnalyticsInfo.VersionInfo.DeviceFamily;

            var appView = ApplicationView.GetForCurrentView();
            var titleBar = appView.TitleBar;

            if (family.Contains("Desktop"))
            {
                CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                coreTitleBar.ExtendViewIntoTitleBar = false;
            }
            else if (family.Contains("Mobile"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.ShowAsync();
            }

            TransitionCollection collection = new TransitionCollection();
            NavigationThemeTransition theme = new NavigationThemeTransition();

            var info = new EntranceNavigationTransitionInfo();

            theme.DefaultNavigationTransitionInfo = info;
            collection.Add(theme);
            this.Transitions = collection;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            mainFrame.Navigate(typeof(LandingPage));
        }
    }
}
