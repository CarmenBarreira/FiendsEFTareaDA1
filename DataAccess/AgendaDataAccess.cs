using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccess
{
    public class AgendaDataAccess : IDataAccess<Agenda>
    {
        public void Add(Agenda entity)
        {
            using (FriendContext context = new FriendContext())
            {
                context.Agendas.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(Agenda entity)
        {
            using (FriendContext context = new FriendContext())
            {
                var customer = context.Agendas.Single(o => o.Id == entity.Id);
                context.Agendas.Remove(customer);
                context.SaveChanges();
            }
        }

        public Agenda Get(Guid id)
        {
            using (FriendContext context = new FriendContext())
            {
                Agenda agenda = context.Agendas.FirstOrDefault(a => a.Id == id);
                context.Entry(agenda).Reference(a => a.Owner).Load();
                context.Entry(agenda).Collection(a => a.Contacts).Load(); 
                return agenda;
            }
        }

        public Agenda Get(string name)
        {
            using (FriendContext context = new FriendContext())
            {
                Agenda agenda = context.Agendas.FirstOrDefault(a => a.Name == name);
                context.Entry(agenda).Reference(a => a.Owner).Load();
                context.Entry(agenda).Collection(a => a.Contacts).Load();
                return agenda;
            }
        }

        public ICollection<Agenda> GetAll()
        {
            using (FriendContext context = new FriendContext())
            {
                return context.Agendas.ToList();
            }
        }

        public void Modify(Agenda entity)
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
    }
}
