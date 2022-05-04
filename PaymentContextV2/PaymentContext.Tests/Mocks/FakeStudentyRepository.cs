using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            
        }

        public bool DocumentExists(string document)
        {
            if (document == "99999999999")
                return true;

            return false;

        }

        public bool EmailExistis(string email)
        {
            if (email =="hello@balta.io")
                return true;
            return false;
        }
    }
}