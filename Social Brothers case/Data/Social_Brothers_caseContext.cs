using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social_Brothers_case.Models;

namespace Social_Brothers_case.Data
{
    public class Social_Brothers_caseContext : DbContext
    {
        public Social_Brothers_caseContext (DbContextOptions<Social_Brothers_caseContext> options)
            : base(options)
        {
        }

        public DbSet<Social_Brothers_case.Models.adress> adress { get; set; } = default!;
    }
}
