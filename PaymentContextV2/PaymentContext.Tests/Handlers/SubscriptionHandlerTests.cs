using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValeuObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]

        public void ShoudReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "hello@balta.io1";
            command.BarCode = "123456789";
            command.BoletoNumber = "123454987";
            command.PaymentNumber = "123121";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Wayne Corp";
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EdocumentType.Cpf;
            command.PayerEmail = "batman@dc.com";
            command.Street = "asdas";
            command.Number = "asdd";
            command.Neighborhood = "asdfdre";
            command.City = "sa";
            command.State = "sa";
            command.Country = "as";
            command.ZipCode = "987654321";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
        
     
    }
}