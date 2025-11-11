using PayFlow.Domain.Interfaces;
using PayFlow.Domain.ValueObjects;

namespace PayFlow.Infrastructure.Providers;

public class FastPayProvider : IPaymentProvider
{
    public string Name => "FastPay";

    public async Task<PaymentResult?> ProcessAsync(PaymentRequest paymentRequest)
    {
        if (paymentRequest.AmountCents > 0)
        {
            return new PaymentResult
            {
                Id = "FP-884512",
                Status = "approved",
                StatusDetail = "Pagamento aprovado"
            };
        }
        else if (paymentRequest.Amount > 0)
        {
            var fee = CalculateFee(paymentRequest);

            return new PaymentResult
            {
                Id = "2",
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
        return Math.Round(payment.TransactionAmount.Value * 0.0349m, 2);
    }

}
