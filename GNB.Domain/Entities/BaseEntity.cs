namespace GNB.Domain.Entities;

public abstract class BaseEntity
{
     public Guid Id { get; } = Guid.NewGuid();
     public DateTime DateCreated { get; } = DateTime.UtcNow;
     public DateTime DateModified { get; set; } = DateTime.UtcNow;
}