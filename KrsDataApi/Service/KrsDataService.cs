using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KrsDataApi.Service
{
    using KrsApiIntegration;

    using Microsoft.AspNetCore.Http;
    using OfficeOpenXml;
    using System.IO;
    using System.Linq;

    public class KrsDataService : IKrsDataService
    {
        private readonly IKrsApiService _krsApiService;

        public KrsDataService(IKrsApiService krsApiService)
        {
            this._krsApiService = krsApiService;
        }

        public async Task<ExcelPackage> GetCompaniesExcelData(IFormFile file)
        {
            var companiesKrs = await this.GetCompaniesKrsFromFile(file);

            return await this._krsApiService.GetCompaniesExcelData(companiesKrs);
        }

        private async Task<IList<string>> GetCompaniesKrsFromFile(IFormFile file)
        {
            string fileContent = String.Empty;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                fileContent = await reader.ReadToEndAsync();
            }

            if (String.IsNullOrEmpty(fileContent))
            {
                return new List<string>();
            }

            return fileContent.Split(';').ToList();

        }
    }
}
