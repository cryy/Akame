using System;
using System.Collections.Generic;
using System.Text;

namespace Akame
{
    public class WeebApiException : Exception
    {
        public WeebApiException()
        {
        }

        public WeebApiException(string message)
            : base(message)
        {
        }

        public WeebApiException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
