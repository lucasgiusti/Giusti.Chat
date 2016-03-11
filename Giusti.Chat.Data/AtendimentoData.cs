using System.Collections.Generic;
using System.Linq;
using System.Data;
using Giusti.Chat.Model;
using Giusti.Chat.Data.Library;

namespace Giusti.Chat.Data
{
    public class AtendimentoData : DataBase
    {
        public void SalvaUsuario(Usuario itemGravar)
        {
            Usuario itemBase = Context.Usuarios.Include("UsuarioAtendimentos").Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Usuarios.Create();
                itemBase.UsuarioAtendimentos = new List<UsuarioAtendimento>();
                Context.Entry<Usuario>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Usuario>(itemBase, itemGravar);

            foreach (UsuarioAtendimento itemUsuarioAtendimento in new List<UsuarioAtendimento>(itemBase.UsuarioAtendimentos))
            {
                if (!itemGravar.UsuarioAtendimentos.Where(f => f.Id == itemUsuarioAtendimento.Id).Any())
                {
                    Context.Entry<UsuarioAtendimento>(itemUsuarioAtendimento).State = System.Data.Entity.EntityState.Deleted;
                }
            }
            foreach (UsuarioAtendimento itemUsuarioAtendimento in new List<UsuarioAtendimento>(itemGravar.UsuarioAtendimentos))
            {
                UsuarioAtendimento itemBaseUsuarioAtendimento = !itemUsuarioAtendimento.Id.HasValue ? null : itemBase.UsuarioAtendimentos.Where(f => f.Id == itemUsuarioAtendimento.Id).FirstOrDefault();
                if (itemBaseUsuarioAtendimento == null)
                {
                    itemBaseUsuarioAtendimento = Context.UsuarioAtendimentos.Create();
                    itemBase.UsuarioAtendimentos.Add(itemBaseUsuarioAtendimento);
                }
                else {
                    Context.Entry<UsuarioAtendimento>(itemBaseUsuarioAtendimento).State = System.Data.Entity.EntityState.Modified;
                }
                AtualizaPropriedades<UsuarioAtendimento>(itemBaseUsuarioAtendimento, itemUsuarioAtendimento);
            }

            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
    }
}
