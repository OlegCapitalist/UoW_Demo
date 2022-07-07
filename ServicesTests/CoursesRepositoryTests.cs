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
    public class CoursesRepositoryTests
    {
        private readonly CoursesRepository _repository;

        public CoursesRepositoryTests()
        {
            string connectionString = ConnectionString.Get();

            if (connectionString != null)
            {
                _repository = new CoursesRepository(
                    new UnitOfWork<UniversityDbContext>(
                        new UniversityDbContext(connectionString)
                    )
                 );

            }
        }

        #region Tests

        [TestMethod]
        public void CoursesRepository_Add_Correct_Success()
        {
            // arrange
            const int nameLength = 50;
            var course = new Course();
            course.Name = RandomString.Generate(nameLength);
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(course, dataBaseAction => _repository.Add(course));
            int actualId = course.Id;

            // assert
            Assert.AreNotEqual(expectedId, actualId);
        }

        [TestMethod]
        public void CoursesRepository_Add_WithoutName_NotSuccess()
        {
            // arrange
            var course = new Course();
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(course, dataBaseAction => _repository.Add(course));
            int actualId = course.Id;

            // assert
            Assert.AreEqual(expectedId, actualId);

        }

        [TestMethod]
        public void CoursesRepository_Add_OversizeName_NotSuccess()
        {
            // arrange
            const int nameLength = 51;
            var course = new Course();
            course.Name = RandomString.Generate(nameLength);
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(course, dataBaseAction => _repository.Add(course));
            int actualId = course.Id;

            // assert
            Assert.AreEqual(expectedId, actualId);

        }

        [TestMethod]
        public void CoursesRepository_Add_OversizeDescription_NotSuccess()
        {
            // arrange
            const int nameLength = 151;
            var course = new Course();
            course.Name = RandomString.Generate(nameLength);
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(course, dataBaseAction => _repository.Add(course));
            int actualId = course.Id;

            // assert
            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
            //[ExpectedException(typeof(Exception),
            //     "Существуюут подчиненнные элементы")]
        public void CoursesRepository_DeleteWithSubjectItems_NotSuccess()
        {
            if (_repository != null)
            {
                // arrange
                int expectedId = 1;
                var course = _repository.GetById(expectedId);

                // act
                _repository.WriteIntoDataBase(course, dataBaseAction => _repository.Delete(course));
                int actualId = course.Id;

                // assert
                Assert.AreEqual(expectedId, actualId);
            }
        }

        [TestMethod]
        public void CoursesRepository_Delete_Success()
        {
            if (_repository != null)
            {
                // arrange
                const int nameLength = 50;
                var course = new Course();
                course.Name = RandomString.Generate(nameLength);
                _repository.WriteIntoDataBase(course, dataBaseAction => _repository.Add(course));
                int id = course.Id;
                Course expectedCourse = null;

                // act
                _repository.WriteIntoDataBase(course, dataBaseAction => _repository.Delete(course));
                var actualCourse = _repository.GetById(id);

                // assert
                Assert.AreEqual(expectedCourse, actualCourse);
            }
        }

        #endregion

    }
}