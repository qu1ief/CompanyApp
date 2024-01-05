using CompanyApp.Domain.Entities.Common;

namespace CompanyApp.Domain.Entities;

public class Employee:BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public byte Age { get; set; }
    public string Address { get; set; }
    public Department Department { get; set; }
    public int DepartmentId { get; set; }


    public override string ToString()
    {
        return $"Name:{Name}  Surname:{Surname} {Age}";
    }
}
