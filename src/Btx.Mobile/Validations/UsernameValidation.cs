using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Validations
{
    public class UsernameValidation<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            string userName = value as string;
            
            if (String.IsNullOrWhiteSpace(userName))
                return false;
           
            return true;
        }
    }
}
