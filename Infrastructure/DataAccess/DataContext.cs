using Api.Base;
using Api.Books;
using Api.Libraries;
using Api.LibraryBook;
using Api.People;
using Api.PersonBook;
using Api.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;
            var baseEntity = (BaseEntity)entity.Entity;

            if (entity.State == EntityState.Added)
            {
                baseEntity.CreatedDate = now;
            }

            baseEntity.UpdatedDate = now;
        }
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<PersonBook> PersonBooks { get; set; }
    public DbSet<LibraryBook> LibraryBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity => 
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("books");

            entity.HasMany(e => e.Libraries)
                .WithMany(e => e.Books)
                .UsingEntity<LibraryBook>();

            entity.HasMany(e => e.People)
                .WithMany(e => e.Books)
                .UsingEntity<PersonBook>();
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("libraries"); 
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("people");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("users"); 
        });

        modelBuilder.Entity<LibraryBook>(entity =>
        {
            entity.HasKey(lb => new { lb.BookId, lb.LibraryId });
            entity.ToTable("library_books");
            
            entity.HasOne(lb => lb.Library)
                .WithMany(l => l.LibraryBooks)
                .HasForeignKey(lb => lb.LibraryId)
                .OnDelete(DeleteBehavior.Cascade); 

            entity.HasOne(lb => lb.Book)
                .WithMany(b => b.LibraryBooks)
                .HasForeignKey(lb => lb.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PersonBook>(entity =>
        {
            entity.HasKey(pb => new { pb.BookId, pb.PersonId });
            entity.ToTable("person_books");

            entity.HasOne(pb => pb.Person)
                .WithMany(p => p.PersonBooks)
                .HasForeignKey(pb => pb.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }); 
    }
}