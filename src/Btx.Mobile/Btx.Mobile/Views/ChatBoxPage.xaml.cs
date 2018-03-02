using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Btx.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatBoxPage : ContentPage
    {
        public ChatBoxPage()
        {
            InitializeComponent();
            chatTxtBox.ScrollView = textScroll;
            
        }

        private void TextScroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            var scroll = (ScrollView)sender;

            Debug.WriteLine($"Content height: {scroll.ContentSize}");
        }

        private void TextScroll_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}