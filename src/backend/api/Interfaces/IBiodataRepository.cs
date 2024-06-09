using System;
using api.Models;

namespace api.Interfaces
{
	public interface IBiodataRepository
	{
        ICollection<BiodataResponse> GetBiodataByName(string name, int algorithm = 0);
    }
}

