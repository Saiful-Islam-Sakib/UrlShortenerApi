using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.ApiService.Interface
{
	public interface IUniqueIdGenerator
	{
		string GenerateNextId();
	}
}
