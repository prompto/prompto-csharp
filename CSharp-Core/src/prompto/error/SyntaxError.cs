using System;

namespace prompto.error {

    public class SyntaxError : PromptoError
    {

        public SyntaxError(String message)
         :   base(message)
        {
        }

        public String Suffix { get; set; }

        public override string Message => base.Message + (Suffix!=null ? Suffix : "");

    }

}
