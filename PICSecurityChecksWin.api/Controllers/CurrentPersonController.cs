using Microsoft.AspNetCore.Mvc;
using PicSecurityChecksWin.api.Interface;

namespace PicSecurityChecksWin.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentPersonController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentPersonController(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public string Index()
        {
            string? username = _contextAccessor.HttpContext.User.Identity.Name;
            return username ?? string.Empty;
        }
    }
}
