using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Snapper.API;
using Windows.UI;
using Windows.UI.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Snapper.Views.Frames {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LandingPage : Page {
        public LandingPage() {
            this.InitializeComponent();

            TransitionCollection collection = new TransitionCollection();
            NavigationThemeTransition theme = new NavigationThemeTransition();

            var info = new ContinuumNavigationTransitionInfo();

            theme.DefaultNavigationTransitionInfo = info;
            collection.Add(theme);
            this.Transitions = collection;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            string imgUrl = Extensions.getRandomLandingImage();

            BackgroundImage.Source = new BitmapImage(new Uri(imgUrl, UriKind.Absolute));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e) {
            var gridVisual = ElementCompositionPreview.GetElementVisual(TitleBlur);

            var compositor = gridVisual.Compositor;

            var effectVisual = compositor.CreateSpriteVisual();

            effectVisual.Size = new Vector2(
              (float)10000,
              (float)110);

            GaussianBlurEffect blurEffect = new GaussianBlurEffect()
            {
                BorderMode = EffectBorderMode.Hard,
                BlurAmount = 50f,
                Source = new CompositionEffectSourceParameter("source")
            };

            var effectFactory = compositor.CreateEffectFactory(blurEffect);
            var effectBrush = effectFactory.CreateBrush();
            effectBrush.SetSourceParameter("source", compositor.CreateBackdropBrush());

            effectVisual.Brush = effectBrush;

            ElementCompositionPreview.SetElementChildVisual(TitleBlur, effectVisual);

            var elements = await Snapper.API.Extensions.getTrendingImages(10);

            foreach (FlickrImage item in elements)
            {
                var Img = new Image();
                Img.Source = new BitmapImage(new Uri(item.image_url));
                Img.VerticalAlignment = VerticalAlignment.Center;
                Img.Stretch = Stretch.UniformToFill;

                var Title = new TextBlock();
                Title.Text = item.title.ToUpper();
                Title.Foreground = new SolidColorBrush() { Color = Colors.White, Opacity = 1 };
                Title.FontSize = 25;
                Title.FontFamily = new FontFamily("Segoe UI");
                Title.FontWeight = FontWeights.SemiBold;
                Title.CharacterSpacing = 50;
                Title.Margin = new Thickness(15, 10, 15, 0);

                var Author = new TextBlock();
                Author.Text = "by " + item.author;
                Author.Foreground = new SolidColorBrush() { Color = Colors.White, Opacity = 0.8 };
                Author.FontSize = 16;
                Author.FontFamily = new FontFamily("Segoe UI");
                Author.FontWeight = FontWeights.SemiLight;
                Author.Margin = new Thickness(15, 0, 15, 0);

                var pivotItem = new PivotItem();
                pivotItem.Margin = new Thickness(0, -48, 0, 0);

                var st = new StackPanel();
                st.Children.Add(Title);
                st.Children.Add(Author);

                var bg = new Grid();

                var g = new Grid();
                g.Children.Add(bg);
                g.Children.Add(st);
                g.VerticalAlignment = VerticalAlignment.Bottom;
                g.HorizontalAlignment = HorizontalAlignment.Left;
                g.Margin = new Thickness(0, 0, 0, 35);

                var gr = new Grid();
                gr.Children.Add(Img);
                gr.Children.Add(g);

                pivotItem.Content = gr;

                pv.Items.Add(pivotItem);

                var gridVisual2 = ElementCompositionPreview.GetElementVisual(bg);

                var compositor2 = gridVisual2.Compositor;

                var effectVisual2 = compositor2.CreateSpriteVisual();

                effectVisual2.Size = new Vector2(
                  (float)10000,
                  (float)80);

                GaussianBlurEffect blurEffect2 = new GaussianBlurEffect()
                {
                    BorderMode = EffectBorderMode.Hard,
                    BlurAmount = 50f,
                    Source = new CompositionEffectSourceParameter("source2")
                };

                var effectFactory2 = compositor2.CreateEffectFactory(blurEffect2);
                var effectBrush2 = effectFactory2.CreateBrush();
                effectBrush2.SetSourceParameter("source2", compositor2.CreateBackdropBrush());

                effectVisual2.Brush = effectBrush2;

                ElementCompositionPreview.SetElementChildVisual(bg, effectVisual2);

            }
        }
    }
}
