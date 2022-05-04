using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValeuObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : 
    Notifiable,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>
    {
        /*para o Handle funcionar precisa ser passado o repository (independente de qual seja)*/
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService; 
        }

        /*Espera um comando de saída "ICommandResult", e um comando de entrada "command"*/
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            /*Fail - Fast - Validations*/
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            /*Verificar se Documento já está cadastrado*/
            if (_repository.DocumentExists(command.Document))
            {
                AddNotifications(command);
                    return new CommandResult(false, "Este CPF já está em uso");
            }

            /*Verificar se Email já está cadastrado*/
            if (_repository.EmailExistis(command.Email))
            {
                AddNotifications(command);
                    return new CommandResult(false, "Este Email já está em uso");
            }
            
            /*Gerar os objetos de valor (v.o.s)*/
            var name = new Name (command.FirstName, command.LastName);
            var document = new Document (command.Document, EdocumentType.Cpf);
            var email = new Email(command.Email);
            var address = new Address (command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            /*Gerar as entidades*/
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email
            );

            /*Relacionamentos*/
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            /*Agrupar as validações*/
            AddNotifications(name, document, email, address, student, subscription, payment);

            /*Salvar as infos*/
            _repository.CreateSubscription(student);

            /*Checar as notificações*/
            if(Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            /*Enviar e-mail de boas vindas*/
            _emailService.Send(student.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");

            /*Retorno das informações*/
                return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {

            /*Verificar se Documento já está cadastrado*/
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            /*Verificar se Email já está cadastrado*/
            if (_repository.EmailExistis(command.Email))
                AddNotification("Email", "Este Email já está em uso");
            
            /*Gerar os objetos de valor (v.o.s)*/
            var name = new Name (command.FirstName, command.LastName);
            var document = new Document (command.Document, EdocumentType.Cpf);
            var email = new Email(command.Email);
            var address = new Address (command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            /*Gerar as entidades*/
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PaypalPayment(
                command.TransactionCode,
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email
            );

            /*Relacionamentos*/
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            /*Agrupar as validações*/
            AddNotifications(name, document, email, address, student, subscription, payment);

            /*Salvar as infos*/
            _repository.CreateSubscription(student);

            /*Enviar e-mail de boas vindas*/
            _emailService.Send(student.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");

            /*Retornar informações*/
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            /*Verificar se Documento já está cadastrado*/
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            /*Verificar se Email já está cadastrado*/
            if (_repository.EmailExistis(command.Email))
                AddNotification("Email", "Este Email já está em uso");
            
            /*Gerar os objetos de valor (v.o.s)*/
            var name = new Name (command.FirstName, command.LastName);
            var document = new Document (command.Document, EdocumentType.Cpf);
            var email = new Email(command.Email);
            var address = new Address (command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            /*Gerar as entidades*/
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CredCardPayment(
                command.CardHolderName,
                command.CardNumber,
                command.LastTransactionNumber,
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email
            );

            /*Relacionamentos*/
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            /*Agrupar as validações*/
            AddNotifications(name, document, email, address, student, subscription, payment);

            /*Salvar as infos*/
            _repository.CreateSubscription(student);

            /*Enviar e-mail de boas vindas*/
            _emailService.Send(student.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");

            /*Retornar informações*/
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}