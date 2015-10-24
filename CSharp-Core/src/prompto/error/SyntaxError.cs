using System;

namespace prompto.error {

    public class SyntaxError : PromptoError
    {

        public SyntaxError(String message)
         :   base(message)
        {
        }

    }

}
