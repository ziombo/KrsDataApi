using KrsApiIntegration;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrsDataApi.Service
{
    public interface IKrsDataService
    {
        Task<ExcelPackage> GetCompaniesExcelData(IFormFile file);
    }
}
