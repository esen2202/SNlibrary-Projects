namespace SN.Network.Model
{
    public class NetAdapterModelBase : IModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string NetworkInterfaceType { get; set; }
        public bool IsOperationalStatusUp { get; set; }

        public IModel Clone()
        {
           return (IModel)this.MemberwiseClone();
        }
    }
}
