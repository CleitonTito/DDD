using System;
using PaymentContext.Domain.ValeuObjects;

namespace PaymentContext.Domain.Entites
{
    public class BoletoPayment : Payment
    {
        public BoletoPayment(
            string barCode, 
            string boletoNumber, 
            DateTime paidDate, 
            DateTime expireDate, 
            decimal total, 
            decimal totalPaid, 
            string payer, 
            Document document, 
            Address address, 
            Email email): 
                base (paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }

        /*Código de barras*/
        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }   
    }

}