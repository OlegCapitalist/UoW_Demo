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
    public class GroupsRepositoryTests
    {
        private readonly GroupsRepository _repository;

        public GroupsRepositoryTests()
        {
            string connectionString = ConnectionString.Get();

            if (connectionString != null)
            {
                _repository = new GroupsRepository(
                    new UnitOfWork<UniversityDbContext>(
                        new UniversityDbContext(connectionString)
                    )
                 );

            }
        }

        #region Tests

        [TestMethod]
        public void GroupsRepository_Add_Correct_Success()
        {
            // arrange
            const int nameLength = 50;
            var group = new Group();
            group.Name = RandomString.Generate(nameLength);
            group.CourseId = 1;
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(group, dataBaseAction => _repository.Add(group));
            int actualId = group.Id;

            // assert
            Assert.AreNotEqual(expectedId, actualId);
        }

        [TestMethod]
        public void GroupsRepository_Add_WithoutName_NotSuccess()
        {
            // arrange
            var group = new Group();
            group.CourseId = 1;
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(group, dataBaseAction => _repository.Add(group));
            int actualId = group.Id;

            // assert
            Assert.AreEqual(expectedId, actualId);

        }

        [TestMethod]
        public void GroupsRepository_Add_OversizeName_NotSuccess()
        {
            // arrange
            const int nameLength = 51;
            var group = new Group();
            group.Name = RandomString.Generate(nameLength);
            group.CourseId = 1;
            int expectedId = 0;

            // act
            if (_repository != null)
                _repository.WriteIntoDataBase(group, dataBaseAction => _repository.Add(group));
            int actualId = group.Id;

            // assert
            Assert.AreEqual(expectedId, actualId);

        }

        [TestMethod]
        public void GroupsRepository_DeleteWithSubjectItems_NotSuccess()
        {
            if (_repository != null)
            {
                // arrange
                int expectedId = 1;
                var group = _repository.GetById(expectedId);

                // act
                _repository.WriteIntoDataBase(group, dataBaseAction => _repository.Delete(group));
                int actualId = group.Id;

                // assert
                Assert.AreEqual(expectedId, actualId);
            }
        }

        [TestMethod]
        public void GroupsRepository_Delete_Success()
        {
            if (_repository != null)
            {
                // arrange
                const int nameLength = 50;
                var group = new Group();
                group.Name = RandomString.Generate(nameLength);
                group.CourseId = 1;
                _repository.WriteIntoDataBase(group, dataBaseAction => _repository.Add(group));
                int id = group.Id;
                Group expectedGroup = null;

                // act
                _repository.WriteIntoDataBase(group, dataBaseAction => _repository.Delete(group));
                var actualGroup = _repository.GetById(id);

                // assert
                Assert.AreEqual(expectedGroup, actualGroup);
            }
        }

        #endregion

    }
}