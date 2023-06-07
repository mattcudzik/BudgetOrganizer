using AutoMapper;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.AccountModel;
using BudgetOrganizer.Models.CategoryModel;
using BudgetOrganizer.Models.GroupModel;
using BudgetOrganizer.Models.RoleModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetOrganizer.Services
{
	public class AccountCreationService : IAccountCreationService
	{
		private readonly BudgetOrganizerDbContext _context;
		private readonly IMapper _mapper;
		private readonly String[] defaultCategories = { "Zakupy", "Rachunki", "Transport", "Rozrywka i wypoczynek", "Zdrowie", "Edukacja", "Dzieci", "Inne", "Kieszonkowe", "Emerytura", "Sprzedaż", "Wynagrodzenie" };

		public AccountCreationService(BudgetOrganizerDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Account> CreateNewAccount(AddAccountDTO addAccountDTO)
		{
			//Create and add to database new Account object
			//We use automapping (AccountMappingProfiles) to write one line of code instead of many:
			Account account = _mapper.Map<Account>(addAccountDTO);

			//Default role is adult
			if (addAccountDTO.RoleId == null)
			{
				var role = await _context.Roles.Where(o => o.Name == "adult").FirstOrDefaultAsync();

				//If there is no adult role create one 
				if (role == null)
				{
					role = new Role() { Id = Guid.NewGuid(), Name = "adult" };
					await _context.Roles.AddAsync(role);
					await _context.SaveChangesAsync();
				}

				account.RoleId = role.Id;
				account.Role = role;
			}
			else
			{
				var role = await _context.Roles.FindAsync(addAccountDTO.RoleId);

				if (role == null)
				{
					throw new BadHttpRequestException("Incorrect RoleId");
				}
				   
				account.Role = role;
			}

			Group? group;
			//Default group is new
			if (addAccountDTO.GroupId == null)
			{
				group = new Group();
				await _context.Groups.AddAsync(group);
				await _context.SaveChangesAsync();

				account.Group = group;
				account.GroupId = group.Id;
			}
			else
			{
				group = await _context.Groups.FindAsync(addAccountDTO.GroupId);
				if (group == null)
					throw new BadHttpRequestException("Incorrect GroupId");
				account.Group = group;
			}

			await AddDefultCategories(account);

            return account;
		}

		private async Task AddDefultCategories(Account account)
		{
			int i = 0;
			foreach(var categoryName in defaultCategories)
			{
				var category = await _context.Categories.SingleOrDefaultAsync(o=>o.Name==categoryName);
				if(category == null)
				{
					var color = ColorFromHSV(i * (double)360 / defaultCategories.Length, 0.9, 0.9);
					category = new Category()
					{
						Name = categoryName,
						Id = new Guid(),
						Color = color
					};

					await _context.Categories.AddAsync(category);
					await _context.SaveChangesAsync();
				}

				account.Categories.Add(category);
				i++;
			}
		}

		//hue 0-360 
		private string ColorFromHSV(double hue, double saturation, double value)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value = value * 255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

			if (hi == 0)
				return v.ToString("X2") + t.ToString("X2") + p.ToString("X2");
			else if (hi == 1)
				return q.ToString("X2") + v.ToString("X2") + p.ToString("X2");
			else if (hi == 2)	  
				return p.ToString("X2") + v.ToString("X2") + t.ToString("X2");
			else if (hi == 3)	  
				return p.ToString("X2") + q.ToString("X2") + v.ToString("X2");
			else if (hi == 4)	  
				return t.ToString("X2") + p.ToString("X2") + v.ToString("X2");
			else				  
				return v.ToString("X2") + p.ToString("X2") + q.ToString("X2");
		}

	}
}
