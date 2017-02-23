﻿
using Xamarin.Forms;

namespace Prestart.View
{
    public class PrestartNavigationPage : NavigationPage
    {
        public PrestartNavigationPage(Page root) : base(root)
        {
            Init();
        }

        public PrestartNavigationPage()
        {
            Init();
        }

        void Init()
        {

            BarBackgroundColor = Color.FromHex("#0099C7");
            BarTextColor = Color.White;
        }
    }
}
