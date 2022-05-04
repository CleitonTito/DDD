using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValeuObjects;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        /*Exemplo de teste do command*/
        public void ShouldReturnErrorWheNameisInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "";
            command.Validate();
            Assert.AreEqual(false, command.Valid);
        }

    }
}