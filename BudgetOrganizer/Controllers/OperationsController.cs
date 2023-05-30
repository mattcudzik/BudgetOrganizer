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

namespace BudgetOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : Controller
    {
        private readonly BudgetOrganizerDbContext _context;
        private readonly IMapper _mapper;

        public OperationsController(BudgetOrganizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //TODO: get accountId from token
        [HttpGet]
        [Route("{accoundId:guid}")]
        public async Task<ActionResult<IEnumerable<GetOperationDTO>>> GetOperationsByAccountId([FromRoute] Guid accountId)
        {
            if (_context.Operations == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }

            var operations = _context.Operations.Where(operation => operation.AccountId == accountId).ToList();

            if(operations.Count == 0) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<Operation>,List<GetOperationDTO>>(operations));
        }


        // GET: api/Operations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operation>> GetOperationById(Guid id)
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

            return operation;
        }

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

        // POST: api/Operations
        [HttpPost]
        [Route("{accoundId:guid}")]
        public async Task<ActionResult<Operation>> AddOperation(AddOperationDTO operationToAdd, [FromRoute]Guid accountId)
        {
            if (_context.Operations == null)
            {
                return Problem("Entity set 'BudgetOrganizerDbContext.Operations'  is null.");
            }

            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            var operation = _mapper.Map<Operation>(operationToAdd);

            //TODO create mapping
            operation.DateTime = DateTime.UtcNow;
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

            return Ok(operation);
        }

        // DELETE: api/Operations/5
        [HttpDelete("{id}")]
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

            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //        private bool OperationExists(Guid id)
        //        {
        //            return (_context.Operations?.Any(e => e.Id == id)).GetValueOrDefault();
        //        }
    }
}
