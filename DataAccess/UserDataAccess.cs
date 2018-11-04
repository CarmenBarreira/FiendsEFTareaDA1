using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class UserDataAccess : IDataAccess<User>
    {
        public void Add(User entity)
        {
            using (FriendContext context = new FriendContext())
            {
                context.Users.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(User entity)
        {
            using (FriendContext context = new FriendContext())
            {
                var user = context.Users.Single(o => o.Id == entity.Id);
                context.Users.Remove(user);

                context.SaveChanges();
            }
        }

        public User Get(Guid id)
        {
            using (FriendContext context = new FriendContext())
            {
                var user = context.Users.Single(o => o.Id == id);
                return user;
            }
        }

        public User Get(string name)
        {
            using (FriendContext context = new FriendContext())
            {
                var user = context.Users.Single(o => o.Name == name);
                return user;
            }
        }

        public ICollection<User> GetAll()
        {
            using (FriendContext context = new FriendContext())
            {
                return context.Users.ToList();
            }
        }

        public void Modify(User entity)
        {
            using (FriendContext context = new FriendContext())
            {

                var user = (from u in context.Users
                            where u.Id == entity.Id
                            select u).FirstOrDefault();
                user.Age = entity.Age;
                user.Agendas = entity.Agendas;
                user.Name = entity.Name;

                context.SaveChanges();
            }
        }
    }
}
