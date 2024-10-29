
namespace Medicine.Domain.Common;

public abstract class AuditableEntity : AggregateRoot
{
    public DateTime CreationDate { get; set; }
    public required string CreatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}
