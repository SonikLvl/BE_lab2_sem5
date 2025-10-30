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
        public async Task<ActionResult<RecordDTO>> CreateRecord([FromBody] CreateRecordDTO recordDTO)
        {
            try
            {
                var createdRecord = await _recordService.CreateRecordAsync(recordDTO, recordDTO.UserId);
                return CreatedAtAction(nameof(GetRecordById), new { recordId = createdRecord.Id, userId = createdRecord.UserId }, createdRecord);// 201
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

        [HttpGet("{recordId}")] // GET /record/{id}?userId=userId
        public async Task<ActionResult<RecordDTO>> GetRecordById(int recordId, [FromQuery] int userId)
        {
            try
            {
                var record = await _recordService.GetRecordByIdAsync(recordId, userId);
                return Ok(record); // 200 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404 
            }
        }

        [HttpDelete("{recordId}")] // DELETE /record/{id}?userId=userId
        public async Task<IActionResult> DeleteRecord(int recordId, [FromQuery] int userId)
        {
            try
            {
                await _recordService.DeleteRecordAsync(recordId, userId);
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
