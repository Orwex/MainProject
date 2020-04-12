using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public interface IAirLineDAO : IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUsername(string UserName);
        IList<AirlineCompany> GetAllAirLinesByCountry(int CountryId);
    }
}
