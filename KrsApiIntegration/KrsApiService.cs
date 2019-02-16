using KrsApiIntegration.Utils;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KrsApiIntegration
{
    public class KrsApiService : IKrsApiService
    {
        private readonly IKrsApiClient _krsApiClient;


        public KrsApiService(IKrsApiClient krsApiClient)
        {
            this._krsApiClient = krsApiClient;
        }

        public async Task<ExcelPackage> GetCompaniesExcelData(IList<string> krsNumbers)
        {
            var companies = await GetCompaniesDataAsync(krsNumbers);

            return ExcelHelper.CreateExcelFile(companies, "Rejestr KRS");
        }

        private async Task<IList<Company>> GetCompaniesDataAsync(IList<string> krsNumbers)
        {
            var companiesData = new List<JObject>();
            var companies = new List<Company>();
            foreach (var krsNumber in krsNumbers)
            {
                var companyJObject = await this._krsApiClient.GetCompanyData(krsNumber);
                if ((int)companyJObject["Count"] == 0)
                {
                    continue;
                }

                var companyDTO = new Company();
                companyDTO.Krs = companyJObject["Dataobject"][0]["data"]["krs_podmioty.krs"].ToString();
                companyDTO.Name = companyJObject["Dataobject"][0]["data"]["krs_podmioty.nazwa"].ToString();
                companies.Add(companyDTO);
            }

            return companies;
        }

    }
}