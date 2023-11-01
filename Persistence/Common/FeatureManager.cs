using Application.Common.Enums;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;


namespace Persistence.Common
{
    public class FeatureManager : IFeatureFlags
    {
        private readonly IConfiguration _configuration;

        public FeatureManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsEnabled(FeatureFlags featureFlag)
        {
            string flagName =  featureFlag.ToString();

            var flagValue = _configuration.GetSection(flagName).Value;
                       
             return bool.TryParse(flagValue, out var isEnabled) && isEnabled; ;

            // Convert the flag value to boolean (assuming it's stored as a string in configuration)
            //return   bool.TryParse(flagValue, out var isEnabled) && isEnabled;
        }
    }
}
