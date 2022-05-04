using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValeuObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {
        /*Metodologia Red, Green, Refactor*/
        /*Faz ele falhar, faz ele passar, e refatora o código*/
        [TestMethod]

        /*Primeiro teste deve retornar erro quando o cnpj for invalido*/
        public void ShouldReturnErrorWheCNPJisInvalid()
        {
            /*Passando um cnpj invalido*/
            var doc = new Document("123", EdocumentType.Cnpj);
            /*Garanta que é verdadeiro que meu doc é invalido*/
            Assert.IsTrue(doc.Invalid);
        }

        /*Segundo teste deve retornar sucesso quando o cnpj for valido*/
        public void ShouldReturnSuccessWheCNPJisValid()
        {
            var doc = new Document("34110468000150", EdocumentType.Cnpj);
            Assert.IsTrue(doc.Valid);
        }

        /*Primeiro teste deve retornar erro quando o cpf for invalido*/
        public void ShouldReturnErrorWheCPFisInvalid()
        {
            var doc = new Document("123", EdocumentType.Cpf);
            Assert.IsTrue(doc.Invalid);
        }

        /*Segundo teste deve retornar sucesso quando o cpf for valido*/
        public void ShouldReturnSuccessWheCPFisValid()
        {
            var doc = new Document("34101813850", EdocumentType.Cpf);
            Assert.IsTrue(doc.Valid);
        }
    }
}