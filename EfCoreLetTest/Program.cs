using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

const string connectionString = @"Data Source=mydb.db";

var optionsBuilder = new DbContextOptionsBuilder<ApplicationDatabaseContext>();
optionsBuilder.UseSqlite(connectionString);
optionsBuilder.LogTo(Console.WriteLine);

using var dbContext = new ApplicationDatabaseContext(optionsBuilder.Options);
if (dbContext is null)
{
    throw new NullReferenceException($"{nameof(dbContext)} is null!");
}

dbContext.Database.EnsureCreated();

if (!dbContext.ToDos.Any())
{
    dbContext.ToDos.Add(new ToDo(1, "Test Task 1"));
    dbContext.ToDos.Add(new ToDo(2, "Test Task 2"));
    dbContext.ToDos.Add(new ToDo(3, "Test Task 3"));
    await dbContext.SaveChangesAsync();
}

var query = from t in dbContext.ToDos
            let todoTitle = t.TaskTitle
            where todoTitle == "Test Task 2"
            select t;

foreach (var todo in query.ToArray())
{
    Console.WriteLine(todo);
}

public record ToDo([property: Key] long Id, string TaskTitle);

public class ApplicationDatabaseContext : DbContext
{
    public ApplicationDatabaseContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<ToDo> ToDos { get; set; }
}
