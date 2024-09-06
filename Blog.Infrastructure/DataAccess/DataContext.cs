using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.DataAccess;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options) {
    public DbSet<User> Users { get; set; }
}