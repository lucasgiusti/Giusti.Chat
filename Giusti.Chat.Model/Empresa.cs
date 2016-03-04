using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;

namespace Giusti.Chat.Model
{
    [HasSelfValidation()]
    public class Empresa
    {
        public Empresa()
        {
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        [SelfValidation]
        private void ValidaNome(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (Nome != null && Nome.Length > 100)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Empresa_NomeTamanho, this, "Nome", null, null);
                results.AddResult(result);
            }
        }
        public string EmailUsuarioAdm { get; set; }
        [SelfValidation]
        private void ValidaEmailUsuarioAdm(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (EmailUsuarioAdm != null && EmailUsuarioAdm.Length > 100)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Empresa_EmailUsuarioAdmTamanho, this, "Email", null, null);
                results.AddResult(result);
            }
        }
        public string SenhaUsuarioAdm { get; set; }
        [SelfValidation]
        private void ValidaSenhaUsuarioAdm(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (SenhaUsuarioAdm != null && SenhaUsuarioAdm.Length > 300)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Empresa_SenhaUsuarioAdmTamanho, this, "Senha", null, null);
                results.AddResult(result);
            }
        }
        public string NovaSenhaUsuarioAdm { get; set; }
        public string SenhaUsuarioAdmConfirmacao { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Usuario Clone()
        {
            return (Usuario)this.MemberwiseClone();
        }
    }
}
