using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace KrsApiIntegration
{
    public interface IKrsApiClient
    {
        Task<JObject> GetCompanyData(string krsNumber);
    }
}