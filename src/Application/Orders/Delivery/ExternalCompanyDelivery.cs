using Application.Delivery.DTOs;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Net.Http.Json;

namespace Application.Orders.Delivery
{
    public class ExternalCompanyDeliveryStrategy : IExternallDeliverStrategy
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


        /// <summary>
        /// Assigns the given order to an external delivery company by invoking its API.
        /// </summary>
        /// <param name="order">The order to be assigned for delivery.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no active delivery integration is available or the API endpoint is missing.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the order lacks required customer or item details.
        /// </exception>
        /// <remarks>
        /// This method performs the following steps:
        /// <list type="number">
        ///   <item>Retrieves the delivery integration configuration.</item>
        ///   <item>Validates the presence of API credentials and endpoint.</item>
        ///   <item>Constructs an HTTP client with required headers.</item>
        ///   <item>Builds a payload containing order, customer, and customization data.</item>
        ///   <item>Sends the payload via POST to the external API.</item>
        ///   <item>Parses the response and updates the order with delivery metadata.</item>
        /// </list>
        /// </remarks>

        public async Task AssignAsync(Order order)
        {

           

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
