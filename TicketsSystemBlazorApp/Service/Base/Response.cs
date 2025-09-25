namespace TicketsSystemBlazorApp.Service.Base
{
    public class Response<T>
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ValidationError { get; set; }

    }
}
