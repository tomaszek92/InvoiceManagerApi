using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Models
{
    public interface IApplicationDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceRow> InvoiceRows { get; set; }
        DatabaseFacade Database { get; }

        EntityEntry Entry([NotNull] object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
