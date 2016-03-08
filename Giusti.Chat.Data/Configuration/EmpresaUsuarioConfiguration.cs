using System.Data.Entity.ModelConfiguration;
using Giusti.Chat.Model;

namespace Giusti.Chat.Data.Configuration
{
    public partial class EmpresaUsuarioConfiguration : EntityTypeConfiguration<EmpresaUsuario>
    {
        public EmpresaUsuarioConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("EmpresaUsuario");
            else
                this.ToTable("EmpresaUsuario", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.EmpresaId).HasColumnName("EmpresaId");
            this.Property(i => i.AreaId).HasColumnName("AreaId");
            this.Property(i => i.Nome).HasColumnName("Nome");
            this.Property(i => i.Email).HasColumnName("Email");
            this.Property(i => i.Senha).HasColumnName("Senha");
            this.Property(i => i.Ativo).HasColumnName("Ativo");
            this.Property(i => i.Disponivel).HasColumnName("Disponivel");
            this.Property(i => i.TotalAtendimentosPossiveis).HasColumnName("TotalAtendimentosPossiveis");
            this.Property(i => i.Supervisor).HasColumnName("Supervisor");
            this.HasRequired(i => i.Empresa).WithMany().HasForeignKey(d => d.EmpresaId);
            this.HasRequired(i => i.Area).WithMany().HasForeignKey(d => d.AreaId);
            this.Property(i => i.DataInclusao).HasColumnName("DataInclusao");
            this.Property(i => i.DataAlteracao).HasColumnName("DataAlteracao");
            this.Ignore(i => i.NovaSenha);
            this.Ignore(i => i.SenhaConfirmacao);
        }
    }
}
