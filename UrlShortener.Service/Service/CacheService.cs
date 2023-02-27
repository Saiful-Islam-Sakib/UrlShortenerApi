using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ApiService.Interface;

namespace UrlShortener.ApiService.Service
{
	public class CacheService : ICacheService
	{
		public T GetData<T>(string key) where T : class
		{
			throw new NotImplementedException();
		}

		public bool SetData<T>(string key, T value, DateTime expirationTime) where T : class
		{
			throw new NotImplementedException();
		}

		public T RemoveData<T>(string key) where T : class
		{
			throw new NotImplementedException();
		}
	}
}
