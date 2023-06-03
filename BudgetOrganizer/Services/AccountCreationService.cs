using AutoMapper;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.AccountModel;
using BudgetOrganizer.Models.GroupModel;
using BudgetOrganizer.Models.RoleModel;
using Microsoft.AspNetCore.Mvc;

namespace BudgetOrganizer.Services
{
    public class AccountCreationService : IAccountCreationService
    {
        private readonly BudgetOrganizerDbContext _context;
        private readonly IMapper _mapper;

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
                var role = _context.Roles.Where(o => o.Name == "adult").FirstOrDefault();

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

            return account;
        }

        private void AddDefultCategories(Account account)
        {

        }


    }
}
