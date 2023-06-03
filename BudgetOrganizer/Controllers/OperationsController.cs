using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.OperationModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using BudgetOrganizer.Services;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace BudgetOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : Controller
    {
        private readonly BudgetOrganizerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;
        private readonly IAuthService _authService;

        public OperationsController(BudgetOrganizerDbContext context, IMapper mapper, IReportService reportService, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _reportService = reportService;
            _authService = authService;
        }

        #region Admin
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{accountId:guid}")]
        public async Task<ActionResult<IEnumerable<GetOperationDTO>>> GetOperationsByAccountId([FromRoute] Guid accountId, string? sortOrder, [FromQuery] FilterOperationDTO? filterParam)
        {
            if (_context.Operations == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(accountId);

            if (account == null)
            {
                return NotFound("That account doesn't exist");
            }

            var operations = _reportService.GetOpertaionsReport(accountId, sortOrder, filterParam);
            if (operations == null)
                return NotFound("No operations were found");

            return Ok(_mapper.Map<List<Operation>, List<GetOperationDTO>>(operations.ToList()));
        }
        #endregion


        #region User
        [Authorize]
        [HttpGet]
        [Route("me")]
        public async Task<ActionResult<IEnumerable<GetOperationDTO>>> GetOperationsFromToken(string? sortOrder, [FromQuery] FilterOperationDTO? filterParam)
        {
            if (_context.Operations == null)
            {
                return NotFound();
            }

            //Server Error token doesn't have account id
            var claim = HttpContext.User.FindFirst("id");
            if (claim == null)
                return StatusCode(500);

            var accountId = new Guid(claim.Value);
            var account = await _context.Accounts.FindAsync(accountId);

            if (account == null)
            {
                return NotFound("That account doesn't exist");
            }

            var operations = _reportService.GetOpertaionsReport(accountId, sortOrder, filterParam);
            if (operations == null)
                return NotFound("No operations were found");

            return Ok(_mapper.Map<List<Operation>, List<GetOperationDTO>>(operations.ToList()));
        }

        // POST: api/Operations
        [Authorize]
        [HttpPost]
        [Route("me")]
        public async Task<ActionResult<GetOperationDTO>> AddOperation(AddOperationDTO operationToAdd)
        {
            if (_context.Operations == null)
            {
                return Problem("Entity set 'BudgetOrganizerDbContext.Operations'  is null.");
            }

            var claim = HttpContext.User.FindFirst("id");
            if (claim == null)
                return StatusCode(500);

            var accountId = new Guid(claim.Value);

            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound("That account doesn't exist");
            }

            var operation = _mapper.Map<Operation>(operationToAdd);

            if (operationToAdd.DateTime == null)
            {
                operation.DateTime = DateTime.UtcNow;
            }

            var category = await _context.Categories.FindAsync(operation.CategoryId);

            if (category == null)
            {
                return NotFound("Incorrect category id");
            }

            operation.Category = category;
            operation.AccountId = accountId;
            operation.Account = account;

            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<GetOperationDTO>(operation));
        }

        // DELETE: api/Operations/5
        [HttpDelete("me/{id}")]
        public async Task<IActionResult> DeleteOperation(Guid id)
        {
            if (_context.Operations == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations.FindAsync(id);

            if (operation == null)
            {
                return NotFound();
            }

            var claim = HttpContext.User.FindFirst("id");
            if (claim == null)
                return StatusCode(500);

            var accountId = new Guid(claim.Value);

            if (accountId != operation.AccountId)
                return Unauthorized("This operation belongs to another account");

            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion




        //// GET: api/Operations/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Operation>> GetOperationById(Guid id)
        //{
        //    if (_context.Operations == null)
        //    {
        //        return NotFound();
        //    }
        //    var operation = await _context.Operations.FindAsync(id);

        //    if (operation == null)
        //    {
        //        return NotFound();
        //    }

        //    return operation;
        //}

        //        // PUT: api/Operations/5
        //        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //        [HttpPut("{id}")]
        //        public async Task<IActionResult> PutOperation(Guid id, Operation operation)
        //        {
        //            if (id != operation.Id)
        //            {
        //                return BadRequest();
        //            }

        //            _context.Entry(operation).State = EntityState.Modified;

        //            try
        //            {
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!OperationExists(id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }

        //            return NoContent();
        //        }
    }
}
