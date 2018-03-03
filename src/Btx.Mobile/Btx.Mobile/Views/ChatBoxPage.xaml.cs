﻿using Btx.Mobile.ViewModels;
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
        protected ChatBoxViewModel ViewModel => BindingContext as ChatBoxViewModel;

        public ChatBoxPage()
        {
            InitializeComponent();
            chatTxtBox.ScrollView = textScroll;

            this.BindingContext = new ChatBoxViewModel();
        }
        
    }
}