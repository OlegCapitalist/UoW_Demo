using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;
using Services.Interfaces;
using Services.Repositories;
using Infrastructure;
using ServicesTests.Services;


namespace ServicesTests
{
    [TestClass]
    public class StudentsRepositoryTests
    {
        private readonly StudentsRepository _repository;

        public StudentsRepositoryTests()
        {
            string connectionString = ConnectionString.Get();

            if (connectionString != null)
            {
                _repository = new StudentsRepository(
                    new UnitOfWork<UniversityDbContext>(
                        new UniversityDbContext(connectionString)
                    )
                 );

            }
        }

        #region Tests

        [TestMethod]
        public void StudentsRepository_Add_Correct_Success()
        {
            // arrange
            const int nameLength = 150;
            var student = new Student();
            student.FirstName = RandomString.Generate(nameLength);
            student.LastName = RandomString.Generate(nameLength);
            student.GroupId = 1;
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(student, dataBaseAction => _repository.Add(student));
            int actualId = student.Id;

            // assert
            Assert.AreNotEqual(expectedId, actualId);
        }

        [TestMethod]
        public void StudentsRepository_Add_WithoutFirstName_NotSuccess()
        {
            // arrange
            const int nameLength = 150;
            var student = new Student();
            student.FirstName = null;
            student.LastName = RandomString.Generate(nameLength);
            student.GroupId = 1;
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(student, dataBaseAction => _repository.Add(student));
            int actualId = student.Id;

            // assert
            Assert.AreEqual(expectedId, actualId);

        }

        [TestMethod]
        public void StudentsRepository_Add_WithoutLastName_NotSuccess()
        {
            // arrange
            const int nameLength = 150;
            var student = new Student();
            student.FirstName = RandomString.Generate(nameLength);
            student.LastName = null;
            student.GroupId = 1;
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(student, dataBaseAction => _repository.Add(student));
            int actualId = student.Id;

            // assert
            Assert.AreEqual(expectedId, actualId);

        }

        [TestMethod]
        public void StudentsRepository_Add_OversizeFirstName_NotSuccess()
        {
            // arrange
            const int firstNameLength = 151;
            const int lastLastLength = 150;
            var student = new Student();
            student.FirstName = RandomString.Generate(firstNameLength);
            student.LastName = RandomString.Generate(lastLastLength);
            student.GroupId = 1;
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(student, dataBaseAction => _repository.Add(student));
            int actualId = student.Id;

            // assert
            Assert.AreEqual(expectedId, actualId);

        }

        [TestMethod]
        public void StudentsRepository_Add_OversizeLastName_NotSuccess()
        {
            // arrange
            const int firstNameLength = 150;
            const int lastLastLength = 151;
            var student = new Student();
            student.FirstName = RandomString.Generate(firstNameLength);
            student.LastName = RandomString.Generate(lastLastLength);
            student.GroupId = 1;
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(student, dataBaseAction => _repository.Add(student));
            int actualId = student.Id;

            // assert
            Assert.AreEqual(expectedId, actualId);

        }

        [TestMethod]
        public void StudentsRepository_Delete_Success()
        {
            if (_repository != null)
            {
                // arrange
                const int nameLength = 50;
                var student = new Student();
                student.FirstName = RandomString.Generate(nameLength);
                student.LastName = RandomString.Generate(nameLength);
                student.GroupId = 1;
                _repository.WriteIntoDataBase(student, dataBaseAction => _repository.Add(student));
                int id = student.Id;
                Student expectedStudent = null;

                // act
                _repository.WriteIntoDataBase(student, dataBaseAction => _repository.Delete(student));
                var actualStudent = _repository.GetById(id);

                // assert
                Assert.AreEqual(expectedStudent, actualStudent);
            }
        }

        #endregion

    }
}