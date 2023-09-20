using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.HttpServices.Requests;

namespace Shared.HttpServices.Services;

public class HttpService : IHttpService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<HttpService> _logger;

    public HttpService(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<HttpService> logger)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<T> SendPostEmailAsync<T>(string baseUrl, object body)
    {
        try
        {
            using (var _httpClient = new System.Net.Http.HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseUrl);

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var _response = await _httpClient.PostAsync(baseUrl, content); // Await this async call

                if (_response.IsSuccessStatusCode)
                {
                    var _content1 = await _response.Content.ReadAsStringAsync(); // Await this async call
                    var item = JsonConvert.DeserializeObject<T>(_content1);
                    return item;
                }
                else
                {
                    Console.WriteLine(_response);
                    throw new Exception(await _response.Content.ReadAsStringAsync()); // Await this async call
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public async Task<T> SendPostRequestInternal<T, U>(PostRequest<U> request)
    {
        var client = _clientFactory.CreateClient("retryPolicy");
        var message = new HttpRequestMessage
        {
            RequestUri = new Uri(request.Url),
            Method = HttpMethod.Post
        };
        message.Headers.Add("Accept", "application/json");
        message.Headers.Add("stb-client", "internal");
        client.DefaultRequestHeaders.Clear();
        //client.DefaultRequestHeaders.Add("X-CID", clientSecretKey);
        var data = JsonConvert.SerializeObject(request.Data);
        _logger.LogInformation("Sending POST request to {Url} with Body {data}", message.RequestUri, data);
        message.Content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.SendAsync(message);
        var content = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Response from {Url} is {response}", message.RequestUri, content);
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<T> SendGetRequest<T>(GetRequest request)
    {
        //TODO: Validate the request URL to ensure it is a valid external URL
        var url = request.Url;
        var uri = new Uri(url);
        if (!uri.IsWellFormedOriginalString() || !uri.IsAbsoluteUri || uri.IsLoopback)
        {
            throw new ArgumentException("Invalid URL");
        }

        var client = _clientFactory.CreateClient();
        var message = new HttpRequestMessage();
        message.RequestUri = uri;
        message.Method = HttpMethod.Get;
        message.Headers.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Clear();
        _logger.LogInformation("Sending GET request to {Url}", message.RequestUri);
        var response = await client.SendAsync(message);
        var content = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Response from {Url} is {response}", message.RequestUri, content);
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<T> SendGetRequestInternal<T>(GetRequest request)
    {
        //TODO: Validate the request URL to ensure it is a valid external URL
        var url = request.Url;
        var uri = new Uri(url);
        if (!uri.IsWellFormedOriginalString() || !uri.IsAbsoluteUri || uri.IsLoopback)
        {
            throw new ArgumentException("Invalid URL");
        }

        var client = _clientFactory.CreateClient();
        var message = new HttpRequestMessage();
        message.RequestUri = uri;
        message.Method = HttpMethod.Get;
        message.Headers.Add("Accept", "application/json");
        message.Headers.Add("stb-clientId", "internal");
        client.DefaultRequestHeaders.Clear();
        _logger.LogInformation("Sending GET request to {Url}", message.RequestUri);
        var response = await client.SendAsync(message);
        var content = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Response from {Url} is {response}", message.RequestUri, content);
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<T> SendPostRequest<T, U>(PostRequest<U> request)
    {
        var client = _clientFactory.CreateClient();
        var message = new HttpRequestMessage
        {
            RequestUri = new Uri(request.Url),
            Method = HttpMethod.Post
        };
        message.Headers.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Clear();
        //client.DefaultRequestHeaders.Add("X-CID", clientSecretKey);
        var data = JsonConvert.SerializeObject(request.Data);
        _logger.LogInformation("Sending POST request to {Url} with Body {data}", message.RequestUri, data);
        message.Content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.SendAsync(message);
        var content = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Response from {Url} is {response}", message.RequestUri, content);
        return JsonConvert.DeserializeObject<T>(content);
    }
}