using Blog.Domain.Repositories.UOW;
using Blog.Infrastructure.DataAccess;

namespace Blog.Infrastructure.Repositories;

public class UnitOfWork(DataContext context) : IUnitOfWork {

    public async Task Commit() {
        await context.SaveChangesAsync();
    }
}