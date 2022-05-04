using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValeuObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EdocumentType type)
        {
            Number = number;
            Type = type;

            /*Se não for validado, irá retornar a msg Documento inválido*/
            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Validate(), "Document.Number", "Documento inválido")
            );
        }

        public string Number { get; private set; }
        public EdocumentType Type { get; private set; }

        /* método validando se cnpj tem a quantia certa de caracter (o mesmo para cpf)*/
        private bool Validate()
        {
            if (Type == EdocumentType.Cnpj && Number.Length == 14)
            {
                return true;
            }
            if (Type == EdocumentType.Cpf && Number.Length == 11)
            {
                return true;
            }

            return false;
        }
    }
}