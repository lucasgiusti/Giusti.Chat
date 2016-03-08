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

        public UsuarioLogado EfetuaLoginSistema(string email, string senha, string ip, string nomeMaquina)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(email))
                IncluiErroBusiness("Usuario_Email");

            if (string.IsNullOrEmpty(senha))
                IncluiErroBusiness("Usuario_Senha");

            UsuarioLogado retorno = null;
            if (IsValid())
            {
                EmpresaBusiness bizEmpresa = new EmpresaBusiness();
                Empresa itemBase = bizEmpresa.RetornaEmpresa_EmailUsuarioAdm(email);

                if (itemBase == null)
                    IncluiErroBusiness("Usuario_EmailInvalido");

                if (IsValid() && !PasswordHash.ValidatePassword(senha, itemBase.SenhaUsuarioAdm))
                    IncluiErroBusiness("Usuario_SenhaInvalida");

                if (IsValid())
                {
                    retorno = new UsuarioLogado();
                    retorno.Id = itemBase.Id.Value;
                    retorno.DataHoraAcesso = DateTime.Now;
                    retorno.Email = itemBase.EmailUsuarioAdm;
                    retorno.Nome = itemBase.Nome;
                    retorno.WorkstationId = nomeMaquina;

                    PerfilUsuarioBusiness bizPerfilUsuario = new PerfilUsuarioBusiness();
                    IList<string> listFuncionalidade = bizPerfilUsuario.RetornaFuncionalidades_UsuarioId((int)itemBase.Id);

                    UsuarioBusiness bizUsuario = new UsuarioBusiness();
                    retorno.Token = bizUsuario.GeraToken(email, string.Join(",", new List<string>() {
                        Constantes.FuncionalidadeAlterarSenha,
                        Constantes.FuncionalidadeArea,
                        Constantes.FuncionalidadeAreaConsulta,
                        Constantes.FuncionalidadeAreaEdicao,
                        Constantes.FuncionalidadeAreaInclusao,
                        Constantes.FuncionalidadeAreaExclusao }));
                }

            }
            return retorno;
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
        public void AlteraSenha(Empresa itemGravar)
        {
            LimpaValidacao();
            ValidaRegrasAlterarSenha(ref itemGravar);
            if (IsValid())
            {
                ValidateService(itemGravar);
                ValidaRegrasSalvar(itemGravar);
                if (IsValid())
                {
                    using (EmpresaData data = new EmpresaData())
                    {
                        data.SalvaEmpresa(itemGravar);
                        IncluiSucessoBusiness("Usuario_SenhaAlteradaOK");
                    }
                }
            }
        }
        public void GeraNovaSenha(Empresa itemGravar)
        {
            LimpaValidacao();
            ValidaRegrasGerarNovaSenha(ref itemGravar);
            if (IsValid())
            {
                string novaSenha = string.Empty;
                novaSenha = PasswordHash.GenerateRandomPassword();
                itemGravar.SenhaUsuarioAdm = novaSenha;
                itemGravar.SenhaUsuarioAdmConfirmacao = novaSenha;

                ValidateService(itemGravar);
                ValidaRegrasSalvar(itemGravar);
                if (IsValid())
                {
                    using (EmpresaData data = new EmpresaData())
                    {
                        data.SalvaEmpresa(itemGravar);
                        IncluiSucessoBusiness("Empresa_NovaSenhaGeradaOK");

                        GeraEmailEsqueciSenha(itemGravar, novaSenha);
                    }
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
        public void ValidaRegrasAlterarSenha(ref Empresa itemGravar)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(itemGravar.EmailUsuarioAdm))
                IncluiErroBusiness("Usuario_Email");

            if (string.IsNullOrEmpty(itemGravar.SenhaUsuarioAdm))
                IncluiErroBusiness("Usuario_Senha");

            if (string.IsNullOrEmpty(itemGravar.NovaSenhaUsuarioAdm))
                IncluiErroBusiness("Usuario_NovaSenha");

            if (string.IsNullOrEmpty(itemGravar.SenhaUsuarioAdmConfirmacao))
                IncluiErroBusiness("Usuario_NovaSenhaConfirmacao");

            if (IsValid())
            {
                Empresa itemBase = RetornaEmpresa_EmailUsuarioAdm(itemGravar.EmailUsuarioAdm);

                if (itemBase == null)
                    IncluiErroBusiness("Usuario_EmailInvalido");

                if (IsValid() && !PasswordHash.ValidatePassword(itemGravar.SenhaUsuarioAdm, itemBase.SenhaUsuarioAdm))
                    IncluiErroBusiness("Usuario_SenhaInvalida");

                if (IsValid())
                {
                    itemBase.SenhaUsuarioAdm = itemGravar.NovaSenhaUsuarioAdm;
                    itemBase.SenhaUsuarioAdmConfirmacao = itemGravar.SenhaUsuarioAdmConfirmacao;
                    itemGravar = itemBase;
                }
            }
        }
        public void ValidaRegrasGerarNovaSenha(ref Empresa itemGravar)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(itemGravar.EmailUsuarioAdm))
                IncluiErroBusiness("Usuario_Email");

            if (IsValid())
            {
                Empresa itemBase = RetornaEmpresa_EmailUsuarioAdm(itemGravar.EmailUsuarioAdm);

                if (itemBase == null)
                    IncluiErroBusiness("Usuario_EmailInvalido");

                if (IsValid())
                    itemGravar = itemBase;
            }
        }
        public void ValidaExistencia(Empresa itemGravar)
        {
            if (itemGravar == null)
                IncluiErroBusiness("Empresa_NaoEncontrada");
        }

        public void GeraEmailEsqueciSenha(Empresa itemGravar, string novaSenha)
        {
            EmailBusiness biz = new EmailBusiness();
            biz.GeraEmailEsqueciSenha(itemGravar.Nome, itemGravar.EmailUsuarioAdm, novaSenha);
        }
    }
}
