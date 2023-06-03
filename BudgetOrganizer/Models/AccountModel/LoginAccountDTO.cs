using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOrganizer.Models.CategoryModel;
using BudgetOrganizer.Models.OperationModel;
using BudgetOrganizer.Models.RoleModel;
using Microsoft.AspNetCore.Identity;

namespace BudgetOrganizer.Models.AccountModel
{
    public class LoginAccountDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
