using DataAccess;
using Entities;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class UserTest
    {
        IDataAccess<User> dataAccess;
        private const string NOMBRE_USUARIO = "User";

        [TestInitialize]
        public void Setup()
        {
            dataAccess = new UserDataAccess();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ICollection<User> users = dataAccess.GetAll();
            foreach (User user in users)
            {
                if (user != null)
                {
                    dataAccess.Delete(user);
                }
            }
        }


        [TestMethod]
        public void AddUserTest()
        {
            User user = new User()
            {
                Age = 23,
                Name = NOMBRE_USUARIO,
                Agendas = new List<Agenda>()
            };
            dataAccess.Add(user);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            User user1 = new User()
            {
                Name = NOMBRE_USUARIO,
                Age = 22,
                Agendas = new List<Agenda>()
            };
            dataAccess.Add(user1);
            dataAccess.Delete(user1);
        }

        [TestMethod]
        public void ModifyUserTest()
        {
            User user2 = new User()
            {
                Name = NOMBRE_USUARIO,
                Age = 20,
                Agendas = new List<Agenda>()
            };
            dataAccess.Add(user2);
            user2.Name = "User 2.1";

            dataAccess.Modify(user2);
        }

        [TestMethod]
        public void GetUserTest()
        {
            User user3 = new User()
            {
                Name = NOMBRE_USUARIO,
                Age = 11,
                Agendas = new List<Agenda>()
            };
            dataAccess.Add(user3);
            Assert.AreEqual(dataAccess.Get(user3.Id).Name, NOMBRE_USUARIO);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            List<string> nameUsers = new List<string>();
            nameUsers.Add(NOMBRE_USUARIO);

            User user0 = new User()
            {
                Name = NOMBRE_USUARIO,
                Age = 11,
                Agendas = new List<Agenda>()
            };
            dataAccess.Add(user0);

            ICollection<User> users = dataAccess.GetAll();
            foreach (User user in users)
            {
                if (nameUsers.Contains(user.Name))
                {
                    nameUsers.Remove(user.Name);
                }
                else
                {
                    Assert.Fail();
                }
            }
            Assert.IsTrue(nameUsers.Count == 0);
        }

    }
}
