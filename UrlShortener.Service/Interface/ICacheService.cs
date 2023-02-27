using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.ApiService.Interface
{
	public interface ICacheService
	{
		T GetData<T>(string key) where T : class;
		bool SetData<T>(string key, T value, DateTime expirationTime) where T : class;
		T RemoveData<T>(string key) where T : class;
	}
}
