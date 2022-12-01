namespace Exceptions
{
    //This class is used to throw exception when a person with a given ID is not found in DB. Hence we are extending the ArgumentException
    //class here
    public class InvalidPersonIDException : ArgumentException
    {
        public InvalidPersonIDException()
        {
        }

        public InvalidPersonIDException(string? message) : base(message)
        {
        }

        public InvalidPersonIDException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}