namespace presto.error {

    public class TerminatedError : PrestoError
    {

        public TerminatedError()
            : base("Terminated!")
        {
        }

    }

}
