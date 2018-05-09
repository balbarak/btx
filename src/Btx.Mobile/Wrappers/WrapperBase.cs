using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Btx.Mobile.Wrappers
{
    public class WrapperBase<TModel> : ObservableObject
    {
        public TModel Model { get; private set; }

        public WrapperBase(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Model = model;
        }

        protected void SetValue<TValue>(TValue value,[CallerMemberName]string properyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(properyName);
            var currentValue = propertyInfo.GetValue(Model);

            if (!Equals(currentValue,value))
            {
                propertyInfo.SetValue(Model, value);

                OnPropertyChanged(properyName);
            }
        }

        protected TValue GetValue<TValue>([CallerMemberName]string properyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(properyName);

            return (TValue) propertyInfo.GetValue(Model);
            
        }
    }
}
