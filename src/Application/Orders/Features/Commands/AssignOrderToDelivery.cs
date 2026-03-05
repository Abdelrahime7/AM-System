using Application.Common.Models;
using Domain.Entities;

namespace Application.Orders.Features.Commands;

public partial class OrderCommands
{
    public async Task <Result<bool>> AssignOrderToDelivery(Order order)
    {
        // it will be audited
        try
        {
            if (order.Customer.City == "Algiers")
                await _local.AssignAsync(order);
            else
                await _external.AssignAsync(order);
            return Result<bool>.Success(true);
        }
        catch (Exception ex) 
        { return Result<bool>.Failure("failed to Assign Order "+ ex.Message); }
    }

}