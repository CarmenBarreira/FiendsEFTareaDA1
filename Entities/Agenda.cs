using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Agenda
    {
        //Id convention name. If Class has a property that ends with "Id" it will become the PK
        public Guid Id { get; set; }
        public string Name { get; set; }

        //VIRTUAL in this case means that the navigable poperty can be Lazy Loaded  
        public virtual User Owner { get; set; }
        public virtual ICollection<User> Contacts { get; set; }
    }
}
