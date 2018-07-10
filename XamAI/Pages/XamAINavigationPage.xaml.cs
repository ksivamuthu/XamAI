using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamAI.Pages
{
    public partial class XamAINavigationPage : NavigationPage
    {
        public XamAINavigationPage()
        {
            InitializeComponent();
        }

        public XamAINavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if(!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}