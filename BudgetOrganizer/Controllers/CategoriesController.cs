using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.CategoryModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace BudgetOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly BudgetOrganizerDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(BudgetOrganizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryDTO>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
          var categories = await _context.Categories.ToListAsync();

           return Ok(_mapper.Map< List < Category > ,List <GetCategoryDTO>>(categories));
        }

        [Authorize]
        [HttpGet]
        [Route("me")]
        public async Task<ActionResult<IEnumerable<GetCategoryDTO>>> GetCurrentAccountCategories()
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var claim = HttpContext.User.FindFirst("id");
            if (claim == null)
                return StatusCode(500);

            var accountId = new Guid(claim.Value);
            var account = await _context.Accounts.Where(e => e.Id == accountId).Include(o => o.Categories).FirstOrDefaultAsync();

            if (account == null)
                return Problem("Account doesn't exist");

            return Ok(_mapper.Map<List<Category>, List<GetCategoryDTO>>(account.Categories.ToList()));
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDTO>> GetCategory(Guid id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return _mapper.Map<GetCategoryDTO>(category);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(AddCategoryDTO categoryToAdd)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'BudgetOrganizerDbContext.Categories'  is null.");
          }
            var category = _mapper.Map<Category>(categoryToAdd);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok("Successfuly done");
        }

        [Authorize]
        [HttpPost]
        [Route("me")]
        public async Task<ActionResult> AddCategoryToCurrentUser(Guid categoryId)
        {
            if (_context.Users == null || _context.Categories == null)
                return Problem("Database error");

            var claim = HttpContext.User.FindFirst("id");
            if (claim == null)
                return StatusCode(500);

            var accountId = new Guid(claim.Value);
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
                return Problem("Account doesn't exsist");

            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
                return Problem("Category doesn't exist");

            category.Accounts.Add(account);
            account.Categories.Add(category);

            _context.Accounts.Update(account);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
