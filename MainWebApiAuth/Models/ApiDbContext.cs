using System;
using System.Collections.Generic;

namespace TokenApp.Models
{
    internal class ApiDbContext
    {
        public List<User> Users { get; internal set; }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}