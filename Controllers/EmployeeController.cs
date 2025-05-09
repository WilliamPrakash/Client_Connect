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

        public IActionResult EditEmployeeView(int id)
        {
            using (_dbContext)
            {
                Employee employeeToEdit = _dbContext.Employees.Single(employee => employee.Id == id);
                return View("EditEmployee", employeeToEdit);
            }
        }
        #endregion

        #region CRUD actions
        public IActionResult EditEmployee(Employee editedEmployee)
        {
            using (_dbContext)
            {
                Employee employeeToEdit = _dbContext.Employees.Single(employee => employee.Id == editedEmployee.Id);
                if (editedEmployee != null)
                {
                    employeeToEdit.Name = !string.IsNullOrEmpty(editedEmployee.Name)  ? editedEmployee.Name : employeeToEdit.Name;
                    employeeToEdit.Email = !string.IsNullOrEmpty(editedEmployee.Email) ? editedEmployee.Email : employeeToEdit.Email;
                    employeeToEdit.Occupation = !string.IsNullOrEmpty(editedEmployee.Occupation) ? editedEmployee.Occupation : employeeToEdit.Occupation;
                    /* ERROR
                    System.InvalidOperationException: 'The instance of entity type 'Employee' cannot be tracked because
                    another instance with the same key value for {'Id'} is already being tracked. When attaching existing
                    entities, ensure that only one entity instance with a given key value is attached. Consider using
                    'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.' 
                    _dbContext.Employees.Update(editedEmployee);*/
                    _dbContext.SaveChanges();
                }

                return RedirectToAction("Index"); // returns the view with the same name as the method if no specific name is supplied
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
