using DomainEnums = _4EPlatform.Domain.Enums.Enums;
using System.ComponentModel.DataAnnotations;

namespace _4EPlatform.Domain.Models.BaseEntities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { set; get; }

        public DomainEnums.State State { set; get; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
