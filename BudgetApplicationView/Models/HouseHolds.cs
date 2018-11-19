﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetApplicationView.Models
{
    public class HouseHolds
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator{ get; set; }

        public HouseHolds()
        {
            HouseHoldInvites = new HashSet<HouseHoldInvites>();
            Accounts = new HashSet<Accounts>();
            Categories = new HashSet<Categories>();
            HouseHoldUser = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<HouseHoldInvites> HouseHoldInvites { get; set; }
        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<ApplicationUser> HouseHoldUser { get; set; }
    }
}