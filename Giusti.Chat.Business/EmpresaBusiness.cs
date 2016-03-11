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
    public class EmpresaBusiness : BusinessBase
    {
        public Empresa RetornaEmpresa_Id(int id)
        {
            LimpaValidacao();
            Empresa RetornoAcao = null;
            if (IsValid())
            {
                using (EmpresaData data = new EmpresaData())
                {
                    RetornoAcao = data.RetornaEmpresa_Id(id);
                }
            }

            return RetornoAcao;
        }
        public Empresa RetornaEmpresa_Id(int id, int? empresaId)
        {
            LimpaValidacao();
            Empresa RetornoAcao = null;
            if (IsValid())
            {
                using (EmpresaData data = new EmpresaData())
                {
                    RetornoAcao = data.RetornaEmpresa_Id(id, empresaId);
                }
            }

            return RetornoAcao;
        }
        public bool ExisteUsuario_EmpresaId(int empresaId)
        {
            LimpaValidacao();
            bool RetornoAcao = false;
            if (IsValid())
            {
                using (EmpresaData data = new EmpresaData())
                {
                    RetornoAcao = data.ExisteUsuario_EmpresaId(empresaId);
                }
            }

            return RetornoAcao;
        }
        public bool ExisteArea_EmpresaId(int empresaId)
        {
            LimpaValidacao();
            bool RetornoAcao = false;
            if (IsValid())
            {
                using (EmpresaData data = new EmpresaData())
                {
                    RetornoAcao = data.ExisteArea_EmpresaId(empresaId);
                }
            }

            return RetornoAcao;
        }
        public IList<Empresa> RetornaEmpresas()
        {
            LimpaValidacao();
            IList<Empresa> RetornoAcao = new List<Empresa>();
            if (IsValid())
            {
                using (EmpresaData data = new EmpresaData())
                {
                    RetornoAcao = data.RetornaEmpresas();
                }
            }

            return RetornoAcao;
        }
        public IList<Empresa> RetornaEmpresas(int? empresaId)
        {
            LimpaValidacao();
            IList<Empresa> RetornoAcao = new List<Empresa>();
            if (IsValid())
            {
                using (EmpresaData data = new EmpresaData())
                {
                    RetornoAcao = data.RetornaEmpresas(empresaId);
                }
            }

            return RetornoAcao;
        }

        public void SalvaEmpresa(Empresa itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar);
            if (IsValid())
            {
                using (EmpresaData data = new EmpresaData())
                {
                    data.SalvaEmpresa(itemGravar);
                    IncluiSucessoBusiness("Empresa_SalvaEmpresaOK");
                }
            }
        }
        public void ExcluiEmpresa(Empresa itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (EmpresaData data = new EmpresaData())
                {
                    data.ExcluiEmpresa(itemGravar);
                    IncluiSucessoBusiness("Empresa_ExcluiEmpresaOK");
                }
            }
        }
        
        public void ValidaRegrasSalvar(Empresa itemGravar)
        {
            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Nome))
                IncluiErroBusiness("Empresa_Nome");

            if (IsValid())
            {
                if (itemGravar.Id.HasValue)
                {
                    Empresa itemBase = RetornaEmpresa_Id((int)itemGravar.Id);
                    ValidaExistencia(itemBase);
                    if (IsValid())
                    {
                        itemGravar.Chave = itemBase.Chave;
                        itemGravar.DataInclusao = itemBase.DataInclusao;
                        itemGravar.DataAlteracao = DateTime.Now;
                    }
                }
                else
                {
                    itemGravar.Chave = Guid.NewGuid().ToString();
                    itemGravar.DataInclusao = DateTime.Now;
                    itemGravar.Ativo = true;
                }
            }

            if (IsValid() && itemGravar.Id == (int)Constantes.EmpresaMasterId)
                IncluiErroBusiness("Empresa_SemPermissaoEdicaoExclusao");
        }
        public void ValidaRegrasExcluir(Empresa itemGravar)
        {
            if (IsValid())
            {
                Empresa itemBase = RetornaEmpresa_Id((int)itemGravar.Id);
                ValidaExistencia(itemBase);
            }

            if (IsValid() && itemGravar.Id == (int)Constantes.EmpresaMasterId)
                IncluiErroBusiness("Empresa_SemPermissaoEdicaoExclusao");

            if (IsValid() && ExisteUsuario_EmpresaId((int)itemGravar.Id))
                IncluiErroBusiness("Empresa_CadastroUtilizado");

            if (IsValid() && ExisteArea_EmpresaId((int)itemGravar.Id))
                IncluiErroBusiness("Empresa_CadastroUtilizado");
        }
        public void ValidaExistencia(Empresa itemGravar)
        {
            if (itemGravar == null)
                IncluiErroBusiness("Empresa_NaoEncontrada");
        }
    }
}
