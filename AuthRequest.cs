using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace McLib.Netwrok.Authentication;

public class AuthRequestBase
{
    private HttpClient _client;
    private HttpContext _context;
    private readonly bool existContext = false;
    public AuthRequestBase(HttpClient client,HttpContext context)
    {
        this._client = client;
        this._context = context;
        this.existContext = context == null ? false : true;
    }
    public AuthRequestBase() : this(new HttpClient(),null){}
    
    public AuthRequestBase(HttpClient client) : this(client,null) {}
    private HttpRequestMessage HttpRequest(HttpMethod method,string requestURL)
    {
        
    }

    protected async Task<HttpResponseMessage> HttpRequestPost(string requestURL,object _object)
    {
        var request = this.HttpRequest(HttpMethod.Post,requestURL, _object);
        if (existContext)
        {
            return await _client.SendAsync(request, _context.RequestAborted);
        }
        else
        {
            return await _client.SendAsync(request);
        }
            

    }

    protected async Task<HttpResponseMessage> HttpReqeustGet(string requestURL)
    {
        this.HttpRequest(HttpMethod.Post, requestURL,"");

        return Task.CompletedTask;
    }

    protected StringContent a(string stringifyContent)
    {
        return new StringContent(stringifyContent, Encoding.UTF8, "application/json");
    }

    protected StringContent a(object objectContent)
    {
        string serializedContent = JsonSerializer.Serialize(objectContent);
        return new StringContent(serializedContent, Encoding.UTF8, "application/json");
    }
}