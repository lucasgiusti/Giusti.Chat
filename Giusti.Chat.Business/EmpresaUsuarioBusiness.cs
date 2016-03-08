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
    public class EmpresaUsuarioBusiness : BusinessBase
    {
        public EmpresaUsuario RetornaEmpresaUsuario_Id(int id, int? empresaId)
        {
            LimpaValidacao();
            EmpresaUsuario RetornoAcao = null;
            if (IsValid())
            {
                using (EmpresaUsuarioData data = new EmpresaUsuarioData())
                {
                    RetornoAcao = data.RetornaEmpresaUsuario_Id(id, empresaId);
                }
            }

            return RetornoAcao;
        }
        public EmpresaUsuario RetornaEmpresaUsuario_Email(string email)
        {
            LimpaValidacao();
            EmpresaUsuario RetornoAcao = null;
            if (IsValid())
            {
                using (EmpresaUsuarioData data = new EmpresaUsuarioData())
                {
                    RetornoAcao = data.RetornaEmpresaUsuario_Email(email);
                }
            }

            return RetornoAcao;
        }
        public IList<EmpresaUsuario> RetornaEmpresaUsuarios(int? empresaId)
        {
            LimpaValidacao();
            IList<EmpresaUsuario> RetornoAcao = new List<EmpresaUsuario>();
            if (IsValid())
            {
                using (EmpresaUsuarioData data = new EmpresaUsuarioData())
                {
                    RetornoAcao = data.RetornaEmpresaUsuarios(empresaId);
                }
            }

            return RetornoAcao;
        }

        public UsuarioLogado EfetuaLoginSistema(string email, string senha, string ip, string nomeMaquina)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(email))
                IncluiErroBusiness("EmpresaUsuario_Email");

            if (string.IsNullOrEmpty(senha))
                IncluiErroBusiness("EmpresaUsuario_Senha");

            UsuarioLogado retorno = null;
            if (IsValid())
            {
                EmpresaUsuarioBusiness bizUsuario = new EmpresaUsuarioBusiness();
                EmpresaUsuario itemBase = bizUsuario.RetornaEmpresaUsuario_Email(email);

                if (itemBase == null)
                    IncluiErroBusiness("EmpresaUsuario_EmailInvalido");

                if (IsValid() && !PasswordHash.ValidatePassword(senha, itemBase.Senha))
                    IncluiErroBusiness("EmpresaUsuario_SenhaInvalida");

                if (IsValid())
                {
                    retorno = new UsuarioLogado();
                    retorno.Id = itemBase.Id.Value;
                    retorno.DataHoraAcesso = DateTime.Now;
                    retorno.Email = itemBase.Email;
                    retorno.Nome = itemBase.Nome;
                    retorno.WorkstationId = nomeMaquina;

                    retorno.Token = GeraToken(email, string.Join(",", string.Empty));
                }

            }
            return retorno;
        }
        public void SalvaEmpresaUsuario(EmpresaUsuario itemGravar, int? empresaId)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar, empresaId);
            if (IsValid())
            {
                using (EmpresaUsuarioData data = new EmpresaUsuarioData())
                {
                    data.SalvaEmpresaUsuario(itemGravar);
                    IncluiSucessoBusiness("EmpresaUsuario_SalvaEmpresaUsuarioOK");
                }
            }
        }
        public void ExcluiEmpresaUsuario(EmpresaUsuario itemGravar, int? empresaId)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (EmpresaUsuarioData data = new EmpresaUsuarioData())
                {
                    data.ExcluiEmpresaUsuario(itemGravar);
                    IncluiSucessoBusiness("EmpresaUsuario_ExcluiEmpresaUsuarioOK");
                }
            }
        }
        public void AlteraSenha(EmpresaUsuario itemGravar)
        {
            LimpaValidacao();
            ValidaRegrasAlterarSenha(ref itemGravar);
            if (IsValid())
            {
                ValidateService(itemGravar);
                ValidaRegrasSalvar(itemGravar, itemGravar.EmpresaId);
                if (IsValid())
                {
                    using (EmpresaUsuarioData data = new EmpresaUsuarioData())
                    {
                        data.SalvaEmpresaUsuario(itemGravar);
                        IncluiSucessoBusiness("EmpresaUsuario_SenhaAlteradaOK");
                    }
                }
            }
        }
        public void GeraNovaSenha(EmpresaUsuario itemGravar, int? empresaId)
        {
            LimpaValidacao();
            ValidaRegrasGerarNovaSenha(ref itemGravar);
            if (IsValid())
            {
                string novaSenha = string.Empty;
                novaSenha = PasswordHash.GenerateRandomPassword();
                itemGravar.Senha = novaSenha;
                itemGravar.SenhaConfirmacao = novaSenha;

                ValidateService(itemGravar);
                ValidaRegrasSalvar(itemGravar, empresaId);
                if (IsValid())
                {
                    using (EmpresaUsuarioData data = new EmpresaUsuarioData())
                    {
                        data.SalvaEmpresaUsuario(itemGravar);
                        IncluiSucessoBusiness("EmpresaUsuario_NovaSenhaGeradaOK");

                        GeraEmailEsqueciSenha(itemGravar, novaSenha);
                    }
                }
            }
        }

        public void ValidaRegrasSalvar(EmpresaUsuario itemGravar, int? empresaId)
        {
            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Nome))
                IncluiErroBusiness("EmpresaUsuario_Nome");

            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Email))
                IncluiErroBusiness("EmpresaUsuario_Email");

            if (IsValid())
            {
                EmpresaUsuario itemBase = RetornaEmpresaUsuario_Email(itemGravar.Email);
                if (itemBase != null && itemGravar.Id != itemBase.Id)
                    IncluiErroBusiness("EmpresaUsuario_CadastroDuplicado");
            }
            if (IsValid())
            {
                if (itemGravar.Id.HasValue)
                {
                    EmpresaUsuario itemBase = RetornaEmpresaUsuario_Id((int)itemGravar.Id, empresaId);
                    ValidaExistencia(itemBase);
                    if (IsValid())
                    {
                        itemGravar.DataInclusao = itemBase.DataInclusao;
                        itemGravar.DataAlteracao = DateTime.Now;

                        if (string.IsNullOrWhiteSpace(itemGravar.Senha) && string.IsNullOrWhiteSpace(itemGravar.SenhaConfirmacao))
                            itemGravar.Senha = itemBase.Senha;
                        else
                        {
                            if (string.IsNullOrWhiteSpace(itemGravar.Senha))
                                IncluiErroBusiness("EmpresaUsuario_Senha");

                            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.SenhaConfirmacao))
                                IncluiErroBusiness("EmpresaUsuario_SenhaConfirmacao");

                            if (IsValid() && !itemGravar.Senha.Equals(itemGravar.SenhaConfirmacao))
                                IncluiErroBusiness("EmpresaUsuario_SenhaConfirmacao_Incorreta");

                            if (IsValid())
                                itemGravar.Senha = PasswordHash.HashPassword(itemGravar.Senha);
                        }
                    }
                }
                else
                {
                    itemGravar.DataInclusao = DateTime.Now;
                    itemGravar.Ativo = true;

                    if (string.IsNullOrWhiteSpace(itemGravar.Senha))
                        IncluiErroBusiness("EmpresaUsuario_Senha");

                    if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.SenhaConfirmacao))
                        IncluiErroBusiness("EmpresaUsuario_SenhaConfirmacao");

                    if (IsValid() && !itemGravar.Senha.Equals(itemGravar.SenhaConfirmacao))
                        IncluiErroBusiness("EmpresaUsuario_SenhaConfirmacao_Incorreta");

                    if (IsValid())
                        itemGravar.Senha = PasswordHash.HashPassword(itemGravar.Senha);
                }
            }


        }
        public void ValidaRegrasExcluir(EmpresaUsuario itemGravar)
        {
            if (IsValid())
                ValidaExistencia(itemGravar);
        }
        public void ValidaRegrasAlterarSenha(ref EmpresaUsuario itemGravar)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(itemGravar.Email))
                IncluiErroBusiness("EmpresaUsuario_Email");

            if (string.IsNullOrEmpty(itemGravar.Senha))
                IncluiErroBusiness("EmpresaUsuario_Senha");

            if (string.IsNullOrEmpty(itemGravar.NovaSenha))
                IncluiErroBusiness("EmpresaUsuario_NovaSenha");

            if (string.IsNullOrEmpty(itemGravar.SenhaConfirmacao))
                IncluiErroBusiness("EmpresaUsuario_NovaSenhaConfirmacao");

            if (IsValid())
            {
                EmpresaUsuario itemBase = RetornaEmpresaUsuario_Email(itemGravar.Email);

                if (itemBase == null)
                    IncluiErroBusiness("EmpresaUsuario_EmailInvalido");

                if (IsValid() && !PasswordHash.ValidatePassword(itemGravar.Senha, itemBase.Senha))
                    IncluiErroBusiness("EmpresaUsuario_SenhaInvalida");

                if (IsValid())
                {
                    itemBase.Senha = itemGravar.NovaSenha;
                    itemBase.SenhaConfirmacao = itemGravar.SenhaConfirmacao;
                    itemGravar = itemBase;
                }
            }
        }
        public void ValidaRegrasGerarNovaSenha(ref EmpresaUsuario itemGravar)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(itemGravar.Email))
                IncluiErroBusiness("EmpresaUsuario_Email");

            if (IsValid())
            {
                EmpresaUsuario itemBase = RetornaEmpresaUsuario_Email(itemGravar.Email);

                if (itemBase == null)
                    IncluiErroBusiness("EmpresaUsuario_EmailInvalido");

                if (IsValid())
                    itemGravar = itemBase;
            }
        }
        public void ValidaExistencia(EmpresaUsuario itemGravar)
        {
            if (itemGravar == null)
                IncluiErroBusiness("EmpresaUsuario_NaoEncontrado");
        }

        public string GeraToken(string email, string funcionalidades)
        {
            try
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddMinutes(60), false, funcionalidades);

                string ticketCriptografado = FormsAuthentication.Encrypt(authTicket);
                return ticketCriptografado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void GeraEmailEsqueciSenha(EmpresaUsuario itemGravar, string novaSenha)
        {
            EmailBusiness biz = new EmailBusiness();
            biz.GeraEmailEsqueciSenha(itemGravar.Nome, itemGravar.Email, novaSenha);
        }

    }
}
