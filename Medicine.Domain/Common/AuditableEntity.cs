
namespace Medicine.Domain.Common;

public abstract class AuditableEntity : AggregateRoot
{
    public DateTime CreationDate { get; set; }
    public string CreatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}
