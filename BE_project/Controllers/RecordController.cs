using BE_project.DTOs.Record;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        [HttpPost] // POST /record
        public IActionResult CreateRecord(CreateRecordDTO userDto)
        {
            return Ok();
        }

        [HttpGet("{recordId}")] // GET /record/{id}
        public IActionResult GetRecordById(int recordId)
        {
            return Ok();
        }

        [HttpDelete("{recordId}")] // DELETE /record/{id}
        public IActionResult DeleteRecord(int recordId)
        {
            return Ok();
        }

        [HttpGet] // GET /record?userId=1&categoryId=5
        public IActionResult GetRecords([FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!userId.HasValue && !categoryId.HasValue)
            {
                return BadRequest("You must provide at least a userId or a categoryId.");
            }
            // ... 
            return Ok();
        }
    }
}
