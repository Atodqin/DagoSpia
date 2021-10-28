using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Models
{
    public class Quantita
    {
        public int TagliaId { get; set; }
        public int ColoreId { get; set; }
        public int ProdottoId { get; set; }
        public int Quantitativo { get; set; }

        public Prodotto Prodotti { get; set; }
        public Colore Colori { get; set; }
        public Taglia Taglie { get; set; }

        public void FromDictionary(Dictionary<string, string> riga)
        {
            TagliaId = int.Parse(riga["tagliaid"]);
            ColoreId = int.Parse(riga["coloreid"]);
            ProdottoId = int.Parse(riga["prodottoid"]);
            Quantitativo = int.Parse(riga["quantitativo"]);

            //Prodotti = new Prodotto { Nome = riga["nome"],
            //                          Foto = riga["foto"],
            //                          Categoria = riga["categoria"],
            //                          Prezzo = double.Parse(riga["prezzo"])};

            //Colori = new Colore {Nome = riga["colore"] };
            //Taglie = new Taglia {Nome = riga["taglia"] };
        }
    }
}
