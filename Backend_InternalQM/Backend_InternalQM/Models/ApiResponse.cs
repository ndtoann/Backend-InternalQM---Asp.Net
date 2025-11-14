namespace Backend_InternalQM.Models
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
