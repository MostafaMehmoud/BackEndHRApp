
using HrApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrApp.DAL.Data;

public class HrAppDbContext :DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RequestVacation> RequestVacations { get; set; }
    public DbSet<RequestAdvancePayment> RequestAdvancePayments { get; set; }
    public DbSet<AdvancePaymentTotal> AdvancePaymentTotals { get; set; }
    public DbSet<RequestCustody> RequestCustodies { get; set; }
    public DbSet<RequestLetter> RequestLetters { get; set; }
    public DbSet<RequestPermission> RequestPermissions { get; set; }
    public DbSet<UploadFile> UploadFiles { get; set; }
    public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
    public DbSet<AuthLog> AuthLogs { get; set; }
    public DbSet<EmployeeImage> EmployeeImages { get; set; }
    public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
    public DbSet<EmployeeTree> EmployeesTree { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Manage> Manages { get; set; }

    public DbSet<Company> Companys { get; set; }    
    public HrAppDbContext()
    {
        
    }
    public HrAppDbContext(DbContextOptions<HrAppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RequestAdvancePayment>()
            .HasOne(m=>m.Employee)
            .WithMany(m=>m.RequestAdvancePayments)
            .HasForeignKey(m =>m.EmployeeId);
        modelBuilder.Entity<RequestVacation>()
           .HasOne(m => m.Employee)
           .WithMany(m => m.RequestVacations)
           .HasForeignKey(m => m.EmployeeId);

        modelBuilder.Entity<EmployeeAttendance>()
          .HasOne(m => m.Employee)
          .WithMany(m => m.EmployeeAttendances)
          .HasForeignKey(m => m.EmployeeId);
        base.OnModelCreating(modelBuilder);
    }
}
