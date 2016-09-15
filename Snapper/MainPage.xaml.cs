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
using Snapper.FlickrAPI;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Hosting;
using System.Numerics;
using Microsoft.Graphics.Canvas.Effects;
using Windows.UI.Composition;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.ApplicationModel.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Snapper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            string family = AnalyticsInfo.VersionInfo.DeviceFamily;

            var appView = ApplicationView.GetForCurrentView();
            var titleBar = appView.TitleBar;

            if (family.Contains("Desktop"))
            {
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonForegroundColor = Colors.WhiteSmoke;
                titleBar.ButtonHoverBackgroundColor = Colors.Transparent;
                titleBar.ButtonHoverForegroundColor = Colors.DimGray;
                titleBar.ButtonPressedBackgroundColor = Colors.Transparent;
                titleBar.ButtonPressedForegroundColor = Colors.DimGray;
                titleBar.InactiveBackgroundColor = Colors.Transparent;
                titleBar.InactiveForegroundColor = Colors.WhiteSmoke;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveForegroundColor = Colors.WhiteSmoke;
                CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                coreTitleBar.ExtendViewIntoTitleBar = true;
            }
            else if (family.Contains("Mobile"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.HideAsync();
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var elements = await Extensions.getTrendingImages(1);
            var item = elements.First();
            BackgroundImage.Source = new BitmapImage(new Uri(item.image_url));
            Title.Text = item.title;
            Author.Text = "by " + item.author;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var gridVisual = ElementCompositionPreview.GetElementVisual(BackgroundImage);

            var compositor = gridVisual.Compositor;

            var effectVisual = compositor.CreateSpriteVisual();

            effectVisual.Size = new Vector2(
              (float)10000,
              (float)10000);

            GaussianBlurEffect blurEffect = new GaussianBlurEffect()
            {
                BorderMode = EffectBorderMode.Hard,
                BlurAmount = 30f,
                Source = new CompositionEffectSourceParameter("source")
            };

            var effectFactory = compositor.CreateEffectFactory(blurEffect);
            var effectBrush = effectFactory.CreateBrush();
            effectBrush.SetSourceParameter("source", compositor.CreateBackdropBrush());

            effectVisual.Brush = effectBrush;

            ElementCompositionPreview.SetElementChildVisual(BackgroundImage, effectVisual);
        }
    }
}
