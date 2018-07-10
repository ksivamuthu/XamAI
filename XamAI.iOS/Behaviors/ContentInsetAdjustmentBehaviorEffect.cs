using System;
using System.ComponentModel;
using UIKit;
using XamAI.iOS.Behaviors;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("XamAI")]
[assembly: ExportEffect(typeof(ContentInsetAdjustmentBehaviorEffect), "ContentInsetAdjustmentBehavior")]
namespace XamAI.iOS.Behaviors
{
    public class ContentInsetAdjustmentBehaviorEffect : PlatformEffect
    {

        protected override void OnAttached()
        {
            try
            {
                var scroll = Control as UIScrollView;
                scroll.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;
                var inset = (Thickness)Element.GetValue(XamAI.Behaviors.ContentInsetAdjustmentBehavior.ContentInsetProperty);
                scroll.ContentInset = new UIEdgeInsets((nfloat)inset.Top, (nfloat)inset.Left, (nfloat)inset.Bottom, (nfloat)inset.Right);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if(args.PropertyName == "ContentInset")
                {
                    var scroll = Control as UIScrollView;
                    var inset = (Thickness)Element.GetValue(XamAI.Behaviors.ContentInsetAdjustmentBehavior.ContentInsetProperty);
                    scroll.ContentInset = new UIEdgeInsets((nfloat)inset.Top, (nfloat)inset.Left, (nfloat)inset.Bottom, (nfloat)inset.Right);

                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}