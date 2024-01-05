using CompanyApp.Domain.Entities;

namespace CompanyApp.DataContent;

public static class DbContext
{
    static DbContext()
    {
        Departments = new List<Department>();
        Employees=new List<Employee>();
    }
    public static List<Department> Departments{ get; set; }
    public static List<Employee> Employees{ get; set; }
}
