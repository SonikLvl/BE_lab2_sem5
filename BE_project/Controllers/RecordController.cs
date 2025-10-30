using BE_project.DTOs.Record;
using BE_project.Exceptions;
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
        public async Task<ActionResult<RecordDTO>> CreateRecord(CreateRecordDTO recordDTO)
        {
            try
            {
                var createdRecord = await _recordService.CreateRecordAsync(recordDTO);
                return CreatedAtAction(nameof(GetRecordById), new { id = createdRecord.Id }, createdRecord);// 201
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404
            }
        }

        [HttpGet("{recordId}")] // GET /record/{id}
        public async Task<ActionResult<RecordDTO>> GetRecordById(int recordId)
        {
            try
            {
                var record = await _recordService.GetRecordByIdAsync(recordId);
                return Ok(record); // 200 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404 
            }
        }

        [HttpDelete("{recordId}")] // DELETE /record/{id}
        public async Task<IActionResult> DeleteRecord(int recordId)
        {
            try
            {
                await _recordService.DeleteRecordAsync(recordId);
                return NoContent(); // 204 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404 
            }
        }

        [HttpGet] // GET /record?userId=1&categoryId=5
        public async Task<IActionResult> GetRecords([FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!userId.HasValue && !categoryId.HasValue)
            {
                return BadRequest("You must provide at least a userId or a categoryId.");
            }
            var records = await _recordService.GetRecordsAsync(userId, categoryId);
            return Ok(records); // 200
        }
    }
}
