using System.Data.Entity.ModelConfiguration;
using Giusti.Chat.Model;

namespace Giusti.Chat.Data.Configuration
{
    public partial class AreaConfiguration : EntityTypeConfiguration<Area>
    {
        public AreaConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("Area");
            else
                this.ToTable("Area", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.EmpresaId).HasColumnName("EmpresaId");
            this.Property(i => i.Nome).HasColumnName("Nome");
            this.HasRequired(i => i.Empresa).WithMany().HasForeignKey(d => d.EmpresaId);
            this.Property(i => i.DataInclusao).HasColumnName("DataInclusao");
            this.Property(i => i.DataAlteracao).HasColumnName("DataAlteracao");
        }
    }
}
