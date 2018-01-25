using Game.Domain;
using System.Data.Entity.ModelConfiguration;


namespace Game.Infra.Mapping
{
    public class SessionMap : EntityTypeConfiguration<Session>
    {
        public SessionMap()
        {
            ToTable("Session");
            HasKey(x => x.Id);
            Property(x => x.UserId).IsRequired();
            Property(x => x.IsActive).IsRequired();

            HasRequired(x => x.User);

        }
    }
}
