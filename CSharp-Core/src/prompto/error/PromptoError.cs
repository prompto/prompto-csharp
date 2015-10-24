using System;

namespace prompto.error
{

    public class PromptoError : Exception
    {

        protected PromptoError()
        {
        }

        protected PromptoError(String message)
            : base(message)
        {
        }

        protected PromptoError(Exception e)
            : base(null, e)
        {
        }

    }

}
