using SN.Data.Entities;
using SQLite;

namespace SN.NetSet.Entities.Concrete.User
{
    public class User : IEntity
    {
        [PrimaryKey,AutoIncrement]
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }

        public IEntity Clone()
        {
            return (IEntity)this.MemberwiseClone();
        }
    }
}
