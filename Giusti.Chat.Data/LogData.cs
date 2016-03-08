using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using Giusti.Chat.Model;
using Giusti.Chat.Data.Library;

namespace Giusti.Chat.Data
{
    public class LogData : DataBase
    {
        public IList<Log> RetornaLogs()
        {
            IQueryable<Log> query = Context.Logs.Include("Usuario").Include("Usuario.Empresa").OrderByDescending(a => a.Id);

            return query.ToList();
        }
        public IList<Log> RetornaLogs(int? empresaId)
        {
            IQueryable<Log> query = Context.Logs.Include("Usuario").Include("Usuario.Empresa").Where(a => a.Usuario.EmpresaId == empresaId).OrderByDescending(a => a.Id);

            return query.ToList();
        }

        public void SalvaLog(Log itemGravar)
        {
            Log itemBase = Context.Logs
            .Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Logs.Create();
                Context.Entry<Log>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Log>(itemBase, itemGravar);
            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
    }
}
