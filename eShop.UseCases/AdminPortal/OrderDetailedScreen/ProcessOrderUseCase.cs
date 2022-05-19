﻿namespace eShop.UseCases.AdminPortal.OrderDetailedScreen;

public class ProcessOrderUseCase : IProcessOrderUseCase
{
    private readonly IOrderRepositoryUI _orderRepoUI;
    private readonly IOrderService _orderService;

    public ProcessOrderUseCase(IOrderRepositoryUI orderRepoUI, IOrderService orderService)
    {
        _orderRepoUI = orderRepoUI;
        _orderService = orderService;
    }

    public async Task<bool> ExecuteAsync(string url, int orderId, string adminUserName)
    {
        var order = await _orderRepoUI.GetbyId(url, orderId);
        order.AdminUser = adminUserName;
        order.DateProcessed = DateTime.Now;

        if (!_orderService.ValidateUpdateOrder(order))
        {
            return await _orderRepoUI.Update(url, order, orderId);
        }

        return false;
    }
}
