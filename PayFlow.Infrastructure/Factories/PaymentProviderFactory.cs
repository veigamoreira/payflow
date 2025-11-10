using Microsoft.Extensions.DependencyInjection;
using PayFlow.Application.Interfaces;
using PayFlow.Domain.Interfaces;
using PayFlow.Infrastructure.Providers;
namespace PayFlow.Infrastructure.Factories;

public class PaymentProviderFactory : IPaymentProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PaymentProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IPaymentProvider Get(string name)
    {
        return name switch
        {
            "FastPay" => _serviceProvider.GetRequiredService<FastPayProvider>(),
            "SecurePay" => _serviceProvider.GetRequiredService<SecurePayProvider>(),
            _ => throw new ArgumentException($"Provedor '{name}' não reconhecido.")
        };
    }
}
