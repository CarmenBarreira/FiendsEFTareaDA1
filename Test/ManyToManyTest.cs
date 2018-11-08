using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Entities;
using DataAccess;

namespace Test
{
    [TestClass]
    public class ManyToManyTest
    {
        private IDataAccess<Agenda> dataAccessAgenda;
        private IDataAccess<User> dataAccessUsuario;
        private const string NOMBRE_USUARIO = "Owner";
        private const string NOMBRE_USUARIO_2 = "User2";
        private const string NOMBRE_AGENDA = "Agenda";
        private const string NOMBRE_AGENDA_2 = "Agenda_2";

        [TestInitialize]
        public void Setup()
        {
            dataAccessAgenda = new AgendaDataAccess();
            dataAccessUsuario = new UserDataAccess();
        }

        /*
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

            ICollection<User> users = dataAccessUsuario.GetAll();
            foreach (User user in users)
            {
                if (user != null)
                {
                    dataAccessUsuario.Delete(user);
                }
            }
        }
        */

        [TestMethod]
        public void AgregarDosAgendasAUser()
        {
            Agenda agenda1 = new Agenda()
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

            Agenda agenda2 = new Agenda()
             {
                 Name = NOMBRE_AGENDA_2,
                 Owner = new User()
                 {
                     Age = 99,
                     Name = NOMBRE_USUARIO,
                     Agendas = new List<Agenda>()
                 },
                 Contacts = new List<User>()
             };

            List<Agenda> agendas = new List<Agenda>();
            agendas.Add(agenda1);
            agendas.Add(agenda2);

            User user1 = new User()
            {
                Name = NOMBRE_USUARIO,
                Age = 22,
                Agendas = agendas
            };
            dataAccessUsuario.Add(user1);
        }

        [TestMethod]
        public void AgregarDosAgendasAUserYOtroUserAUnaDeLasAgendas()
        {
            List<User> usuarios = new List<User>();
            User user2 = new User()
            {
                Name = NOMBRE_USUARIO_2,
                Age = 22,
            };
            usuarios.Add(user2);

            Agenda agenda1 = new Agenda()
            {
                Name = NOMBRE_AGENDA,
                Owner = new User()
                {
                    Age = 99,
                    Name = NOMBRE_USUARIO,
                    Agendas = new List<Agenda>()
                },
                Contacts = usuarios
            };

            Agenda agenda2 = new Agenda()
            {
                Name = NOMBRE_AGENDA_2,
                Owner = new User()
                {
                    Age = 99,
                    Name = NOMBRE_USUARIO,
                    Agendas = new List<Agenda>()
                },
                Contacts = new List<User>()
            };

            List<Agenda> agendas = new List<Agenda>();
            agendas.Add(agenda1);
            agendas.Add(agenda2);

            User user1 = new User()
            {
                Name = NOMBRE_USUARIO,
                Age = 22,
                Agendas = agendas
            };
            dataAccessUsuario.Add(user1);
      

        }


    }
}

