namespace KrsApiIntegration
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    public class KrsApiClient : IKrsApiClient
    {
        private const string GET_COMPANY_DATA_URL = "https://api-v3.mojepanstwo.pl/dane/krs_podmioty.json?conditions[krs_podmioty.krs]=";

        private readonly HttpClient _httpClient;

        public KrsApiClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<JObject> GetCompanyData(string krsNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(GET_COMPANY_DATA_URL + krsNumber);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(responseBody);

                    return json;
                }
                catch(HttpRequestException e)
                {
                    Debug.WriteLine($"{nameof(this.GetCompanyData)} exception caught:");
                    Debug.WriteLine(e.Message);
                    throw;
                }

            }
        }
    }
}