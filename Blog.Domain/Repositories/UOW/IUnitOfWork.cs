namespace Blog.Domain.Repositories.UOW;

public interface IUnitOfWork {
    public Task Commit();
}