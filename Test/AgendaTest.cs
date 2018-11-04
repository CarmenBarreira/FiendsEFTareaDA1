using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using Interfaces;
using DataAccess;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class AgendaTest
    {
        private IDataAccess<Agenda> dataAccessAgenda;
        private IDataAccess<User> dataAccessUsuario;
        private const string NOMBRE_USUARIO = "Owner";
        private const string NOMBRE_AGENDA = "Agenda";
        private const string NOMBRE_AGENDA_MODIFICADA = "Agenda_nueva";

        [TestInitialize]
        public void Setup()
        {
            dataAccessAgenda = new AgendaDataAccess();
            dataAccessUsuario = new UserDataAccess();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ICollection<Agenda> agendas = dataAccessAgenda.GetAll();
            foreach (Agenda agenda in agendas)
            {
                if (agenda != null)
                {
                    dataAccessAgenda.Delete(agenda);
                }
            }
            User ownerAgenda = dataAccessUsuario.Get(NOMBRE_USUARIO);
            dataAccessUsuario.Delete(ownerAgenda);
        }

        [TestMethod]
        public void AgregarAgendaTest()
        {

            Agenda agendaC = new Agenda()
            {
                Name = NOMBRE_AGENDA,
                Owner = new User()
                {
                    Age = 99,
                    Name = NOMBRE_USUARIO,
                    Agendas = new List<Agenda>()
                },
                Contacts = new List<User>()
            };
            dataAccessAgenda.Add(agendaC);
        }

        [TestMethod]
        public void DeleteAgendaTest()
        {
            Agenda ag1 = new Agenda()
            {
                Name = NOMBRE_AGENDA,
                Owner = new User()
                {
                    Age = 99,
                    Name = NOMBRE_USUARIO,
                    Agendas = new List<Agenda>()
                },
                Contacts = new List<User>()
            };
            dataAccessAgenda.Add(ag1);
            dataAccessAgenda.Delete(ag1);
        }

        [TestMethod]
        public void ModifyAgendaTest()
        {
            Agenda ag1 = new Agenda()
            {
                Name = NOMBRE_AGENDA,
                Owner = new User()
                {
                    Age = 99,
                    Name = NOMBRE_USUARIO,
                    Agendas = new List<Agenda>()
                },
                Contacts = new List<User>()
            };
            dataAccessAgenda.Add(ag1);
            ag1.Name = NOMBRE_AGENDA_MODIFICADA;

            dataAccessAgenda.Modify(ag1);
        }


        [TestMethod]
        public void GetAgendaTest()
        {
            Agenda ag1 = new Agenda()
            {
                Name = NOMBRE_AGENDA,
                Owner = new User()
                {
                    Age = 99,
                    Name = NOMBRE_USUARIO,
                    Agendas = new List<Agenda>()
                },
                Contacts = new List<User>()
            };
            dataAccessAgenda.Add(ag1);
            Assert.AreEqual(dataAccessAgenda.Get(ag1.Id).Name, NOMBRE_AGENDA);
        }

        [TestMethod]
        public void GetAllAgendaTest()
        {
            List<string> nombresAgenda = new List<string>();
            nombresAgenda.Add(NOMBRE_AGENDA);
            Agenda agendaNueva = new Agenda()
            {
                Name = NOMBRE_AGENDA,
                Owner = new User()
                {
                    Age = 99,
                    Name = NOMBRE_USUARIO,
                    Agendas = new List<Agenda>()
                },
                Contacts = new List<User>()
            };
            dataAccessAgenda.Add(agendaNueva);

            ICollection<Agenda> agendas = dataAccessAgenda.GetAll();
            foreach (Agenda agenda in agendas)
            {
                if (nombresAgenda.Contains(agenda.Name))
                {
                    nombresAgenda.Remove(agenda.Name);
                }
                else
                {
                    Assert.Fail();
                }
            }
            Assert.IsTrue(nombresAgenda.Count == 0);
        }

    }
}
