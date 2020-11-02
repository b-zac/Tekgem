using System;
using System.Collections.Generic;
using System.Text;

namespace Tekgem
{
    public interface IDataAccess
    {
        ICollection<string> GetCities();
    }
}
