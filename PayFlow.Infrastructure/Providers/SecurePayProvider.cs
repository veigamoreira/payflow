using PayFlow.Domain.Interfaces;
using PayFlow.Domain.ValueObjects;

namespace PayFlow.Infrastructure.Providers;

public class SecurePayProvider : IPaymentProvider
{
    public string Name => "SecurePay";

    public async Task<PaymentResult?> ProcessAsync(PaymentRequest paymentRequest)
    {
        if (paymentRequest.TransactionAmount > 0)
        {
            return new PaymentResult
            {
                TransactionId = "SP-19283",
                Result = "success"
            };
        }
        else if (paymentRequest.Amount > 0)
        {
            var fee = CalculateFee(paymentRequest);
            return new PaymentResult
            {
                Id ="2",
                ExternalId = "",
                Status = "approved",
                Provider = Name,
                GrossAmount = paymentRequest.Amount.Value,
                Fee = fee,
                NetAmount = paymentRequest.Amount.Value - fee
            };
        }
        else
            return null;
    }

    public decimal CalculateFee(PaymentRequest payment)
    {
        return Math.Round(payment.Amount.Value * 0.0299m + 0.40m, 2);
    }
}
