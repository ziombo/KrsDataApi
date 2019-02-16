namespace KrsApiIntegration
{
    using System;
    using System.Net.Http;
    using System.Runtime.InteropServices;

    public class KrsApiClient
    {
        private const string GET_COMPANY_DATA_URL = "https://api-v3.mojepanstwo.pl/dane/krs_podmioty.json?conditions[krs_podmioty.krs]=";

        public async void GetCompanyData(string companyKrs)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(GET_COMPANY_DATA_URL + companyKrs);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseBody);
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine($"{nameof(this.GetCompanyData)} exception caught:");
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}