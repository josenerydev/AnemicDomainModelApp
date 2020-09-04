using CSharpFunctionalExtensions;

using System.Threading.Tasks;

namespace AnemicDomainModelApp.Domain
{
    public sealed class ProductRepository
    {
        private readonly WmsContext _context;

        public ProductRepository(WmsContext context)
        {
            _context = context;
        }

        public async Task<Result<Product>> GetById(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
                return Result.Failure<Product>("Product not found");

            await _context.Entry(product).Collection(x => x.Packaging).LoadAsync();

            return Result.Success(product);
        }

        public void Save(Product product)
        {
            _context.Products.Attach(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
