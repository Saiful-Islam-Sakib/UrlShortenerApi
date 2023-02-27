using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Common.ConfigurationModel
{
	public class SnowFlakeConfigurationSettings
	{
		public int MaxNumberOfBitsInMachineId { get; set; }
		public int MaxNumberOfBitsInSequenceId { get; set; }
		public long InitialMachineId { get; set; }
		public long InitialSequenceId { get; set; }
	}
}
