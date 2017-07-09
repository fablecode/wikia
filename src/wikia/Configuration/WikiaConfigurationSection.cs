using System.Configuration;

namespace wikia.Configuration
{
    public class WikiaConfigurationSection : ConfigurationSection, IWikiaConfig
    {

        private const string WikiDomainKey = "WikiDomain";

        [ConfigurationProperty(WikiDomainKey, IsRequired = true)]
        public string WikiDomain
        {
            get { return (string)this[WikiDomainKey]; }
        }
    }

    public interface IWikiaConfig
    {
        string WikiDomain { get; }
    }
}