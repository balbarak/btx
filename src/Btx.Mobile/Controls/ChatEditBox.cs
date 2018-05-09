using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
namespace Btx.Mobile.Controls
{
    public class ChatEditBox : Editor
    {
        public ScrollView ScrollView { get; set; }

        public ChatEditBox()
        {
            TextChanged += OnTextChanged;
        }

        ~ChatEditBox()
        {
            TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Editor editor = (Editor)sender;

                if (this.Height > 200)
                    this.ScrollView.HeightRequest = 200;
                else
                    this.ScrollView.HeightRequest = -1;

                if (Text.Length == 0)
                    this.ScrollView.HeightRequest = -1;

                this.InvalidateMeasure();
            }

        }
    }
}
