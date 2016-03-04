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
        public Empresa RetornaEmpresa_EmailUsuarioAdm(string emailUsuarioAdm)
        {
            LimpaValidacao();
            Empresa RetornoAcao = null;
            if (IsValid())
            {
                using (EmpresaData data = new EmpresaData())
                {
                    RetornoAcao = data.RetornaEmpresa_EmailUsuarioAdm(emailUsuarioAdm);
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

            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.EmailUsuarioAdm))
                IncluiErroBusiness("Empresa_EmailUsuarioAdm");

            if (IsValid())
            {
                Empresa itemBase = RetornaEmpresa_EmailUsuarioAdm(itemGravar.EmailUsuarioAdm);
                if (itemBase != null && itemGravar.Id != itemBase.Id)
                    IncluiErroBusiness("Empresa_CadastroDuplicado");
            }

            if (IsValid())
            {
                if (itemGravar.Id.HasValue)
                {
                    Empresa itemBase = RetornaEmpresa_Id((int)itemGravar.Id);
                    ValidaExistencia(itemBase);
                    if (IsValid())
                    {
                        itemGravar.DataInclusao = itemBase.DataInclusao;
                        itemGravar.DataAlteracao = DateTime.Now;

                        if (string.IsNullOrWhiteSpace(itemGravar.SenhaUsuarioAdm) && string.IsNullOrWhiteSpace(itemGravar.SenhaUsuarioAdmConfirmacao))
                            itemGravar.SenhaUsuarioAdm = itemBase.SenhaUsuarioAdm;
                        else
                        {
                            if (string.IsNullOrWhiteSpace(itemGravar.SenhaUsuarioAdm))
                                IncluiErroBusiness("Empresa_SenhaUsuarioAdm");

                            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.SenhaUsuarioAdmConfirmacao))
                                IncluiErroBusiness("Empresa_SenhaUsuarioAdmConfirmacao");

                            if (IsValid() && !itemGravar.SenhaUsuarioAdm.Equals(itemGravar.SenhaUsuarioAdmConfirmacao))
                                IncluiErroBusiness("Empresa_SenhaUsuarioAdmConfirmacao_Incorreta");

                            if (IsValid())
                                itemGravar.SenhaUsuarioAdm = PasswordHash.HashPassword(itemGravar.SenhaUsuarioAdm);
                        }
                    }
                }
                else
                {
                    itemGravar.DataInclusao = DateTime.Now;
                    itemGravar.Ativo = true;

                    if (string.IsNullOrWhiteSpace(itemGravar.SenhaUsuarioAdm))
                        IncluiErroBusiness("Empresa_SenhaUsuarioAdm");

                    if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.SenhaUsuarioAdmConfirmacao))
                        IncluiErroBusiness("Empresa_SenhaUsuarioAdmConfirmacao");

                    if (IsValid() && !itemGravar.SenhaUsuarioAdm.Equals(itemGravar.SenhaUsuarioAdmConfirmacao))
                        IncluiErroBusiness("Empresa_SenhaUsuarioAdmConfirmacao_Incorreta");

                    if (IsValid())
                        itemGravar.SenhaUsuarioAdm = PasswordHash.HashPassword(itemGravar.SenhaUsuarioAdm);
                }
            }


        }
        public void ValidaRegrasExcluir(Empresa itemGravar)
        {
            if (IsValid())
                ValidaExistencia(itemGravar);


        }
        public void ValidaExistencia(Empresa itemGravar)
        {
            if (itemGravar == null)
                IncluiErroBusiness("Empresa_NaoEncontrada");
        }
    }
}
