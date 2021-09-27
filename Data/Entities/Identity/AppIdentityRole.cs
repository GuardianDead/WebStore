using Microsoft.AspNetCore.Identity;
using System;

namespace WebStore.Data.Identity
{
    public class AppIdentityRole : IdentityRole<Guid>
    {
        public AppIdentityRole()
        {
        }
        public AppIdentityRole(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }
    }
}
