using System.Collections.Generic;
using System.Linq;
using System.Data;
using Giusti.Chat.Model;
using Giusti.Chat.Data.Library;

namespace Giusti.Chat.Data
{
    public class AreaData : DataBase
    {
        public Area RetornaArea_Id(int id)
        {
            IQueryable<Area> query = Context.Areas;

            query = query.Where(d => d.Id == id);
            return query.FirstOrDefault();
        }
        public IList<Area> RetornaAreas()
        {
            IQueryable<Area> query = Context.Areas;

            return query.ToList();
        }

        public void SalvaArea(Area itemGravar)
        {
            Area itemBase = Context.Areas.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Areas.Create();
                Context.Entry<Area>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Area>(itemBase, itemGravar);

            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
        public void ExcluiArea(Area itemGravar)
        {
            Area itemExcluir = Context.Areas.Where(f => f.Id == itemGravar.Id).FirstOrDefault();

            Context.Entry<Area>(itemExcluir).State = System.Data.Entity.EntityState.Deleted;
            Context.SaveChanges();
        }
    }
}
