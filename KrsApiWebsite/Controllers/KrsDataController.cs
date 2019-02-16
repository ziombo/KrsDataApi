using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KrsApiWebsite.Controllers
{
    using KrsApiIntegration;

    using Microsoft.AspNetCore.Authorization;
    using System.IO;

    using Newtonsoft.Json.Linq;

    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json", "multipart/form-data")]
    public class KrsDataController : ControllerBase
    {
        private readonly IKrsApiClient _krsApiClient;

        public KrsDataController(IKrsApiClient krsApiClient)
        {
            this._krsApiClient = krsApiClient;
        }

        [HttpGet]
        public async Task<JObject> Get(string krsNumber)
        {
            return await this._krsApiClient.GetCompanyData(krsNumber);
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            string fileContent = String.Empty;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                fileContent = await reader.ReadToEndAsync();
            }

            if (String.IsNullOrEmpty(fileContent))
            {
                return this.BadRequest();
            }

            IEnumerable<string> krsNumbers = fileContent.Split(';');

            var companiesData = new List<JObject>();
            var companies = new List<Company>();
            foreach (var krsNumber in krsNumbers)
            {
                var company = await this._krsApiClient.GetCompanyData(krsNumber);
                companiesData.Add(company);
                
                var companyDTO = new Company();
                companyDTO.Krs = company["Dataobject"][0]["data"]["krs_podmioty.krs"].ToString();
                companyDTO.Name = company["Dataobject"][0]["data"]["krs_podmioty.nazwa"].ToString();
                companies.Add(companyDTO);
            }

            return this.Ok(companies);
        }
    }
}