using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValeuObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;
        public StudentTests()
        {
            /*Criando um estudante e inscrição*/
             _name = new Name ("Bruce", "Wayne");
             _document = new Document ("35111507795", EdocumentType.Cpf);
             _email = new Email("batman@dc.com");
             _address = new Address ("Rua 1", "1234", "Bairro legal", "Gothan", "Sp", "BR", "13400123");
             _student = new Student(_name, _document, _email);
             _subscription = new Subscription(null);
        }
        [TestMethod]
        
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PaypalPayment("123456789", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne corp", _document, _address, _email);
            
            _subscription.AddPayment(payment);
            /*Adicionando 2 inscrições*/
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            /*Se a assinatura já existir, retornará invalido*/
            Assert.IsTrue(_student.Invalid);
        }

        /*retorna um erro se a assinatura não tem um pagamento*/
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        public void ShouldReturnSuccesWhenAddSubscription()
        {
            var payment = new PaypalPayment("123456789", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne corp", _document, _address, _email);
            
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Valid);
        }
/*--------------------------------------------------------------------------------------------------------*/ 

        //public void AdicionarAssinatura()
        //{
        //     /*Estanciou um novo objeto de inscrição*/
        //     var subscription = new Subscription(null);

        //     /*Instanciou um novo aluno*/
        //     var student = new Student("Cleiton", "Tito", "123456","tito@gmail.com");

        //     /*Fez a adicição de uma nova inscrição utilizando o método AddAddSubscription criado no student*/
        //     student.AddSubscription(subscription);

            /*Modelo para testar se volta uma notificação*/
            // var name = new Name("Teste", "Teste");
            // foreach (var not in name.Notifications)
            // {
            //     not.Message;
            // }
        //}
    }
}