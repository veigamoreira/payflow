using Microsoft.Extensions.DependencyInjection;
using Moq;
using PayFlow.Domain.Interfaces;
using PayFlow.Infrastructure.Factories;
using PayFlow.Infrastructure.Providers;

namespace PayFlow.Tests;

public class PaymentProviderFactoryTests
{
    [Fact]
    public void DeveSelecionarFastPay_ParaValoresMenoresQue100()
    {
        var services = new ServiceCollection();

        // Registra dependências reais
        services.AddScoped<IPaymentProvider, FastPayProvider>();
        services.AddScoped<IPaymentProvider, SecurePayProvider>();
        services.AddScoped<FastPayProvider,>();

        var securePay = new Mock<IPaymentProvider>();
        var service = new Mock<IServiceProvider>();
        securePay.Setup(p => p.Name).Returns("FastPay");

        var factory = new PaymentProviderFactory(service.Object);

        var provider = factory.Get("FastPay");
        provider.ProcessAsync(new PaymentRequest { AmountCents = 199.99m });

        Assert.Equal("FastPay", provider.Name);
    }

    [Fact]
    public void DeveSelecionarSecurePay_ParaValoresMaioresOuIguaisA100()
    {
        var securePay = new Mock<IPaymentProvider>();
        var service = new Mock<IServiceProvider>();
        securePay.Setup(p => p.Name).Returns("SecurePay");

        var factory = new PaymentProviderFactory(service.Object);

        var provider = factory.Get("SecurePay");
        provider.ProcessAsync(new PaymentRequest { AmountCents = 199.99m });

        Assert.Equal("SecurePay", provider.Name);
    }

    public class PaymentsControllerTests
    {
        [Fact]
        public async Task DeveRetornarRespostaCompleta_UsandoSecurePay()
        {
            var provider = new Mock<IPaymentProvider>();
            provider.Setup(p => p.Name).Returns("SecurePay");
            provider.Setup(p => p.ProcessAsync(It.IsAny<PaymentRequest>()))
                    .ReturnsAsync(("SP-19283", "approved"));
            provider.Setup(p => p.CalculateFee(120.50m)).Returns(4.01m);

            var factory = new PaymentProviderFactory(new[] { provider.Object });
            var controller = new PaymentsController(factory);

            var input = new PaymentInput { Amount = 120.50m, Currency = "BRL" };
            var result = await controller.Post(input) as OkObjectResult;

            var response = result?.Value as PaymentResponse;

            Assert.NotNull(response);
            Assert.Equal("SecurePay", response.Provider);
            Assert.Equal("SP-19283", response.ExternalId);
            Assert.Equal(4.01m, response.Fee);
            Assert.Equal(116.49m, response.NetAmount);
        }
    }
}
