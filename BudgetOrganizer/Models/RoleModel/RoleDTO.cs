using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.RoleModel
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
