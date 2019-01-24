namespace NavyBlue.Demo.ServiceGovern
{
    public class ServiceRegisterEntity
    {
        public string ConsulServer { get; set; }

        public bool IsHttps { get; set; }

        public string ServiceIP { get; set; }

        public string ServiceName { get; set; }

        public int ServicePort { get; set; }
    }
}