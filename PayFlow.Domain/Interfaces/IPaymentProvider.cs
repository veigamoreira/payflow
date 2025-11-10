using PayFlow.Domain.ValueObjects;

namespace PayFlow.Domain.Interfaces;

public interface IPaymentProvider
{
    string Name { get; }
    Task<PaymentResult> ProcessAsync(PaymentRequest payment);
    decimal CalculateFee(PaymentRequest payment);
}
