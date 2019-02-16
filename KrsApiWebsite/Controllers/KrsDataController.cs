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

    using KrsApiWebsite.Service;

    using Newtonsoft.Json.Linq;

    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json", "multipart/form-data")]
    public class KrsDataController : ControllerBase
    {
        private readonly IKrsDataService _krsDataService;

        public KrsDataController(IKrsDataService krsDataClient)
        {
            this._krsDataService = krsDataClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            return this.File((await this._krsDataService.GetCompaniesExcelData(file)).GetAsByteArray(), "application/octet-stream");
        }
    }
}