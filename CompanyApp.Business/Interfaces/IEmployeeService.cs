using CompanyApp.Domain.Entities;

namespace CompanyApp.Business.Interfaces;

public interface IEmployeeService
{
    List<Employee> GetAll();
    List<Employee> GetAll(string name);
    Employee Get(int id);
    Employee GetByAddress(string email);
    void Delete(int id);
    void Update(Employee Employee);
    void Create(Employee Employee);
}
