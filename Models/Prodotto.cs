using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Models
{
    public class Prodotto
    {
        public Prodotto()
        {
        }

        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Categoria { get; set; }
        public double Prezzo { get; set; }

        public void FromDictionary(Dictionary<string, string> riga)
        {
            Nome = riga["nome"];
            Foto = riga["foto"];
            Categoria = riga["categoria"];
            Prezzo = double.Parse(riga["prezzo"]);
        }
    }
}
