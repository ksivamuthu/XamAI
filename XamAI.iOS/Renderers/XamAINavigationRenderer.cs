using System;
using UIKit;
using XamAI.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(XamAINavigationRenderer))]
namespace XamAI.iOS.Renderers
{
    public class XamAINavigationRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.BackgroundColor = UIColor.Clear;
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BarTintColor = UIColor.Clear;
            UINavigationBar.Appearance.Translucent = true;
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                Font = UIFont.SystemFontOfSize((nfloat)22f),
                TextColor = UIColor.White
            });
        }
    }
}
