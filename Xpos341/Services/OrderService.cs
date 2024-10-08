using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class OrderService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";

        private VMResponse response = new VMResponse();

        public OrderService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }
    }
}
