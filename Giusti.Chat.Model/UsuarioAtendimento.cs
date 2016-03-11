using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;

namespace Giusti.Chat.Model
{
    public class UsuarioAtendimento
    {
        public UsuarioAtendimento()
        {   
        }

        public int? Id { get; set; }
        public int? UsuarioId { get; set; }
        public int? Situacao { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Usuario Clone()
        {
            return (Usuario)this.MemberwiseClone();
        }
    }
}
