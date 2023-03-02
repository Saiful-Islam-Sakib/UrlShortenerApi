namespace UrlShortener.Common.Interface
{
	public interface ICacheService
	{
		T? GetData<T>(string key, Func<T> dbOperation) where T : class;
		void SetData<T>(string key, T value, Func<T> dbOperation) where T : class;
		void RemoveData<T>(string key, Func<T> dbOperation) where T : class;
	}
}
