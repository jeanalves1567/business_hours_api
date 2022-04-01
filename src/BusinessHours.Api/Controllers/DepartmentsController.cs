using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessHours.Domain.Dtos.Global;
using BusinessHours.Domain.Dtos.Departments;
using BusinessHours.Domain.Errors;
using BusinessHours.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessHours.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsServices _services;

        public DepartmentsController(IDepartmentsServices services)
        {
            _services = services;
        }

        [ProducesResponseType(typeof(IEnumerable<DepartmentListDto>), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentListDto>>> ListDepartments()
        {
            try
            {
                var result = await _services.ListDepartments();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new DefaultErrorResponse(ex.Message));
            }
        }

        [ProducesResponseType(typeof(DepartmentReadDto), 201)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpPost]
        public async Task<ActionResult<DepartmentReadDto>> CreateDepartment(DepartmentCreateDto payload)
        {
            try
            {
                var result = await _services.CreateDepartment(payload);
                return CreatedAtAction(nameof(GetDepartment), new { departmentId = result.Id }, result);
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

        [ProducesResponseType(typeof(DepartmentReadDto), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpGet("{departmentId}")]
        public async Task<ActionResult<DepartmentReadDto>> GetDepartment(string departmentId)
        {
            try
            {
                var result = await _services.GetDepartment(departmentId);
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

        [ProducesResponseType(typeof(DepartmentReadDto), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 400)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpPut("{departmentId}")]
        public async Task<ActionResult<DepartmentReadDto>> UpdateDepartment(string departmentId, DepartmentUpdateDto payload)
        {
            try
            {
                var result = await _services.UpdateDepartment(departmentId, payload);
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

        [ProducesResponseType(typeof(object), 204)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpDelete("{departmentId}")]
        public async Task<ActionResult> DeleteDepartment(string departmentId)
        {
            try
            {
                await _services.DeleteDepartment(departmentId);
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

        [ProducesResponseType(typeof(DepartmentMomentStatus), 200)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 404)]
        [ProducesResponseType(typeof(DefaultErrorResponse), 500)]
        [HttpGet("{departmentId}/checkhours")]
        public async Task<ActionResult<DepartmentMomentStatus>> CheckDepartmentWorkingHours(string departmentId)
        {
            try
            {
                var result = await _services.CheckDepartmentWorkingHours(departmentId);
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

    }
}
