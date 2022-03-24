namespace SportNugget.Common.API
{
    public class ResponseWrapper<T>
    {
        public T Data { get; set; }
        public bool IsSuccessful { get; set; }
        public Exception Exception { get; set; }
    }
}
