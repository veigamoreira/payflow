using PayFlow.Application.Interfaces;
using PayFlow.Domain.ValueObjects;

namespace PayFlow.Application.Services;

public class PaymentService
{
    private readonly IPaymentProviderFactory _factory;

    public PaymentService(IPaymentProviderFactory factory)
    {
        _factory = factory;
    }

    public async Task<PaymentResult> ExecuteAsync(PaymentRequest payment)
    {
        var primaryName = "";
        var fallbackName = "";


        if (payment.TransactionAmount >= 100 || payment.Amount >= 100)
        {
            primaryName = "SecurePay";
            fallbackName = "FastPay";
        }
        else
        {
            primaryName = "FastPay";
            fallbackName = "SecurePay";
        }

        try
        {
            var primary = _factory.Get(primaryName);
            return await primary.ProcessAsync(payment);
        }
        catch
        {
            var fallback = _factory.Get(fallbackName);
            return await fallback.ProcessAsync(payment);
        }
    }
}
