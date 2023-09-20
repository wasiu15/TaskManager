using Shared.HttpServices.Requests;

namespace Shared.HttpServices.Services
{
    public interface IHttpService
    {
        Task<T> SendPostEmailAsync<T>(string baseUrl, object body);

        Task<T> SendPostRequest<T, U>(PostRequest<U> request);

        Task<T> SendPostRequestInternal<T, U>(PostRequest<U> request);
        Task<T> SendGetRequest<T>(GetRequest request);

        Task<T> SendGetRequestInternal<T>(GetRequest request);
    }
}