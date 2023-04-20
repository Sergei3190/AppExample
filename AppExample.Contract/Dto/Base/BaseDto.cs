using AppExample.Contract.Dto.Base.Interfaces;

namespace AppExample.Contract.Dto.Base;

public abstract class BaseDto : IBaseDto, IEquatable<BaseDto>
{
    public Guid Id { get; set; }

    public bool Equals(BaseDto? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return base.Equals((BaseDto)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(BaseDto? left, BaseDto? right) => Equals(left, right);
    public static bool operator !=(BaseDto? left, BaseDto? right) => !Equals(left, right);
}