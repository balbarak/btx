using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Btx.Mobile.CustomRenders
{
    public class ChatEditBox : Editor
    {
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
            InvalidateMeasure();
        }
    }
}
