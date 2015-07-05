using System;

namespace prompto.error {

    public class SyntaxError : PrestoError
    {

        public SyntaxError(String message)
         :   base(message)
        {
        }

    }

}
