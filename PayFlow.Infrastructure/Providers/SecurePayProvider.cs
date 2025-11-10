using PayFlow.Domain.Interfaces;
using PayFlow.Domain.ValueObjects;

namespace PayFlow.Infrastructure.Providers;

public class SecurePayProvider : IPaymentProvider
{
    public string Name => "SecurePay";

    public async Task<PaymentResult?> ProcessAsync(PaymentRequest payment)
    {
        if (payment.TransactionAmount > 0)
        {
            return new PaymentResult
            {
                TransactionId = "SP-19283",
                Result = "success"
            };
        }
        else if (payment.Amount > 0)
        {
            var fee = Math.Round(payment.Amount.Value * 0.0299m + 0.40m, 2);
            return new PaymentResult
            {
                Id ="2",
                ExternalId = "",
                Status = "approved",
                Provider = Name,
                GrossAmount = payment.Amount.Value,
                Fee = fee,
                NetAmount = payment.Amount.Value - fee
            };
        }
        else
            return null;
    }
}
