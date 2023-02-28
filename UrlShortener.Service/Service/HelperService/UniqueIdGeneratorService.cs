using Microsoft.Extensions.Options;
using UrlShortener.Common.Interface;
using UrlShortener.Common.ConfigurationModel;

namespace UrlShortener.Services.Service.HelperService
{
    public class UniqueIdGeneratorService : IUniqueIdGeneratorService
    {
        private readonly IOptions<SnowFlakeConfigurationSettings> _SnowFlakeConfigurationSettings;
        private SnowFlakeConfigurationSettings _oSnowFlakeConfigurationSettings;
        private readonly object _lockingObject = new object();
        private long _MaxMachineId;
        private long _MaxSequenceId;

        public UniqueIdGeneratorService(IOptions<SnowFlakeConfigurationSettings> snowFlakeSettings)
        {
            _SnowFlakeConfigurationSettings = snowFlakeSettings;
            _oSnowFlakeConfigurationSettings = _SnowFlakeConfigurationSettings.Value;
            _MaxMachineId = -1L ^ -1L << _oSnowFlakeConfigurationSettings.MaxNumberOfBitsInMachineId;
            _MaxSequenceId = -1L ^ -1L << _oSnowFlakeConfigurationSettings.MaxNumberOfBitsInSequenceId;
        }

        private void Validate(SnowFlakeConfigurationSettings pSnowFlakeConfigurationSettings)
        {
            try
            {
                if (pSnowFlakeConfigurationSettings.InitialMachineId > _MaxMachineId || pSnowFlakeConfigurationSettings.InitialMachineId < 0)
                {
                    throw new ArgumentException($"machine ID must be between 0 and {_MaxMachineId}");
                }

                if (pSnowFlakeConfigurationSettings.InitialSequenceId > _MaxSequenceId || pSnowFlakeConfigurationSettings.InitialSequenceId < 0)
                {
                    throw new ArgumentException($"Sequence ID must be between 0 and {_MaxSequenceId}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateNextId()
        {
            try
            {
                Validate(_oSnowFlakeConfigurationSettings);

                lock (_lockingObject)
                {
                    long timeStamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() << _oSnowFlakeConfigurationSettings.MaxNumberOfBitsInMachineId + _oSnowFlakeConfigurationSettings.MaxNumberOfBitsInSequenceId;
                    long result = timeStamp | _oSnowFlakeConfigurationSettings.InitialMachineId << _oSnowFlakeConfigurationSettings.MaxNumberOfBitsInSequenceId | _oSnowFlakeConfigurationSettings.InitialSequenceId;
                    _oSnowFlakeConfigurationSettings.InitialMachineId++;
                    _oSnowFlakeConfigurationSettings.InitialSequenceId++;

                    return Convert.ToString(result, 16);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
