using Microsoft.EntityFrameworkCore;
using SmartSaveApp.Core.Entities;
using SmartSaveApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSaveApp.Infrastructure.Data
{
    public class SmartSaveDbContext : DbContext
    {
        public SmartSaveDbContext(DbContextOptions<SmartSaveDbContext> options)
        : base(options)
        {
        }
    }
}
