using Microsoft.AspNetCore.Mvc;
using PicSecurityChecks.api.Interface;

namespace PicSecurityChecks.api.Controllers
{
    [Route("api/[controller]")]
    public class InetController : Controller
    {
        private readonly IInetRepository _ipvRepository;

        public InetController(IInetRepository ipvRepository)
        {
            _ipvRepository = ipvRepository;
        }

        [HttpGet("{firstName}/{lastName}")]
        public IActionResult GetInet(string firstName, string lastName)
        {
            return Ok(_ipvRepository.GetInetReturns(firstName, lastName));
        }
    }
}
