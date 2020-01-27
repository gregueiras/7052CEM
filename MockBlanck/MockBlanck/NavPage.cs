using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MockBlanck
{
    class NavPage : NavigationPage
    {
        private void init()
        {
            SetHasNavigationBar(this, false);
            SetHasBackButton(this, false);
        }

        public NavPage() : base()
        {
            init();
        }

        public NavPage(Page root) : base(root)
        {
            init();
        }
    }
}
