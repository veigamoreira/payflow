using PayFlow.Domain.Interfaces;

namespace PayFlow.Application.Interfaces;

public interface IPaymentProviderFactory
{
    IPaymentProvider Get(string name);
}
