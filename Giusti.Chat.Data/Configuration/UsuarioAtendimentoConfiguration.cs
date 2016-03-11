using System.Data.Entity.ModelConfiguration;
using Giusti.Chat.Model;

namespace Giusti.Chat.Data.Configuration
{
    public partial class UsuarioAtendimentoConfiguration : EntityTypeConfiguration<UsuarioAtendimento>
    {
        public UsuarioAtendimentoConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("UsuarioAtendimento");
            else
                this.ToTable("UsuarioAtendimento", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.UsuarioId).HasColumnName("UsuarioId");
            this.Property(i => i.Situacao).HasColumnName("Situacao");
            this.Property(i => i.DataInclusao).HasColumnName("DataInclusao");
            this.Property(i => i.DataAlteracao).HasColumnName("DataAlteracao");
        }
    }
}
