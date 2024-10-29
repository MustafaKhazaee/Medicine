
namespace Medicine.Domain.Common;

public abstract class AggregateRoot : IEquatable<AggregateRoot?>
{
    public int Id { get; set; }

    public override bool Equals(object? obj) => Equals(obj as AggregateRoot);

    public bool Equals(AggregateRoot? other) => other is not null && Id == other.Id;

    public override int GetHashCode() => Id;

    public static bool operator ==(AggregateRoot? left, AggregateRoot? right)
        => EqualityComparer<AggregateRoot>.Default.Equals(left, right);

    public static bool operator !=(AggregateRoot? left, AggregateRoot? right) => !(left == right);
}
