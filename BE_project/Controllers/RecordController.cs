using BE_project.DTOs.Record;
using BE_project.Models;
using BE_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace BE_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;
        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpPost] // POST /record
        public ActionResult<RecordDTO> CreateRecord(CreateRecordDTO recordDto)
        {
            var newRecord = _recordService.CreateRecord(recordDto);
            return CreatedAtAction(nameof(GetRecordById), new { recordId = newRecord.Id }, newRecord); //201
        }

        [HttpGet("{recordId}")] // GET /record/{id}
        public ActionResult<RecordDTO> GetRecordById(int recordId)
        {
            var record = _recordService.GetRecordById(recordId);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpDelete("{recordId}")] // DELETE /record/{id}
        public IActionResult DeleteRecord(int recordId)
        {
            _recordService.DeleteRecord(recordId);
            return NoContent(); // 204
        }

        [HttpGet] // GET /record?userId=1&categoryId=5
        public IActionResult GetRecords([FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!userId.HasValue && !categoryId.HasValue)
            {
                return BadRequest("You must provide at least a userId or a categoryId.");
            }
            var record = _recordService.GetRecords(userId, categoryId);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }
    }
}
