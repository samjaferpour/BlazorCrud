using Crud.Frontend.Models;
using Newtonsoft.Json;
using System.Text;

namespace Crud.Frontend.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductServices(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<int> AddNewProduct(ProductModel productModel)
        {
            var client = _httpClientFactory.CreateClient("webapi");
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/products");
            var content = ContentMaker(productModel);
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                return int.Parse(stringResponse);
            }
            else
            {
                throw new Exception("Problem occured in add new product");
            }
        }

        public async Task<ProductModel> EditProduct(ProductModel productModel)
        {
            var client = _httpClientFactory.CreateClient("webapi");
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/products");
            var content = ContentMaker(productModel);
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ProductModel>(stringResponse);
                return result;
            }
            else
            {
                throw new Exception("Problem occured in edit product");
            }
        }

        public async Task<IList<ProductModel>> GetAllProducts()
        {
            var client = _httpClientFactory.CreateClient("webapi");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IList<ProductModel>>(stringResponse);
                return result;
            }
            else
            {
                throw new Exception("Problem occured in fetching products");
            }
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            var client = _httpClientFactory.CreateClient("webapi");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/products/{id}");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ProductModel>(stringResponse);
                return result;
            }
            else
            {
                throw new Exception("Problem occured in fetching the product");
            }
        }

        public async Task RemoveProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("webapi");
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/products/{id}");
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Problem occured in fetching the product");
            }
        }

        private StringContent ContentMaker(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}
