using NavyBlue.AspNetCore.ConsulConfiguration;

namespace NavyBlue.Demo.ServiceGovern
{
    public class NavyBlueConfiguration : IConsulConfiguration
    {
        public string KeyTest { get; set; }

        public string Name { get; set; }

        public string ConnectionString { get; set; }
    }
}
