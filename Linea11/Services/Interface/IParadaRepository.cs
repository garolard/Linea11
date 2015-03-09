using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linea11.ViewModels.Interface;

namespace Linea11.Services.Interface
{
    interface IParadaRepository
    {
        Task<IList<IParadaViewModel>> FindAll(int lineId);
    }
}
