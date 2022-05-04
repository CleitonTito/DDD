using System;
using Flunt.Notifications;

namespace PaymentContext.Shared.Entities
{

    /*Notifiable é do paconte Flunt*/
    public abstract class Entity : Notifiable
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}