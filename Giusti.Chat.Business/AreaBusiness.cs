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
        public Area RetornaArea_Id(int id)
        {
            LimpaValidacao();
            Area RetornoAcao = null;
            if (IsValid())
            {
                using (AreaData data = new AreaData())
                {
                    RetornoAcao = data.RetornaArea_Id(id);
                }
            }

            return RetornoAcao;
        }
        public IList<Area> RetornaAreas()
        {
            LimpaValidacao();
            IList<Area> RetornoAcao = new List<Area>();
            if (IsValid())
            {
                using (AreaData data = new AreaData())
                {
                    RetornoAcao = data.RetornaAreas();
                }
            }

            return RetornoAcao;
        }

        public void SalvaArea(Area itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar);
            if (IsValid())
            {
                using (AreaData data = new AreaData())
                {
                    data.SalvaArea(itemGravar);
                    IncluiSucessoBusiness("Area_SalvaAreaOK");
                }
            }
        }
        public void ExcluiArea(Area itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (AreaData data = new AreaData())
                {
                    data.ExcluiArea(itemGravar);
                    IncluiSucessoBusiness("Area_ExcluiAreaOK");
                }
            }
        }

        public void ValidaRegrasSalvar(Area itemGravar)
        {
            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Nome))
                IncluiErroBusiness("Area_Nome");
        }
        public void ValidaRegrasExcluir(Area itemGravar)
        {
            if (IsValid())
                ValidaExistencia(itemGravar);
        }
        public void ValidaExistencia(Area itemGravar)
        {
            if (itemGravar == null)
                IncluiErroBusiness("Area_NaoEncontrada");
        }
    }
}
