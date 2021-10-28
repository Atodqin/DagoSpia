using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Models
{
    public class Colore
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public void FromDictionary(Dictionary<string, string> riga)
        {
            Nome = riga["nome"];
        }
    }
}
