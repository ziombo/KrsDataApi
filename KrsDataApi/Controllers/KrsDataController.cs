using KrsDataApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KrsDataApi.Controllers
{
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