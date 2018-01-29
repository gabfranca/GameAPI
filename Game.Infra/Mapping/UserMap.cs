using Game.Domain;
using System.Data.Entity.ModelConfiguration;


namespace Game.Infra.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");
            HasKey(x => x.Id);
            Property(x=>x.Name).HasMaxLength(60).IsRequired();
            Property(x => x.Nickname).HasMaxLength(15).IsRequired();
            Property(x => x.Password).HasMaxLength(10).IsRequired();



        }
    }
}
