using CompanyApp.Business.Interfaces;
using CompanyApp.DataContent.Repositories;
using CompanyApp.Domain.Entities;
using CompanyAppHelper;

namespace CompanyApp.Business.Services;

public class DepartmentService : IDepartmentService
{
    private readonly DepartmentRepository _departmentRepository;
    private readonly EmployeeRepository _employeeRepository;
    private static int _departmentCount;

    public DepartmentService()
    {
        _departmentRepository = new();
        _employeeRepository = new();
        _departmentCount = 1;
    }

    public void Create(Department department)
    {
        if (department.Name.Length < 3)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Ad minimum 3 simvol olmalidir");

            return;
        }
        var existDepartment = _departmentRepository.Get(d => d.Name.Trim().ToLower() == department.Name.Trim().ToLower());
        if (existDepartment is not null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Bu adda Department artiq movcuddur");
            return;
        }
        if (department.Capacity < 0)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Capacity menfi ola bilmez!");

            return;
        }
        department.Id = _departmentCount++;
        if (_departmentRepository.Create(department))
        {
            Helper.ChangeTextColor(ConsoleColor.Green, "Department ugurla yaradildi");
        }
        else
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Xeta yasandi yeniden cehd edin");

        }

    }

    public void Delete(int id)
    {
        var existedDepartment = _departmentRepository.Get(d => d.Id == id);
        var departments = _departmentRepository.GetAll();
        if (existedDepartment is null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Department movcud deyil");
            return;
        }
        if (departments.Count == 1)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Sonuncu department siline bilmez");
            return;

        }
        if (_departmentRepository.Delete(existedDepartment))
        {
            Helper.ChangeTextColor(ConsoleColor.Green, "Department ugurla silindi----" + existedDepartment.Name);

            foreach (Employee employee in existedDepartment.Employees)
            {
                employee.Department = departments.First();
                employee.DepartmentId = departments.First().Id;
            }

            return;
        }
        Helper.ChangeTextColor(ConsoleColor.Red, "Xeta yasandi yeniden cehd edin");

    }

    public Department Get(int id)
    {
        var department = _departmentRepository.Get(d => d.Id == id);
        if (department is null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Department movcud deyil");
            return null;
        }
        return department;
    }

    public List<Department> GetAll()
    {
        return _departmentRepository.GetAll();
    }

    public List<Department> GetAll(string name)
    {
        var departments= _departmentRepository.GetAll(x => x.Name.ToLower().Contains(name.Trim().ToLower()));
        if (departments.Count == 0)
        {
            Helper.ChangeTextColor(ConsoleColor.Yellow, "Axtardiginiz department tapilmadi");

        }
        return departments;
    }

    public void Update(Department department)
    {
        var existedDepartment = _departmentRepository.Get(x => x.Id == department.Id);
        if (existedDepartment is null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Department movcud deyil");
            return;

        }
        if (department.Name.Length < 3)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Ad minimum 3 simvol olmalidir");

            return;
        }
        var existName = _departmentRepository.Get(x => x.Name.ToLower().Trim().Contains(department.Name.Trim().ToLower()) && x.Id != department.Id);
        if (existName is not null)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Bu adda department artiq movcudddur");
            return;
        }
        if (department.Capacity < 0)
        {
            Helper.ChangeTextColor(ConsoleColor.Red, "Capacity menfi ola bilmez!");
            return;
        }
        if (_departmentRepository.Update(department))
        {
            Helper.ChangeTextColor(ConsoleColor.Yellow, "Department ugurla update olundu---" + department.Name);
            return;
        }

        Helper.ChangeTextColor(ConsoleColor.Red, "Xeta yasandi yeniden cehd edin");

    }

    public List<Employee> GetAllDepartmentsEmployees(int departmentId)
    {
        var department = Get(departmentId);
        List<Employee> employees = _employeeRepository.GetAll(x => x.Department == department);
        if (employees.Count == 0)
        {
            Helper.ChangeTextColor(ConsoleColor.Yellow, "Bu departmentde employee movcud deyil");
        }
        return employees;

    }
}
