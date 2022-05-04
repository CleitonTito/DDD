using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValeuObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudenQueriesTests
    {

        private IList<Student> _students;

        public StudentQueriesTests()
        {
            for (var i = 0; i <= 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()), 
                    new Document("1111111111" + i.ToString(), EdocumentType.Cpf),
                    new Email(i.ToString() + "@balta.io")
                    ));
            }
        }

        [TestMethod]

        public void ShoudReturnNullWhenDocumentNotExists()
        {
          var exp = StudentQueries.GetStudentInfo("12345678911");
          var studn = _students.AsQueryable().Where(exp).FirstOrDefault(); 

          Assert.AreEqual(null, studn);

        }
        
        public void ShoudReturnStudentWheDocumentExists()
        {
          var exp = StudentQueries.GetStudentInfo("1111111111");
          var studn = _students.AsQueryable().Where(exp).FirstOrDefault(); 

          Assert.AreEqual(null, studn);

        }
     
    }
}