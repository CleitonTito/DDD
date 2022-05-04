using PaymentContext.Domain.Entites;

namespace PaymentContext.Domain.Repositories
{

    /*Consulta no banco para saber se esses dados existem (ver curso modern rebeps*/
    public interface IStudentRepository
    {
        bool DocumentExists(string document);
        bool EmailExistis(string email);
        void CreateSubscription(Student student);
    }
}