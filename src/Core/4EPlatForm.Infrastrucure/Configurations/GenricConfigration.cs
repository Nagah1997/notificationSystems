using _4EPlatform.Domain.Models.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatForm.Infrastrucure.Configurations
{
    public class GenricConfigration<T, Key> : IEntityTypeConfiguration<T> where T : BaseEntity<Key>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasQueryFilter(e => e.State == State.NotDeleted);

        }
    }
}
