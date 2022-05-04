using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValeuObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entites
{
    public class Student : Entity
    {

        /*Variavel só visivel para essa classe que recebe uma lista de Inscrições*/
        private IList<Subscription> _subscriptions;
        public Student (Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            /*Dispara a notificação caso uma dessas entradas seja invalido*/
            AddNotifications(name, document, email); 
        }

        /*Definindo as propriedades de um aluno - Private para evitar de setar essa propriedade 
        lá na aplicação principal, que nesse caso é a app de test*/

        /*Passado as propriedades de nome para uma classe de ValueObjects, lá faremos regras para ele
        e chamaremos ela aqui direto como Name*/
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        
        /*Definindo endereço de entrega*/
        public Address Address { get; private set; }

        /*Criado uma lista de leitura de assinatura, pois o aluno pode ter varias assunaturas, e vc
        pode acabar adicionando assinaturas em duplicidades chamando só o ADD, ao invés do método
        ADDSubscription que foi criado para fazer a validação*/
        public IReadOnlyCollection<Subscription> Subscriptions { get {return _subscriptions.ToArray();}}

        /*Método para fazer adicição de uma assinatura já com uma validação*/
        public void AddSubscription (Subscription subscription)
        {
            var hasSubscriptionActive = false;
            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                {
                    hasSubscriptionActive = true;
                }
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscription.Pauments", "Esta assinatura não possui pagamentos")
            );

            /*Outra alternativa notificação*/
            /*Essa alternativa deu erro no flunt, não converteu o primeiro parametro string (student.subscription*/
            // if (hasSubscriptionActive)
            // {
            //     AddNotifications("Student.Subscriptions", "Você já tem uma assinatura ativa");
            // }


            /*Cancela todas as outras assinaturas, e coloca esta como principal*/

            // foreach (var sub in Subscriptions)
            // {
            //     sub.Inactivate();
            // }

            // _subscriptions.Add(subscription);
        }

        
    }
}