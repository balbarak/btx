using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
namespace Btx.Mobile.CustomRenders
{
    public class ChatEditBox : Editor
    {
        public ScrollView ScrollView { get; set; }

        bool sized = false;
        public double lineHeight = 0;

        public ChatEditBox()
        {
            TextChanged += OnTextChanged;
        }

        ~ChatEditBox()
        {
            TextChanged -= OnTextChanged;
        }


        protected override void OnSizeAllocated(double width, double height)
        {
            
            if (!sized && !String.IsNullOrWhiteSpace(Text))
            {
                int count = Text.Count(c => c == '\n');
                lineHeight = (height / (count + 1));
                sized = true;
            }

            base.OnSizeAllocated(width, height);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //Editor editor = (Editor)sender;

            //if (this.Height > 200)
            //    this.ScrollView.HeightRequest = 200;
            //else
            //    this.ScrollView.HeightRequest = -1;

            //if (Text.Length == 0)
            //    this.ScrollView.HeightRequest = -1;

            //this.HeightRequest = 200;
            //InvalidateMeasure();

        }
    }
}
