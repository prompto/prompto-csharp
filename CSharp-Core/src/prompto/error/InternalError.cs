using System;
namespace prompto.error {

    public class InternalError : PrestoError
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
