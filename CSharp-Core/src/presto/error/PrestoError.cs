using System;
namespace presto.error
{

    public class PrestoError : Exception
    {

        protected PrestoError()
        {
        }

        protected PrestoError(String message)
            : base(message)
        {
        }

        protected PrestoError(Exception e)
            : base(null, e)
        {
        }

    }

}
