using SN.NetSet.Entities.Concrete.Network;
using System.Collections.Generic;

namespace SN.NetSet.Business.Abstract
{
    public interface INetConfigDataService : IAddressableDB
    {
        IList<NetConfigBase> GetConfigList();

        NetConfigBase GetConfig(int id);

        void AddNewConfig(NetConfigBase config);

        void UpdateConfig(NetConfigBase config);

        void DeleteConfig(NetConfigBase config);

        void AddNewListConfig(List<NetConfigBase> configList);

        void ImportFromJsonListConfig(string json);

        void SaveToFileLisConfig(string fileName);
    }
}
