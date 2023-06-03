﻿using BudgetOrganizer.Models.AccountModel;

namespace BudgetOrganizer.Models.GroupModel
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public List<GetAccountDTO> Accounts { get; }
    }
}
