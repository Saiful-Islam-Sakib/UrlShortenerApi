using Microsoft.Extensions.Options;
using UrlShortener.Common.Interface;
using UrlShortener.Common.ConfigurationModel;

namespace UrlShortener.Services.Service.HelperService
{
    public class UniqueIdGeneratorService : IUniqueIdGeneratorService
    {
        private SnowFlakeConfigurationSettings _oSnowFlakeConfigurationSettings;
		private readonly IOptions<SnowFlakeConfigurationSettings> _snowFlakeConfigurationSettings;
        private readonly object _lockingObject = new object();
        private readonly long _maxMachineId;
        private readonly long _maxSequenceId;
        private static long _lasttimeStamp;
        private static long _sequenceId;


		public UniqueIdGeneratorService(IOptions<SnowFlakeConfigurationSettings> snowFlakeSettings)
        {
            _snowFlakeConfigurationSettings = snowFlakeSettings;
            _oSnowFlakeConfigurationSettings = _snowFlakeConfigurationSettings.Value;
            _maxMachineId = -1L ^ -1L << _oSnowFlakeConfigurationSettings.MaxNumberOfBitsInMachineId;
            _maxSequenceId = -1L ^ -1L << _oSnowFlakeConfigurationSettings.MaxNumberOfBitsInSequenceId;
        }

        private void Validate(SnowFlakeConfigurationSettings pSnowFlakeConfigurationSettings)
        {
            try
            {
                if (pSnowFlakeConfigurationSettings.InitialMachineId > _maxMachineId || pSnowFlakeConfigurationSettings.InitialMachineId < 0)
                {
                    throw new ArgumentException($"machine ID must be between 0 and {_maxMachineId}");
                }

                if (pSnowFlakeConfigurationSettings.InitialSequenceId > _maxSequenceId || pSnowFlakeConfigurationSettings.InitialSequenceId < 0)
                {
                    throw new ArgumentException($"Sequence ID must be between 0 and {_maxSequenceId}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

		private string ConvertToBase62(long idToBeConverted)
        {
			string _base62Data = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            int newBase = 62;

            int temporaryRemainder = 0;
            string convertedBase62Id = string.Empty;

			while (idToBeConverted != 0)
            {
				temporaryRemainder = (int)(idToBeConverted % newBase);

                convertedBase62Id = _base62Data[temporaryRemainder].ToString() + convertedBase62Id;

				idToBeConverted /= newBase;
			}

			return convertedBase62Id;
        }

        public string GenerateNextId()
        {
            try
            {
                Validate(_oSnowFlakeConfigurationSettings);

                lock (_lockingObject)
                {
                    long timeStamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();

					long machineId = _oSnowFlakeConfigurationSettings.InitialMachineId << _oSnowFlakeConfigurationSettings.MaxNumberOfBitsInSequenceId;

                    _sequenceId = _lasttimeStamp == timeStamp ? _maxSequenceId & (_sequenceId + 1) : _oSnowFlakeConfigurationSettings.InitialSequenceId;

					_lasttimeStamp = timeStamp;

                    timeStamp <<= (_oSnowFlakeConfigurationSettings.MaxNumberOfBitsInMachineId + _oSnowFlakeConfigurationSettings.MaxNumberOfBitsInSequenceId);

					long resultingId = timeStamp | machineId | _sequenceId;

					return ConvertToBase62(resultingId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
