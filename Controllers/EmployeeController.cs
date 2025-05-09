using Client_Connect.Data;
using Client_Connect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Client_Connect.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        /* Constructor gets called every time we access an action on the controller.
        Seems like a potential performance hit? */
        public EmployeeController(ApplicationDbContext context)
        {
            // ToDo: error handling around context?
            _dbContext = context;
        }

        #region Views
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            using (_dbContext)
            {
                employees = _dbContext.Employees.ToList();
                return View(employees);
            }
        }

        public IActionResult AddEditEmployeeView(int? id)
        {
            using (_dbContext)
            {
                if (id != null)
                {
                    Employee employeeToEdit = _dbContext.Employees.Single(employee => employee.Id == id);
                    return View("AddEditEmployee", employeeToEdit);
                }
                else
                {
                    return View("AddEditEmployee");
                }
            }
        }
        #endregion

        #region CRUD actions
        public IActionResult AddEditEmployee(Employee employee)
        {
            using (_dbContext)
            {
                if (employee.Id != 0) // Editing an existing Employee
                {
                    Employee employeeToEdit = _dbContext.Employees.Single(emp => emp.Id == employee.Id);
                    if (employee != null)
                    {
                        employeeToEdit.Name = !string.IsNullOrEmpty(employee.Name) ? employee.Name : employeeToEdit.Name;
                        employeeToEdit.Email = !string.IsNullOrEmpty(employee.Email) ? employee.Email : employeeToEdit.Email;
                        employeeToEdit.Occupation = !string.IsNullOrEmpty(employee.Occupation) ? employee.Occupation : employeeToEdit.Occupation;
                        _dbContext.SaveChanges();
                    }

                    return RedirectToAction("Index"); // returns the view with the same name as the method if no specific name is supplied
                }
                else // Adding a new Employee
                {
                    _dbContext.Add(employee);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
        }

        public IActionResult DeleteEmployee(int id)
        {
            using (_dbContext)
            {
                // What if the Employees table has no employee matching the provided Id?
                // Well then there wouldn't be an employee record showing in the view in the first place.
                // Should I still consider it a possibility from an error-handling perspective?
                Employee employeeToDelete = _dbContext.Employees.Single(employee => employee.Id == id);
                _dbContext.Employees.Remove(employeeToDelete);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        #endregion

    }
}
