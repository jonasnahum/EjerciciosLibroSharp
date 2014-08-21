using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AsyncDelegateInvocation
{
    public class CalculateCompletedEventArgs : AsyncCompletedEventArgs
    {
        public CalculateCompletedEventArgs(
                                string value,
                                Exception error,
                                bool cancelled,
                                object userState)
        : base(error, cancelled, userState)
        {
            Result = value;
        }
        public string Result { get; private set; }
    }
}
