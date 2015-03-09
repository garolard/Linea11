using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linea11.Domain
{
    class Parada
    {
        // id
        public int Id { get; set; }

        // parada
        public string NombreParada { get; set; }

        // calculated
        public Sentido Sentido { get; set; }
    }

    enum Sentido 
    {
        IDA, VUELTA
    }
}
