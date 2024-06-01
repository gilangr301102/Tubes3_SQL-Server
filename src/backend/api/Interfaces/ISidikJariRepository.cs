using System;
using api.Models;

namespace api.Interfaces
{
	public interface ISidikJariRepository
	{
        SidikJari? GetSidikJariByBerkasCitra(string berkasCitra, int algorithm = 0);
    }
}

