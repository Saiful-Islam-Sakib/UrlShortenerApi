using Microsoft.EntityFrameworkCore;
using UrlShortener.Common.Models;

namespace UrlShortener.Repository.Context
{
	public class ShortUrlDbContext: DbContext
	{
		public ShortUrlDbContext(DbContextOptions options) : base(options) { }
		public DbSet<ShortUrl>? ShortUrl { get; set; }
	}
}
