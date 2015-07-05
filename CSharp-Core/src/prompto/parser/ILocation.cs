namespace prompto.parser
{

    public interface ILocation
    {

        int Index { get; }
        int Line { get; }
        int Column { get; }

    }

}
