using DynamicMenu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicMenu.Infrastructure.Data.Configurations
{
    public class RemoteMenuConfigConfiguration : IEntityTypeConfiguration<RemoteMenuConfig>
    {
        public void Configure(EntityTypeBuilder<RemoteMenuConfig> builder)
        {
            
        }
    }
} 