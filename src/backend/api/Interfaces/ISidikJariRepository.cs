using System;
using api.Models;

namespace api.Interfaces
{
	public interface ISidikJariRepository
	{
        SidikJari? GetSidikJariByberkas_citra(string berkasCitra, int algorithm = 0);
    }
}

