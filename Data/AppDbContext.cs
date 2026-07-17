using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options): DbContext(options)
    {
        public DbSet<Character> Characters => Set<Character>();
    }
}