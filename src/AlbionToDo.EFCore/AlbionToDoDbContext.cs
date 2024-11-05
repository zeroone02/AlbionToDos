using AlbionToDo.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlbionToDo.EFCore;
public class AlbionToDoDbContext : DbContext
{
    public AlbionToDoDbContext(DbContextOptions<AlbionToDoDbContext> options)
          : base(options)
    {
    }

    public DbSet<ToDoTask> ToDoTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ToDoTask>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd(); 
        });
    }

}
