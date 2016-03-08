using System.Collections.Generic;
using System.Linq;
using Giusti.Chat.Model.Results;
using Giusti.Chat.Business.Library;
using Giusti.Chat.Data;
using Giusti.Chat.Model;
using System;
using Giusti.Chat.Model.Dominio;
using System.Web.Security;

namespace Giusti.Chat.Business
{
    public class AreaBusiness : BusinessBase
    {
        public Area RetornaArea_Id(int id, int? empresaId)
        {
            LimpaValidacao();
            Area RetornoAcao = null;
            if (IsValid())
            {
                using (AreaData data = new AreaData())
                {
                    RetornoAcao = data.RetornaArea_Id(id, empresaId);
                }
            }

            return RetornoAcao;
        }
        public IList<Area> RetornaAreas(int? empresaId)
        {
            LimpaValidacao();
            IList<Area> RetornoAcao = new List<Area>();
            if (IsValid())
            {
                using (AreaData data = new AreaData())
                {
                    RetornoAcao = data.RetornaAreas(empresaId);
                }
            }

            return RetornoAcao;
        }

        public void SalvaArea(Area itemGravar, int? empresaId)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar, empresaId);
            if (IsValid())
            {
                using (AreaData data = new AreaData())
                {
                    data.SalvaArea(itemGravar);
                    IncluiSucessoBusiness("Area_SalvaAreaOK");
                }
            }
        }
        public void ExcluiArea(Area itemGravar, int? empresaId)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar, empresaId);
            if (IsValid())
            {
                using (AreaData data = new AreaData())
                {
                    data.ExcluiArea(itemGravar);
                    IncluiSucessoBusiness("Area_ExcluiAreaOK");
                }
            }
        }

        public void ValidaRegrasSalvar(Area itemGravar, int? empresaId)
        {
            if (itemGravar.Id.HasValue)
            {
                Area itemBase = RetornaArea_Id((int)itemGravar.Id, empresaId);
                ValidaExistencia(itemBase, empresaId);
                if (IsValid())
                {
                    itemGravar.DataInclusao = itemBase.DataInclusao;
                    itemGravar.DataAlteracao = DateTime.Now;
                }
            }
            else
                itemGravar.DataInclusao = DateTime.Now;

            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Nome))
                IncluiErroBusiness("Area_Nome");

            if (IsValid())
                itemGravar.EmpresaId = empresaId;
        }
        public void ValidaRegrasExcluir(Area itemGravar, int? empresaId)
        {
            if (IsValid())
            {
                Area itemBase = RetornaArea_Id((int)itemGravar.Id, empresaId);
                ValidaExistencia(itemGravar, empresaId);
            }

            
        }
        public void ValidaExistencia(Area itemGravar, int? empresaId)
        {
            if (itemGravar == null)
                IncluiErroBusiness("Area_NaoEncontrada");
        }
    }
}
