using System;
using Flunt.Validations;
using PaymentContext.Domain.ValeuObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entites
{
    public abstract class  Payment : Entity
    {
        protected Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Document document, Address address, Email email)
        {
            Number = Guid.NewGuid().ToString().Replace("-","").Substring(0, 10).ToUpper();
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
            Payer = payer;
            Document = document;
            Address = address;
            Email = email;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, Total, "Payment.Total", "O total não pode ser zero")
                .IsGreaterOrEqualsThan(Total, totalPaid, "Payment.TotalPaid", "O valor pago é menor que o valor do pagamento")
            );
        }

        /*Definindo propriedades dos pagamentos - Um aluno pode ter uma assinatura com o nome de outra
        pessoal que está pagando (por exemplo pagar com um cartão de outro alguem*/

        /*Numero de identificação do pagamento*/
        public string Number { get; private set; }

        /*Data do pagamento*/
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }

        /*Definindo dados de cobrança, pagador, documento do pagador, email e o endereço de cobraça*/
        public string Payer { get; private set; }

        public Document Document { get; private set; }
        
        public Address Address { get; private set; }
        public Email Email { get; set; }
    }
}