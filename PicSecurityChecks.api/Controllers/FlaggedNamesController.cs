using Microsoft.AspNetCore.Mvc;
using PicSecurityChecks.api.Models;
using PicSecurityChecks.Shared;
    
namespace PicSecurityChecks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlaggedNamesController : Controller
    {
        private readonly IFlagedNamesRepository _flagedNamesRepository;

        public FlaggedNamesController(IFlagedNamesRepository flagedNamesRepository)
        {
            _flagedNamesRepository = flagedNamesRepository;
        }

        [HttpGet]
        public IActionResult GetAllFlaggedNames()
        {
            return Ok(_flagedNamesRepository.GetPIC_FlaggedNames());
        }

        [HttpGet("{id}")]
        public IActionResult GetFlaggedNameById(int id)
        {
            return Ok(_flagedNamesRepository.GetFlaggedNameById(id));
        }

        [HttpGet("{FirstName}/{LastName}/{dob}")]
        public IActionResult CheckFlaggedNames(string FirstName, string LastName, string dob)
        {
            dob = dob.Replace("-", "/");
            DateTime dateOfBirth = DateTime.Parse(dob);
            return Ok(_flagedNamesRepository.CheckFlaggedNames(FirstName, LastName, dateOfBirth));
        }

        [HttpPost]
        public IActionResult AddFlaggedName([FromBody] PIC_FlaggedNames pIC_FlaggedNames)
        {
            if (pIC_FlaggedNames == null)
                return BadRequest();

            if (pIC_FlaggedNames.firstName == string.Empty || pIC_FlaggedNames.lastName == string.Empty)
                ModelState.AddModelError("LastName/FirstName", "The first and last names are required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createFlaggedName = _flagedNamesRepository.AddFlaggedName(pIC_FlaggedNames);

            return Created("flaggedName", createFlaggedName);
        }

        [HttpPut]
        public IActionResult UpdateFlaggedName([FromBody] PIC_FlaggedNames pIC_FlaggedNames)
        {
            if (pIC_FlaggedNames == null)
                return BadRequest();

            if (pIC_FlaggedNames.firstName == string.Empty || pIC_FlaggedNames.lastName == string.Empty)
                ModelState.AddModelError("LastName/FirstName", "The first and last names are required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var flaggedNameToUpdate = _flagedNamesRepository.GetFlaggedNameById(pIC_FlaggedNames.Id);

            if (flaggedNameToUpdate == null)
                return NotFound();

            _flagedNamesRepository.UpdateFlaggedName(pIC_FlaggedNames);

            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFlaggedName(int id)
        {
            if (id == 0)
                return BadRequest();

            var flaggedNameToBeDeleted = _flagedNamesRepository.GetFlaggedNameById(id);
            
            if (flaggedNameToBeDeleted == null)
                return NotFound();

            _flagedNamesRepository.DeleteFlaggedNameById(id);

            return NoContent();
        }
    }
}
