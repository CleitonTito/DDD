using Flunt.Validations;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValeuObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;


            /*Essa validação utilizando a notificação do flunt, onde o contrado requer (Requires)
             que seja um email (IsEmail), passando a propriedade (Adrress) e a menssagem de erro*/
            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Address, "Email.Address", "E-mail inválido")
            );
            
        }

        public string Address { get; private set; }
    }
}