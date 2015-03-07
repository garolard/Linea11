using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linea11.Domain;

namespace Linea11.Services.Interface
{
    interface ILineaRepository
    {
        Task<IList<Linea>> ListAllAsync();
    }
}
