
using Medicine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Domain.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Medication> Medications { get; set; }

    public int SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
