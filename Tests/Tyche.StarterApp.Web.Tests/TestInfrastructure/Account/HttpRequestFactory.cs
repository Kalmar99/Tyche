using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Tyche.StarterApp.Web.Tests.Account;

internal static class HttpRequestFactory
{
    public static HttpRequestMessage Create(string uri, object body, HttpMethod method)
    {
        var request = new HttpRequestMessage(method, uri);

        var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

        request.Content = content;

        return request;
    }
    
    public static HttpRequestMessage Create(string uri, HttpMethod method)
    {
        var request = new HttpRequestMessage(method, uri);

        return request;
    }
}