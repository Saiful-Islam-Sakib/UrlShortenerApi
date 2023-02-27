using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Common.Models;

namespace UrlShortener.DAService.Context
{
	public class ShortUrlDbContext: DbContext
	{
		public ShortUrlDbContext(DbContextOptions options) : base(options) { }
		public DbSet<ShortUrl>? tblShortUrl { get; set; }
	}
}
