using SmartCare.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Utils
{
    public interface IDataSeed 
    {
       public Task  DataSeedingAsync();
       public Task IdentityDataSeedingAsync();

    }
}
