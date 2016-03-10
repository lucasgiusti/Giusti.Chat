using System.Collections.Generic;
using System.Linq;
using System.Data;
using Giusti.Chat.Model;
using Giusti.Chat.Data.Library;

namespace Giusti.Chat.Data
{
    public class UsuarioData : DataBase
    {
        public Usuario RetornaUsuario_Id(int id)
        {
            IQueryable<Usuario> query = Context.Usuarios.Include("Perfis").Include("Empresa");

            query = query.Where(d => d.Id == id);
            return query.FirstOrDefault();
        }
        public Usuario RetornaUsuario_Id(int id, int? empresaId)
        {
            IQueryable<Usuario> query = Context.Usuarios.Include("Perfis").Include("Empresa");

            query = query.Where(d => d.Id == id && d.EmpresaId == empresaId);
            return query.FirstOrDefault();
        }
        public bool ExisteLog_UsuarioId(int usuarioId)
        {
            IQueryable<Log> query = Context.Logs;

            query = query.Where(d => d.UsuarioId == usuarioId);
            return query.Any();
        }
        public Usuario RetornaUsuario_Email(string email)
        {
            IQueryable<Usuario> query = Context.Usuarios.Include("Perfis").Include("Empresa");

            if (!string.IsNullOrEmpty(email))
                query = query.Where(d => d.Email == email);
            return query.FirstOrDefault();
        }
        public IList<Usuario> RetornaUsuarios()
        {
            IQueryable<Usuario> query = Context.Usuarios.Include("Empresa");

            return query.ToList();
        }
        public IList<Usuario> RetornaUsuarios(int? empresaId)
        {
            IQueryable<Usuario> query = Context.Usuarios.Include("Empresa");

            query = query.Where(a => a.EmpresaId == empresaId);

            return query.ToList();
        }

        public void SalvaUsuario(Usuario itemGravar)
        {
            Usuario itemBase = Context.Usuarios.Include("Perfis").Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Usuarios.Create();
                itemBase.Perfis = new List<PerfilUsuario>();
                Context.Entry<Usuario>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Usuario>(itemBase, itemGravar);

            foreach (PerfilUsuario itemPerfilUsuario in new List<PerfilUsuario>(itemBase.Perfis))
            {
                if (!itemGravar.Perfis.Where(f => f.Id == itemPerfilUsuario.Id).Any())
                {
                    Context.Entry<PerfilUsuario>(itemPerfilUsuario).State = System.Data.Entity.EntityState.Deleted;
                }
            }
            foreach (PerfilUsuario itemPerfilUsuario in new List<PerfilUsuario>(itemGravar.Perfis))
            {
                PerfilUsuario itemBasePerfilUsuario = !itemPerfilUsuario.Id.HasValue ? null : itemBase.Perfis.Where(f => f.Id == itemPerfilUsuario.Id).FirstOrDefault();
                if (itemBasePerfilUsuario == null)
                {
                    itemBasePerfilUsuario = Context.PerfilUsuarios.Create();
                    itemBase.Perfis.Add(itemBasePerfilUsuario);
                }
                AtualizaPropriedades<PerfilUsuario>(itemBasePerfilUsuario, itemPerfilUsuario);
            }

            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
        public void ExcluiUsuario(Usuario itemGravar)
        {
            Usuario itemExcluir = Context.Usuarios.Where(f => f.Id == itemGravar.Id).FirstOrDefault();

            Context.Entry<Usuario>(itemExcluir).State = System.Data.Entity.EntityState.Deleted;
            Context.SaveChanges();
        }
    }
}
