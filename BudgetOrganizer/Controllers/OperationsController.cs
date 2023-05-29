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

        [HttpGet]
        [Route("{profileId:guid}")]
        public async Task<ActionResult<IEnumerable<GetOperationDTO>>> GetOperationsByProfileId([FromRoute] Guid profileId)
        {
            if (_context.Operations == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.FindAsync(profileId);
            if (profile == null)
            {
                return NotFound();
            }

            var operations = _context.Operations.Where(operation => operation.ProfileId == profileId).ToList();

            if(operations.Count == 0) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<Operation>,List<GetOperationDTO>>(operations));
        }


    }
}
//        // GET: api/Operations/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Operation>> GetOperation(Guid id)
//        {
//          if (_context.Operations == null)
//          {
//              return NotFound();
//          }
//            var operation = await _context.Operations.FindAsync(id);

//            if (operation == null)
//            {
//                return NotFound();
//            }

//            return operation;
//        }

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

//        // POST: api/Operations
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Operation>> PostOperation(Operation operation)
//        {
//          if (_context.Operations == null)
//          {
//              return Problem("Entity set 'BudgetOrganizerDbContext.Operations'  is null.");
//          }
//            _context.Operations.Add(operation);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetOperation", new { id = operation.Id }, operation);
//        }

//        // DELETE: api/Operations/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteOperation(Guid id)
//        {
//            if (_context.Operations == null)
//            {
//                return NotFound();
//            }
//            var operation = await _context.Operations.FindAsync(id);
//            if (operation == null)
//            {
//                return NotFound();
//            }

//            _context.Operations.Remove(operation);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool OperationExists(Guid id)
//        {
//            return (_context.Operations?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
