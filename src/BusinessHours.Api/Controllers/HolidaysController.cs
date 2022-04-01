using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Dtos.Global;
using BusinessHours.Domain.Dtos.Holidays;
using BusinessHours.Domain.Errors;
using BusinessHours.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessHours.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly IHolidaysServices _services;

        public HolidaysController(IHolidaysServices services)
        {
            _services = services;
        }

        [ProducesResponseType(typeof(IEnumerable<HolidayListDto>), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HolidayListDto>>> ListHolidays()
        {
            try
            {
                var result = await _services.ListHolidays();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new DefaultErrorResponse(ex.Message));
            }
        }

        [ProducesResponseType(typeof(HolidayReadDto), 201)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult<HolidayReadDto>> CreateHoliday(HolidayCreateDto payload)
        {
            try
            {
                var result = await _services.CreateHoliday(payload);
                return CreatedAtAction(nameof(GetHoliday), new { holidayId = result.Id }, result);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new DefaultErrorResponse(ex.Message));
            }
        }

        [ProducesResponseType(typeof(HolidayReadDto), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpGet("{holidayId}")]
        public async Task<ActionResult<HolidayReadDto>> GetHoliday(string holidayId)
        {
            try
            {
                var result = await _services.GetHoliday(holidayId);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new DefaultErrorResponse(ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new DefaultErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new DefaultErrorResponse(ex.Message));
            }
        }

        [ProducesResponseType(typeof(HolidayReadDto), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpPut("{holidayId}")]
        public async Task<ActionResult<HolidayReadDto>> UpdateHoliday(string holidayId, HolidayUpdateDto payload)
        {
            try
            {
                var result = await _services.UpdateHoliday(holidayId, payload);
                return Ok(result);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new DefaultErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new DefaultErrorResponse(ex.Message));
            }
        }

        [ProducesResponseType(typeof(object), 204)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpDelete("{holidayId}")]
        public async Task<ActionResult> DeleteHoliday(string holidayId)
        {
            try
            {
                await _services.DeleteHoliday(holidayId);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new DefaultErrorResponse(ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new DefaultErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new DefaultErrorResponse(ex.Message));
            }
        }
    }
}
