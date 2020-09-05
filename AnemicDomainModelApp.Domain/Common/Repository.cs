using CSharpFunctionalExtensions;

using System.Threading.Tasks;

namespace AnemicDomainModelApp.Domain.Common
{
    public interface IRepository<T>
        where T : AggregateRoot
    {
        Task<Result<T>> GetById(int id);

        void Save(T aggrageteRoot);

        void Remove(T aggrageteRoot);
    }
}
