using System.Collections.Generic;
using System.Linq;
using System.Data;
using Giusti.Chat.Model;
using Giusti.Chat.Data.Library;

namespace Giusti.Chat.Data
{
    public class EmpresaUsuarioData : DataBase
    {
        public EmpresaUsuario RetornaEmpresaUsuario_Id(int id, int? empresaId)
        {
            IQueryable<EmpresaUsuario> query = Context.EmpresaUsuarios.Include("Empresa").Include("Area");

            query = query.Where(d => d.Id == id);

            if (empresaId.HasValue)
                query = query.Where(a => a.EmpresaId == empresaId);

            return query.FirstOrDefault();
        }
        public EmpresaUsuario RetornaEmpresaUsuario_Email(string email)
        {
            IQueryable<EmpresaUsuario> query = Context.EmpresaUsuarios.Include("Empresa").Include("Area");

            if (!string.IsNullOrEmpty(email))
                query = query.Where(d => d.Email == email);
            return query.FirstOrDefault();
        }
        public IList<EmpresaUsuario> RetornaEmpresaUsuarios(int? empresaId)
        {
            IQueryable<EmpresaUsuario> query = Context.EmpresaUsuarios;

            if (empresaId.HasValue)
                query = query.Where(a => a.EmpresaId == empresaId);

            return query.ToList();
        }

        public void SalvaEmpresaUsuario(EmpresaUsuario itemGravar)
        {
            EmpresaUsuario itemBase = Context.EmpresaUsuarios.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.EmpresaUsuarios.Create();
                Context.Entry<EmpresaUsuario>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<EmpresaUsuario>(itemBase, itemGravar);

            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
        public void ExcluiEmpresaUsuario(EmpresaUsuario itemGravar)
        {
            EmpresaUsuario itemExcluir = Context.EmpresaUsuarios.Where(f => f.Id == itemGravar.Id).FirstOrDefault();

            Context.Entry<EmpresaUsuario>(itemExcluir).State = System.Data.Entity.EntityState.Deleted;
            Context.SaveChanges();
        }
    }
}
