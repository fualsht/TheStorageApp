using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Models
{
    public class AppRole
    {
        public bool IsSelected { get; set; } = false;
        public string ConcurrencyStamp { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        AppUser[] RoleUsers { get; set; }
    }
}
