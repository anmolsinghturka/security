using Microsoft.AspNetCore.Mvc;
using PicSecurityChecks.api.Models;
using PicSecurityChecks.Shared;

namespace PicSecurityChecks.api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SearchFlaggedNamesController : Controller
    {
        private readonly ISearchFlaggedNamesRepository _searchFlaggedNamesRepository;

        public SearchFlaggedNamesController(ISearchFlaggedNamesRepository searchFlaggedNamesRepository)
        {
            _searchFlaggedNamesRepository = searchFlaggedNamesRepository;
        }

        [HttpPost]
        public IActionResult FlaggedNameSearch([FromBody] PIC_FlaggedNames pic_FlaggedNames) 
        {
            return Ok(_searchFlaggedNamesRepository.SearchFlaggedNames(pic_FlaggedNames));
        }
    }
}
