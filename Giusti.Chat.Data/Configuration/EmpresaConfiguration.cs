using System.Data.Entity.ModelConfiguration;
using Giusti.Chat.Model;

namespace Giusti.Chat.Data.Configuration
{
    public partial class EmpresaConfiguration : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("Empresa");
            else
                this.ToTable("Empresa", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.Nome).HasColumnName("Nome");
            this.Property(i => i.EmailUsuarioAdm).HasColumnName("EmailUsuarioAdm");
            this.Property(i => i.SenhaUsuarioAdm).HasColumnName("SenhaUsuarioAdm");
            this.Property(i => i.Ativo).HasColumnName("Ativo");
            this.Property(i => i.DataInclusao).HasColumnName("DataInclusao");
            this.Property(i => i.DataAlteracao).HasColumnName("DataAlteracao");
            this.Ignore(i => i.NovaSenhaUsuarioAdm);
            this.Ignore(i => i.SenhaUsuarioAdmConfirmacao);
        }
    }
}
