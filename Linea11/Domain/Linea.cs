using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linea11.Domain
{
    public class Linea
    {
        // id
        public int Id { get; set; }

        // nom_comer
        public string NombreComercial { get; set; }

        // color_linea
        public string ColorLinea { get; set; }

        // orig_linea
        public string OrigenLinea { get; set; }

        // dest_linea
        public string DestinoLinea { get; set; }

        // dest_ida
        public string DestinoIda { get; set; }

        // dest_vuelta
        public string DestinoVuelta { get; set; }
    }
}
