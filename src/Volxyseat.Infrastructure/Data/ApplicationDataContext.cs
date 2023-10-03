using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain;
using Volxyseat.Domain.Core.Data;
using Volxyseat.Domain.Models.ClientModel;

namespace Volxyseat.Infrastructure.Data
{
    public class ApplicationDataContext: DbContext, IUnitOfWork
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {}

        public DbSet<Client> Clients { get; set; }
    }
}
