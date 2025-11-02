using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petanque.petanquebackend.Model
{
    public class Speler
    {
        public string naam;
        public int age;
        public int spelerId;
        public Speler(string naam, int age, int spelerId)
        {
            this.naam = naam;
            this.age = age;
            this.spelerId = spelerId;
        }

        

        
    }
}