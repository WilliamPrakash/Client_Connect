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
        #endregion

        #region CRUD actions
        public IActionResult EditEmployee(int id)
        {
            using (_dbContext)
            {
                Employee employeeToEdit = _dbContext.Employees.Single(employee => employee.Id == id);
                // TODO create EditEmployee view
                return View(employeeToEdit); // returns the view with the same name as the method
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
            return View("Index");
        }
        #endregion

    }
}
