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
        public bool? Ativo { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Usuario Clone()
        {
            return (Usuario)this.MemberwiseClone();
        }
    }
}
