using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Dtos.Global;
using BusinessHours.Domain.Dtos.Rules;
using BusinessHours.Domain.Errors;
using BusinessHours.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessHours.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly IBusinessHoursRulesServices _services;

        public RulesController(IBusinessHoursRulesServices services)
        {
            _services = services;
        }

        [ProducesResponseType(typeof(IEnumerable<RuleListDto>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleListDto>>> ListRules()
        {
            try
            {
                var result = await _services.ListBusinessHoursRules();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [ProducesResponseType(typeof(RuleReadDto), 201)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult<RuleReadDto>> CreateRule(RuleCreateDto payload)
        {
            try
            {
                var result = await _services.CreateBusinessHoursRule(payload);
                return CreatedAtAction(nameof(GetRule), new { ruleId = result.Id }, result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (TimeZoneNotFoundException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (InvalidTimeZoneException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new DefaultErrorResponse(ex.Message));
            }
        }

        [ProducesResponseType(typeof(RuleReadDto), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpGet("{ruleId}")]
        public async Task<ActionResult<RuleReadDto>> GetRule(string ruleId)
        {
            try
            {
                var result = await _services.GetBusinessHoursRule(ruleId);
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

        [ProducesResponseType(typeof(RuleReadDto), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpPut("{ruleId}")]
        public async Task<ActionResult<RuleReadDto>> UpdateRule(string ruleId, RuleUpdateDto payload)
        {
            try
            {
                var result = await _services.UpdateBusinessHoursRule(ruleId, payload);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (TimeZoneNotFoundException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (InvalidTimeZoneException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new DefaultErrorResponse(ex.Message));
            }
        }

        [ProducesResponseType(typeof(object), 204)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpDelete("{ruleId}")]
        public async Task<ActionResult> DeleteRule(string ruleId)
        {
            try
            {
                await _services.DeleteBusinessHoursRule(ruleId);
                return NoContent();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
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

        [ProducesResponseType(typeof(object), 204)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpPost("{ruleId}/holiday/{holidayId}")]
        public async Task<ActionResult> AssignHoliday(string ruleId, string holidayId)
        {
            try
            {
                await _services.AssignHoliday(ruleId, holidayId);
                return NoContent();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
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

        [ProducesResponseType(typeof(object), 204)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpDelete("{ruleId}/holiday/{holidayId}")]
        public async Task<ActionResult> UnassignHoliday(string ruleId, string holidayId)
        {
            try
            {
                await _services.UnassignHoliday(ruleId, holidayId);
                return NoContent();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new DefaultErrorResponse(ex.Message));
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
