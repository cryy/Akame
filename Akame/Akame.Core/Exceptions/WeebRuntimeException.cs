using System;
using System.Collections.Generic;
using System.Text;

namespace Akame
{
    public class WeebRuntimeException : Exception
    {
        public WeebRuntimeException()
        {
        }

        public WeebRuntimeException(string message)
            : base(message)
        {
        }

        public WeebRuntimeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
