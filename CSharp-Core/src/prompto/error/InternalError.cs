using System;
namespace prompto.error {

    public class InternalError : PromptoError
    {

        public InternalError(String msg)
            : base(msg)
        {
        }

        public InternalError(Exception e)
            : base(e)
        {
        }
    }
}
