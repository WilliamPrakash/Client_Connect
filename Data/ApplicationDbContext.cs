using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Project.DAL;
using MVC_Project.Models;

namespace Client_Connect.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Tables
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Employee> Employees { get; set; }

    // Database provider
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        DatabaseConnect databaseConnect = new DatabaseConnect();
        optionsBuilder.UseSqlServer(databaseConnect.sqlConnStr);
    }*/

}
