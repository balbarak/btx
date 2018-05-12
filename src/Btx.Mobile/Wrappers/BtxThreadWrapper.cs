﻿using Btx.Client.Domain.Models;
using Btx.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Btx.Mobile.Wrappers
{
    public class BtxThreadWrapper : WrapperBase<BtxThread>
    {
        public string Id
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Title
        {
            get
            {
                return GetValue<string>();
            }
            set
            {
                SetValue(value);
            }
        }

        private string _lastMessage;

        public string LastMessage
        {
            get { return _lastMessage; }
            set { _lastMessage = value; OnPropertyChanged(); }
        }

        private bool _hasUnreadMessages;

        public bool HasUnreadMessages
        {
            get { return _hasUnreadMessages; }
            set { _hasUnreadMessages = value; OnPropertyChanged(); }
        }

        private int _unReadMessageCount;

        public int UnreadMessageCount
        {
            get { return _unReadMessageCount; }
            set { _unReadMessageCount = value; OnPropertyChanged();}
        }


        public BtxThreadWrapper(BtxThread model) : base(model)
        {

        }
    }
}