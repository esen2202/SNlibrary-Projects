using FluentValidation.Results;
using Newtonsoft.Json;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.Data.Validation;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SN.NetSet.Business.Data
{
    public class NetConfigDbManager : INetConfigDataService 
    {
        private readonly NetConfigBaseValidator validationRules = new NetConfigBaseValidator();

        INetConfigDal _netConfigDal;

        public NetConfigDbManager(INetConfigDal netConfigDal)
        {
            _netConfigDal = netConfigDal;
        }

        public NetConfigBase GetConfig(int id)
        {
            return _netConfigDal.Get(o => o.Id == id);
        }

        public IList<NetConfigBase> GetConfigList() => _netConfigDal.GetList();

        public void AddNewConfig(NetConfigBase config)
        {
            ValidationResult validationResult = validationRules.Validate(config);

            if (validationResult.IsValid)
            {
                _netConfigDal.Add(config);
            }
            else
            {
                throw new System.Exception(validationResult.Errors.First().ErrorMessage);
            }
        }

        public void UpdateConfig(NetConfigBase config)
        {
            ValidationResult validationResult = validationRules.Validate(config);

            if (validationResult.IsValid)
            {
                _netConfigDal.Update(config, record => record.Id == config.Id);
            }
            else
            {
                throw new System.Exception(validationResult.Errors.First().ErrorMessage);
            }
        }

        public void DeleteConfig(NetConfigBase config)
        {
            _netConfigDal.Delete(config);
        }

        public void AddNewListConfig(List<NetConfigBase> configList)
        {
            foreach (var config in configList)
            {
                ValidationResult validationResult = validationRules.Validate(config);

                if (validationResult.IsValid)
                {
                    _netConfigDal.Add(config);
                }
                else
                {
                    throw new System.Exception(validationResult.Errors.First().ErrorMessage);
                }
            }
        }

        public void ImportFromJsonListConfig(string json)
        {
            List<NetConfigBase> configList = JsonConvert.DeserializeObject<List<NetConfigBase>>(json);
            AddNewListConfig(configList);
        }

        public void SaveToFileLisConfig(string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(GetConfigList(), Formatting.Indented));

            //// serialize JSON directly to a file
            //using (StreamWriter file = File.CreateText(fileName))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(file, GetConfigList() );
            //}
        }

        public string GetAddress()
        {
            return _netConfigDal is ISQLiteDBProperties ? (_netConfigDal as ISQLiteDBProperties).GetDBPath() : "";
        }
    }
}
