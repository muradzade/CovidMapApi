using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMapApi.Data
{
    public interface ISeedData
    {
        public void EnsurePopulated();
    }
}
