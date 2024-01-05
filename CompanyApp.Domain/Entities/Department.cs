using CompanyApp.Domain.Entities.Common;

namespace CompanyApp.Domain.Entities;

public class Department:BaseEntity
{
    public Department()
    {
        Employees = new();
    }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public List<Employee> Employees { get; set; }


    public override string ToString()
    {
        return $"Name:{Name}  Capacity:{Capacity}/{Employees.Count}";
    }
}
