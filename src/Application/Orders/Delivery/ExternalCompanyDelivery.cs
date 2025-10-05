using Application.Delivery.DTOs;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Net.Http.Json;

namespace Application.Orders.Delivery
{
    public class ExternalCompanyDeliveryStrategy : IDeliveryStrategy
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDeliveryRepository _integrationRepository;

        public ExternalCompanyDeliveryStrategy(
            IHttpClientFactory httpClientFactory,
            IDeliveryRepository integrationRepository)
        {
            _httpClientFactory = httpClientFactory;
            _integrationRepository = integrationRepository;
        }

        public async Task AssignAsync(Order order)
        {

            // this will be updated later 

            var integration = await _integrationRepository.GetByIdAsync(1);
            if (integration == null || string.IsNullOrWhiteSpace(integration.ApiEndpoint))
                throw new InvalidOperationException("No active delivery integration available.");

            if (order.Customer == null || order.OrderDetails == null)
                throw new ArgumentException("Order is missing required customer or item details.");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", integration.ApiKey ?? "");
            client.DefaultRequestHeaders.Add("X-Api-Secret", integration.ApiSecret ?? "");

            var payload = new
            {
                OrderId = order.Id,
                Customer = new
                {
                    Name = order.Customer.FullName,
                    Address = order.Customer.Address.ToString()
                },
                Items = order.OrderDetails,
                order.Customizations
            };

            var response = await client.PostAsJsonAsync(integration.ApiEndpoint, payload);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<DeliveryIntegrationResponse>();
            order.DeliveryCompanyId = integration.Id;
        }

    }

}
