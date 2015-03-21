using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linea11.Domain
{
    class Bus
    {
        // bus
        public int Id { get; set; }

        // linea
        public int Linea { get; set; }

        // adaptado
        public bool Adaptado { get; set; }

        // metros
        public int Metros { get; set; }

        // color_linea
        public string ColorLinea { get; set; }
    }
}
