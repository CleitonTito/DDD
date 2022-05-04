using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

/*(perguntar)*/

namespace PaymentContext.Domain.Commands
{
    /*Serializador newto soft.json serializer (pesquisar o que é)*/
    public class CreateBoletoSubscriptionCommand : Notifiable, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string BarCode { get; set; }
        public string BoletoNumber { get; set; }   
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public EdocumentType PayerDocumentType { get; set; }
        public string PayerEmail { get; set; }
        public string Street { get;  set; }
        public string Number { get;  set; }
        public string Neighborhood { get;  set; }
        public string City { get;  set; }
        public string State { get;  set; }
        public string Country { get;  set; }
        public string ZipCode { get;  set; }

        /*Melhor utilizar a validação aqui ou no Objeto de valor*/
        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3 , "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
                .HasMinLen(LastName, 3 , "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(FirstName, 40, "Name.FirstName", "Nome deve conter até 40 caracteres")
            ); 
        }
    }
}