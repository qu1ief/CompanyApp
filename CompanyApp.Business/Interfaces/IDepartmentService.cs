using CompanyApp.Domain.Entities;

namespace CompanyApp.Business.Interfaces;

public interface IDepartmentService
{
    List<Department> GetAll();
    List<Department> GetAll(string name);
    List<Employee> GetAllDepartmentsEmployees(int departmentId);
    Department Get(int id);
    void Delete(int id);    
    void Update(Department department);
    void Create(Department department);

}
