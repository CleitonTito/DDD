using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entites
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            /*Iniciamos esse construtor com a data de assinatura padrão do dia e ativo sempre como true,
            assim que recebe o pagamento*/
            CreateDate = DateTime.Now;
            LasUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            _payments = new List<Payment>();
        }

        /*Definindo as propriedades da assinatura do aluno, data de criação, ultima atualização,
        uma data de expiração da assinatura, e uma validação se ele está ativo ou não, pois ele só pode
        ter uma assinatura por vez*/
        public DateTime CreateDate { get; private set; }
        public DateTime LasUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }

        public bool Active { get; private set; }
        
        /*Definindo uma lista de pagamento*/
        public IReadOnlyCollection<Payment> Payments { get {return _payments.ToArray();} }

        /*Criando método para pagamento*/
        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", 
                "A data do pagamento deve ser futura")
            );
            _payments.Add(payment);
        }
        
        /*Método que verifica se está ativo*/
        public void Activate()
        {
            Active = true;
            LasUpdateDate = DateTime.Now;
        }

        /*Método que verifica se está Inativo*/
        public void Inactivate()
        {
            Active = false;
            LasUpdateDate = DateTime.Now;
        }
        
    }
}