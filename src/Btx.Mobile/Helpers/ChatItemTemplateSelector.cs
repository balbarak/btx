using Btx.Client.Domain.Models;
using Btx.Mobile.Models;
using Btx.Mobile.ViewModels;
using Btx.Mobile.Views;
using Btx.Mobile.Wrappers;
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
        private readonly DataTemplate incomingImageTemplate;


        public ChatItemTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingChatItem));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingChatItem));
            this.outgoinrFileTemplate = new DataTemplate(typeof(OutgoinImage));
            this.incomingImageTemplate = new DataTemplate(typeof(IncomingImage));


        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var model = item as BtxMessageWrapper;

            if (model == null)
                throw new ArgumentNullException("Template id cann't be null");

            switch (model.BtxMessageType)
            {
                case BtxMessageType.Incoming:

                    return incomingDataTemplate;

                case BtxMessageType.Outgoing:

                    return outgoingDataTemplate;

                case BtxMessageType.Info:

                    return infoDataTemplate;

                case BtxMessageType.OutgoingImage:
                    
                    return outgoinrFileTemplate;

                case BtxMessageType.IncomingImage:

                    return incomingImageTemplate;

                default:
                    return null;
            }


        }
    }
}
