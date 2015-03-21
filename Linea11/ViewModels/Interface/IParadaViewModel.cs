using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linea11.Domain;

namespace Linea11.ViewModels.Interface
{
    interface IParadaViewModel
    {
        int Id { get; set; }
        int IdLinea { get; set; }
        string NombreParada { get; set; }
        Sentido Sentido { get; set; }
    }
}
