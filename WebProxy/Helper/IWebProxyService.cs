using System.Threading.Tasks;

namespace WebProxy.Helper
{
    public interface IWebProxyService
    {
        Task<string> GetResource(string relativeUrl);
    }
}