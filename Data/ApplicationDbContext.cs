using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Client_Connect.DAL;
using Client_Connect.Models;

namespace Client_Connect.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Tables
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Employee> Employees { get; set; }

    // Database provider set in Program.cs
}
