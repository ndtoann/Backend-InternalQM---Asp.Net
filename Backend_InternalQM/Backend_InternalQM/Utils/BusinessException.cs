namespace Backend_InternalQM.Utils
{
    public class BusinessException : Exception
    {
        public int StatusCode { get; set; } = 400;

        public BusinessException(string message) : base(message)
        {
        }
    }
}
