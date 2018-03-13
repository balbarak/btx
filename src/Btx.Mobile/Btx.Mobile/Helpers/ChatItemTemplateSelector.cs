using Btx.Mobile.Models;
using Btx.Mobile.ViewModels;
using Btx.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Btx.Mobile.Helpers
{
    public class ChatItemTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
        private readonly DataTemplate infoDataTemplate;
        private readonly DataTemplate outgoinrFileTemplate;

        public ChatItemTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingChatItem));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingChatItem));
            this.outgoinrFileTemplate = new DataTemplate(typeof(OutgoingFile));
            
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var model = item as ChatItemViewModel;

            if (model == null)
                return null;

            switch (model.ItemType)
            {
                case ChatItemType.Incoming:

                    return incomingDataTemplate;

                case ChatItemType.Outgoing:

                    return outgoingDataTemplate;

                case ChatItemType.Info:

                    return infoDataTemplate;

                case ChatItemType.OutgoingFile:
                    
                    return outgoinrFileTemplate;
                default:
                    return null;
            }


        }
    }
}
