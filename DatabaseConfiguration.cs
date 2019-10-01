using Kros.KORM;
using Kros.KORM.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.CloundDesignPatterns
{
    public class DatabaseConfiguration : DatabaseConfigurationBase
    {
        public override void OnModelCreating(ModelConfigurationBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Photo>()
                .HasTableName("Photos")
                .HasPrimaryKey(p => p.Id).AutoIncrement();
        }
    }
}
