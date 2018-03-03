using Btx.Mobile.Models;
using Btx.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Btx.Mobile.Helpers
{
    public class ChatItemDataTemplate : DataTemplateSelector
    {
        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
        private readonly DataTemplate infoDataTemplate;

        public ChatItemDataTemplate()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingChatItem));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingChatItem));
            
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var model = item as ChatItem;

            if (model == null)
                return null;

            switch (model.ItemType)
            {
                case ChatItem.ChatItemType.Incoming:

                    return incomingDataTemplate;

                case ChatItem.ChatItemType.Outgoing:

                    return outgoingDataTemplate;

                case ChatItem.ChatItemType.Info:

                    return infoDataTemplate;

                default:
                    return null;
            }


        }
    }
}
