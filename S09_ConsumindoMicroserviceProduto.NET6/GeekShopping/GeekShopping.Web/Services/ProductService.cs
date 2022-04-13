using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services {
    public class ProductService : IProductService {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient client) {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts() {
            var response = await _client.GetAsync(BasePath);
            return await response.ReatContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindProductById(long id) {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReatContentAs<ProductModel>();
        }

        public async Task<ProductModel> CreateProduct(ProductModel model) {
            var response = await _client.PostAsJson(BasePath, model);
            if (response.IsSuccessStatusCode)
                return await response.ReatContentAs<ProductModel>();
            else throw new Exception("Algo deu errado ao chamar API");
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model) {
            var response = await _client.PutAsJson(BasePath, model);
            if (response.IsSuccessStatusCode)
                return await response.ReatContentAs<ProductModel>();
            else throw new Exception("Algo deu errado ao chamar API");
        }

        public async Task<bool> DeleteProductById(long id) {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.ReatContentAs<bool>();
            else throw new Exception("Algo deu errado ao chamar API");
        }

    }
}
