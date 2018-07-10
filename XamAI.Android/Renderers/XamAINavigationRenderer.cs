using System;
using Android.Content;
using XamAI.Droid.Renderers;
using XamAI.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AView = Android.Views.View;
using Android.Support.V7.Widget;


[assembly: ExportRenderer(typeof(NavigationPage), typeof(XamAINavigationRenderer))]
namespace XamAI.Droid.Renderers
{
    public class XamAINavigationRenderer : NavigationPageRenderer
    {
        public XamAINavigationRenderer(Context context) : base(context) { }

        IPageController PageController => Element as IPageController;
        XamAINavigationPage XamAINavigationPage => Element as XamAINavigationPage;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            XamAINavigationPage.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            XamAINavigationPage.IgnoreLayoutChange = false;

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for(var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if(child is Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}
