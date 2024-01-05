namespace CompanyApp.Domain.Entities.Common;

public abstract class BaseEntity
{
    public BaseEntity()
    {
        CreatedTime= DateTime.Now;
    }
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
}
