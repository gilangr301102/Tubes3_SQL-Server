using System;
using api.Models;

namespace api.Interfaces
{
	public interface IBiodataRepository
	{
        ICollection<Biodata> GetBiodataByName(string name, int algorithm = 0);
    }
}

