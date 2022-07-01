namespace Library.Core.Services;

public interface IAuthorService
{
    Task<int> GetAuthorIdOrCreateAsync(string firstName, string lastname);
}