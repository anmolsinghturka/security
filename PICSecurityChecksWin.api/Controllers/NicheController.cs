using Microsoft.AspNetCore.Mvc;
using PicSecurityChecksWin.api.Models;
using PicSecurityChecks.Shared;

namespace PicSecurityChecksWin.api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class NicheController : Controller
    {
        private readonly IConfiguration _configuration;

        public NicheController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{lastname}/{firstname}")]
        public async Task<IActionResult> Index(string lastName, string firstName)
        {
            GetNicheDataRepository dataRepository;
            dataRepository = new GetNicheDataRepository(_configuration);
            return Ok(await dataRepository.GetData(lastName,firstName));
        }

        [HttpGet("{path}")]
        public async Task<IActionResult> getByPath(string path)
        {
            GetNicheDataRepository dataRepository;
            dataRepository = new GetNicheDataRepository(_configuration);
            return Ok(await dataRepository.GetDataByPath(path));
        }
    }
}
