
namespace BlogService.Filter.Exception
{
    public class CustomeException : System.Exception
    {

        public CustomeException() { }

        public CustomeException(string message)
        : base(message)
        { }

        public CustomeException(string message, System.Exception innerException)
        : base(message, innerException)
        { }

    }
}
