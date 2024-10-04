using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class ProductService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse response = new VMResponse();
        public ProductService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }
    }

}
