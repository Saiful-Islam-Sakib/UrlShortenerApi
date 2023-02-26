using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ApiService.Interface;

namespace UrlShortener.ApiService.Service
{
    public class UniqueIdGeneratorService : IUniqueIdGeneratorService
    {
        private const int MaxNumberOfBitsInMachineId = 10;
		private const int MaxNumberOfBitsInSequenceId = 12;
		public string GenerateNextId()
        {
			long timeStamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() << (MaxNumberOfBitsInMachineId + MaxNumberOfBitsInSequenceId);
            long machineId = 1 << MaxNumberOfBitsInSequenceId;
            long sequenceId = 1;

            long result = timeStamp | machineId | sequenceId;

            return Convert.ToString(result, 16);
        }
    }
}
