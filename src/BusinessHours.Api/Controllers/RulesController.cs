using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Dtos;
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

        [ProducesResponseType(typeof(IEnumerable<BusinessHoursRuleListDto>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessHoursRuleListDto>>> ListRules()
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

        [ProducesResponseType(typeof(BusinessHoursRuleReadDto), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult<BusinessHoursRuleReadDto>> CreateRule(BusinessHoursRuleCreateDto payload)
        {
            try
            {
                var result = await _services.CreateBusinessHoursRule(payload);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new DefaultErrorResponse(ex.Message));
            }
        }

        [ProducesResponseType(typeof(BusinessHoursRuleReadDto), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpGet("{ruleId}")]
        public async Task<ActionResult<BusinessHoursRuleReadDto>> GetRule(string ruleId)
        {
            try
            {
                var result = await _services.GetBusinessHoursRule(ruleId);
                return Ok(result);
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

        [ProducesResponseType(typeof(BusinessHoursRuleReadDto), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpPut("{ruleId}")]
        public async Task<ActionResult<BusinessHoursRuleReadDto>> UpdateRule(string ruleId, BusinessHoursRuleUpdateDto payload)
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
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpDelete("{ruleId}")]
        public async Task<ActionResult> DeleteRule(string ruleId)
        {
            try
            {
                await _services.DeleteBusinessHoursRule(ruleId);
                return NoContent();
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
    }
}
