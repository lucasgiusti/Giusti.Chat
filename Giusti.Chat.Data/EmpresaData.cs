﻿using System.Collections.Generic;
using System.Linq;
using System.Data;
using Giusti.Chat.Model;
using Giusti.Chat.Data.Library;

namespace Giusti.Chat.Data
{
    public class EmpresaData : DataBase
    {
        public Empresa RetornaEmpresa_Id(int id)
        {
            IQueryable<Empresa> query = Context.Empresas;

            query = query.Where(d => d.Id == id);
            return query.FirstOrDefault();
        }
        public Empresa RetornaEmpresa_Id(int id, int? empresaId)
        {
            IQueryable<Empresa> query = Context.Empresas;

            query = query.Where(d => d.Id == id && d.Id == empresaId);
            return query.FirstOrDefault();
        }
        public bool ExisteUsuario_EmpresaId(int empresaId)
        {
            IQueryable<Usuario> query = Context.Usuarios;

            query = query.Where(d => d.EmpresaId == empresaId);
            return query.Any();
        }
        public bool ExisteArea_EmpresaId(int empresaId)
        {
            IQueryable<Area> query = Context.Areas;

            query = query.Where(d => d.EmpresaId == empresaId);
            return query.Any();
        }
        public IList<Empresa> RetornaEmpresas()
        {
            IQueryable<Empresa> query = Context.Empresas;

            return query.ToList();
        }
        public IList<Empresa> RetornaEmpresas(int? empresaId)
        {
            IQueryable<Empresa> query = Context.Empresas.Where(a => a.Id == empresaId);

            return query.ToList();
        }

        public void SalvaEmpresa(Empresa itemGravar)
        {
            Empresa itemBase = Context.Empresas.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Empresas.Create();
                Context.Entry<Empresa>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Empresa>(itemBase, itemGravar);

            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
        public void ExcluiEmpresa(Empresa itemGravar)
        {
            Empresa itemExcluir = Context.Empresas.Where(f => f.Id == itemGravar.Id).FirstOrDefault();

            Context.Entry<Empresa>(itemExcluir).State = System.Data.Entity.EntityState.Deleted;
            Context.SaveChanges();
        }

    }
}
