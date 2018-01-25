using Game.Domain;
using System.Data.Entity.ModelConfiguration;



namespace Game.Infra.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {

        public CategoryMap()
        {
            ToTable("Category");
            HasKey(x => x.Id);
            Property(x=>x.Name).IsRequired();
        }
    }
}
