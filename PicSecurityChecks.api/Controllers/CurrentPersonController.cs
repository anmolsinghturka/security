using Microsoft.AspNetCore.Mvc;
using PicSecurityChecks.api.Interface;

namespace PicSecurityChecks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentPersonController : Controller
    {
        private readonly ICurrentPersonRepository _currentPersonRepository;

        public CurrentPersonController(ICurrentPersonRepository currentPersonRepository)
        {
            _currentPersonRepository = currentPersonRepository;
        }

        public IActionResult Index()
        {
            return Ok(_currentPersonRepository.GetCurrentPerson());
        }
    }
}
