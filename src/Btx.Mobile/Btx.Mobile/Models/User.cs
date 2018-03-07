using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Models
{
    public class User : ObservableObject
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string userName;
        public string Username
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        private string nickName;

        public string Nickname
        {
            get { return nickName; }
            set { nickName = value; OnPropertyChanged(); }
        }


        public DateTime? RegisteredDate { get; set; }

        private DateTime? lastSeen;
        public DateTime? LastSeen
        {
            get { return lastSeen; }
            set { lastSeen = value; OnPropertyChanged(); }
        }

        public string PublicKey { get; set; }

    }
}
