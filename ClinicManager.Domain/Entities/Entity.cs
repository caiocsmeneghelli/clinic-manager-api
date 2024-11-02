using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Entities
{
    public class Entity
    {
        public Entity()
        {
            CreatedAt = DateTime.Now;
            Active = true;
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }

        public void Delete() { Active = false; }
    }
}
