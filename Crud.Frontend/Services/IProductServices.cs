using Crud.Frontend.Models;

namespace Crud.Frontend.Services
{
    public interface IProductServices
    {
        Task<int> AddNewProduct(ProductModel productModel);
        Task<IList<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductById(int id);
        Task RemoveProduct(int id);
        Task<ProductModel> EditProduct(ProductModel productModel);
    }
}
