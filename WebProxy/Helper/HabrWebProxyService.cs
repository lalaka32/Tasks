using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebProxy.Helper
{
    public class HabrWebProxyService : IWebProxyService
    {
        private readonly Uri _api = new Uri("https://habr.com");
        private readonly IHttpContextAccessor _httpContextAccessor;


        public HabrWebProxyService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetResource(string relativeUrl)
        {
            using (var httpClient = new HttpClient())
            {
                var resource = await httpClient.GetStringAsync(new Uri(_api, relativeUrl).ToString());

                // Change all links from habr to current site
                resource = resource.Replace(_api.ToString(),
                    $"https://{_httpContextAccessor.HttpContext.Request.Host}/");
                
                return resource;
            }
        }
    }
}