using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linea11.Domain;
using Linea11.ViewModels.Interface;
using Linea11.ViewModels;

namespace Linea11.Services.Interface
{
    interface ILineaRepository
    {
        Task<IList<ILineaViewModel>> ListAllAsync();
    }
}
