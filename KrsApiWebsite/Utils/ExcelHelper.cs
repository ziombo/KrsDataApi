using KrsApiIntegration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KrsApiWebsite.Utils
{
    public class ExcelHelper
    {
        public static ExcelPackage CreateExcelFile(IEnumerable<Company> companies, string fileName)
        {
            fileName = FixFileNameExcel(fileName);

            FileInfo f = new FileInfo(fileName);
            var excelFile = new ExcelPackage(f);

            var worksheet = excelFile.Workbook.Worksheets.Add("KrsData");
            worksheet.Cells["A1"].LoadFromCollection(Collection: companies, PrintHeaders: true);
            worksheet.Cells.AutoFitColumns(0);

            return excelFile;
        }

        private static string FixFileNameExcel(string fileName)
        {
            if (Path.HasExtension(fileName))
            {
                string ext = Path.GetExtension(fileName);

                if (ext != ".xlsx")
                {
                    fileName = Path.GetFileNameWithoutExtension(fileName) + ".xlsx";
                }
            }
            else
            {
                fileName += ".xlsx";
            }

            return fileName;
        }
    }
}
