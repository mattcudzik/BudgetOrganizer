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
using BudgetOrganizer.Models.AccountModel;
using Microsoft.Data.SqlClient;
using System.Web.Http.Results;

namespace BudgetOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : Controller
    {
        private readonly BudgetOrganizerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        public OperationsController(BudgetOrganizerDbContext context, IMapper mapper, IReportService reportService)
        {
            _context = context;
            _mapper = mapper;
            _reportService = reportService;
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

            
            try
            {
                var operations = _reportService.GetOpertaionsReport(accountId, sortOrder, filterParam);
                if (operations == null)
                    return Problem("Server error - operations cant be null");
                return Ok(_mapper.Map<List<Operation>, List<GetOperationDTO>>(operations.ToList()));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion


        #region User
        [Authorize]
        [HttpGet]
        [Route("me")]
        public async Task<ActionResult<IEnumerable<GetOperationDTO>>> GetOperationsFromToken
            (string? sortOrder, [FromQuery] FilterOperationDTO? filterParam)
        {
            if (_context.Operations == null)
            {
                return NotFound("Database error");
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
            try
            {
                var operations = _reportService.GetOpertaionsReport(accountId, sortOrder, filterParam);
                if (operations == null)
                    return Problem("Server error");
                return Ok(_mapper.Map<List<Operation>, List<GetOperationDTO>>(operations.ToList()));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("me/category-report")]
        public async Task<ActionResult<IEnumerable<OperationByCategoryReportDTO>>> GetOperationByCategoryReport(bool positive)
        {
            if (_context.Operations == null)
                return Problem("Database Error");

            //Server Error token doesn't have account id
            var claim = HttpContext.User.FindFirst("id");
            if (claim == null)
                return Problem("Server error token doesn't have account id");

            var accountId = new Guid(claim.Value);

            try
            {
                var operations = await _reportService.GetOpertaionsCategoryReport(accountId, positive);
                if (operations == null)
                    return Problem("Server error");
                return Ok(operations);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
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

            

            if (operationToAdd.DateTime == null)
            {
                operationToAdd.DateTime = DateTime.UtcNow;
            }
            var operation = _mapper.Map<Operation>(operationToAdd);
            var category = await _context.Categories.FindAsync(operation.CategoryId);

            if (category == null)
            {
                return NotFound("Incorrect category id");
            }


            account.Budget += operation.Amount;

            operation.Category = category;
            operation.AccountId = accountId;
            operation.Account = account;

            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<GetOperationDTO>(operation);
            result.CurrentBudget = account.Budget;

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("me/transfer")]
        public async Task<IActionResult> MakeTransfer(TransferOperationDTO transferOperationDTO)
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

            var destinationAccount =  await _context.Accounts.FindAsync(transferOperationDTO.DestinationAccount);
            if(destinationAccount == null)
            {
                return NotFound("Destination account doesn't exsits");
            }

            
            //create operation for both accounts
            var category = await _context.Categories.Where(o => o.Name == "Przelew").FirstOrDefaultAsync();
            if (category == null)
            {
                category = new Models.CategoryModel.Category()
                {
                    Color = "#ffffff",
                    Name = "Przelew",
                    Id = Guid.NewGuid()
                };
                _context.Categories.Add(category);
            }
                

            //deduct from one account
            account.Budget -= transferOperationDTO.Amount;
            var operation = new Operation()
            {
                Account = account,
                Category = category,
                Amount = -transferOperationDTO.Amount,
                DateTime = DateTime.UtcNow
            };
            account.Operations.Add(operation);

            //add to another account
            destinationAccount.Budget += transferOperationDTO.Amount;
            operation.Amount = transferOperationDTO.Amount;
            destinationAccount.Operations.Add(operation);

            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Operations/5
        [HttpDelete("me/{id}")]
        public async Task<IActionResult> DeleteOperation(Guid id)
        {
            if (_context.Operations == null)
            {
                return NotFound("Database error");
            }

            var operation = await _context.Operations.FindAsync(id);

            if (operation == null)
            {
                return NotFound("Operation not found");
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
