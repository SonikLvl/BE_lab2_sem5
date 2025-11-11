using BE_project.DTOs.Record;
using BE_project.Exceptions;
using BE_project.Models;
using BE_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var userId = GetCurrentUserId();

            try
            {
                var createdRecord = await _recordService.CreateRecordAsync(recordDTO, userId);
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

        [HttpGet("{recordId}")] // GET /record/{id}
        public async Task<ActionResult<RecordDTO>> GetRecordById(int recordId)
        {
            var userId = GetCurrentUserId();

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

        [HttpDelete("{recordId}")] // DELETE /record/{id}
        public async Task<IActionResult> DeleteRecord(int recordId)
        {
            var userId = GetCurrentUserId();

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
        public async Task<IActionResult> GetRecords([FromQuery] int? categoryId)
        {
            var userId = GetCurrentUserId();

            var records = await _recordService.GetRecordsAsync(userId, categoryId);
            return Ok(records); // 200
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Cannot find user ID claim in token.");
            }

            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            throw new UnauthorizedAccessException("User ID claim is in an invalid format.");
        }
    }
}
