

namespace prompto.intrinsic
{
    public class Binary
    {

        public Binary(string mimeType, byte[] data)
        {
            MimeType = mimeType;
            Data = data;
        }

        public string MimeType { get; set; }

        public byte[] Data { get; set; }
    }
}
