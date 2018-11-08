using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Common;
using System.Data.Entity.Core;

namespace DataAccess
{
    public class AgendaDataAccess : IDataAccess<Agenda>
    {
        public void Add(Agenda entity)
        {
            try
            {
                using (FriendContext context = new FriendContext())
                {
                    context.Agendas.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (DbException e)
            {
                throw new AccesoDatosBDException();

            }
            catch (UpdateException exception)
            {
                throw new AccesoDatosBDException();
            }
        }

        public void Delete(Agenda entity)
        {
            try
            {
                using (FriendContext context = new FriendContext())
                {
                    var customer = context.Agendas.Single(o => o.Id == entity.Id);
                    context.Agendas.Remove(customer);
                    context.SaveChanges();
                }
            }
            catch (DbException e)
            {
                throw new AccesoDatosBDException();

            }
            catch (UpdateException exception)
            {
                throw new AccesoDatosBDException();
            }
        }

        public Agenda Get(Guid id)
        {
            try
            {
                using (FriendContext context = new FriendContext())
                {
                    Agenda agenda = context.Agendas.FirstOrDefault(a => a.Id == id);
                    context.Entry(agenda).Reference(a => a.Owner).Load();
                    context.Entry(agenda).Collection(a => a.Contacts).Load();
                    return agenda;
                }
            }
            catch (DbException e)
            {
                throw new AccesoDatosBDException();

            }
            catch (UpdateException exception)
            {
                throw new AccesoDatosBDException();
            }
        }


        public Agenda Get(string name)
        {
            try
            {
                using (FriendContext context = new FriendContext())
                {
                    Agenda agenda = context.Agendas.FirstOrDefault(a => a.Name == name);
                    context.Entry(agenda).Reference(a => a.Owner).Load();
                    context.Entry(agenda).Collection(a => a.Contacts).Load();
                    return agenda;
                }
            }
            catch (DbException e)
            {
                throw new AccesoDatosBDException();

            }
            catch (UpdateException exception)
            {
                throw new AccesoDatosBDException();
            }
        }

        public ICollection<Agenda> GetAll()
        {
            try
            {
                using (FriendContext context = new FriendContext())
                {
                    return context.Agendas.ToList();
                }
            }
            catch (DbException e)
            {
                throw new AccesoDatosBDException();

            }
            catch (UpdateException exception)
            {
                throw new AccesoDatosBDException();
            }
        }

        public void Modify(Agenda entity)
        {
            try
            {
                using (FriendContext context = new FriendContext())
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.Entry(entity.Owner).State = EntityState.Modified;

                    foreach (var contact in entity.Contacts)
                    {
                        context.Entry(contact).State = contact.Id.Equals(Guid.Empty) ? EntityState.Added : EntityState.Unchanged;
                    }

                    context.SaveChanges();
                }
            }
            catch (DbException e)
            {
                throw new AccesoDatosBDException();

            }
            catch (UpdateException exception)
            {
                throw new AccesoDatosBDException();
            }

        }
    }
}
