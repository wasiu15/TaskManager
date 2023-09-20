namespace Shared.HttpServices.Requests;

public class PostRequest<T>
{
    public string Url { get; set; }
    public T Data { get; set; }
}