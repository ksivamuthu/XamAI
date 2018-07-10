using System;
using Xamarin.Forms;

namespace XamAI.Behaviors
{
    public static class ContentInsetAdjustmentBehavior
    {
        public static readonly BindableProperty ContentInsetProperty =
            BindableProperty.CreateAttached("ContentInset", typeof(Thickness), typeof(ContentInsetAdjustmentBehaviorEffect), new Thickness(0));

        public static Thickness GetContentInset(BindableObject view)
        {
            return (Thickness)view.GetValue(ContentInsetProperty);
        }
    }

    public class ContentInsetAdjustmentBehaviorEffect : RoutingEffect
    {
        static readonly string RoutingId = "XamAI.ContentInsetAdjustmentBehavior";
        public ContentInsetAdjustmentBehaviorEffect() : base(RoutingId)
        {

        }
    }
}
