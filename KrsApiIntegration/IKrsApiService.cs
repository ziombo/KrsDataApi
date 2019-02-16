using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KrsApiIntegration
{
    public interface IKrsApiService
    {
        Task<ExcelPackage> GetCompaniesExcelData(IList<string> krsNumbers);
    }
}
