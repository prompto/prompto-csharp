using System;

namespace presto.error {

    public class SyntaxError : PrestoError
    {

        public SyntaxError(String message)
         :   base(message)
        {
        }

    }

}
