using FluentValidation.Results;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.Data.Validation;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;
using System.Collections.Generic;
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
            ValidationResult validationResult =  validationRules.Validate(config);
    
            if(validationResult.IsValid)
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
    }
}
