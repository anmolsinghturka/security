using Microsoft.AspNetCore.Mvc;
using PicSecurityChecksWin.api.Models;
using PicSecurityChecks.Shared;

namespace PicSecurityChecksWin.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityCheckNamesController : Controller
    {
        private readonly ISecurityCheckNamesRepository _securityCheckNamesRepository;

        public SecurityCheckNamesController(ISecurityCheckNamesRepository securityCheckNamesRepository)
        {
            _securityCheckNamesRepository = securityCheckNamesRepository;
        }

        [HttpGet]
        public IActionResult GetAllRecords()
        {
            return Ok(_securityCheckNamesRepository.GetAllRecords());
        }

        [HttpGet("{username}")]
        public IActionResult GetRecordForUser(string userName) 
        {
            return Ok(_securityCheckNamesRepository.GetRecordsForUser(userName));
        }

        [HttpGet("{id}/{username}")]
        public IActionResult GetRecordById(string id, string userName)
        {
            return Ok(_securityCheckNamesRepository.GetRecordById(id, userName));
        }
        [HttpDelete("{id}/{checkName}")]
        public IActionResult UpdateSecurityCheckNames(string id, bool checkName)
        {
            if (id == string.Empty) 
            {
            return BadRequest();
            }

            if (_securityCheckNamesRepository.GetRecordById(id, "test") == null)
                return NotFound();

            _securityCheckNamesRepository.UpdateSerurityCheck(id, checkName);


            return NoContent();
        }
    }
}
