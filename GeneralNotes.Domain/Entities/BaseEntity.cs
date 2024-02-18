namespace GeneralNotes.Domain.Entities;
public class BaseEntity
{
    public long Id { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
}
