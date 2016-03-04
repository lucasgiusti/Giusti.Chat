using System;
using System.Collections.Generic;
using System.Web.Http;
using System.IO;
using Giusti.Chat.Web.Adm.Library;
using System.Net.Http;
using System.Net;
using Giusti.Chat.Model;
using Giusti.Chat.Model.Dominio;
using System.Web;
using Giusti.Chat.Business;

namespace Giusti.Chat.Web.Adm.Controllers.Api
{
    /// <summary>
    /// Signin
    /// </summary>
    public class SigninController : ApiBase
    {
        EmpresaBusiness biz = new EmpresaBusiness();

        public UsuarioLogado Post([FromBody]Empresa empresa)
        {
            UsuarioLogado usuarioLogado = new UsuarioLogado();
            try
            {
                string ipMaquina = string.Empty;
                string nomeMaquina = string.Empty;
                string email = null;
                string senha = null;

                if (empresa != null)
                {
                    if (!string.IsNullOrEmpty(empresa.EmailUsuarioAdm))
                        email = empresa.EmailUsuarioAdm;
                    if (!string.IsNullOrEmpty(empresa.SenhaUsuarioAdm))
                        senha = empresa.SenhaUsuarioAdm;
                }

                nomeMaquina = Dns.GetHostName();
                //IPAddress[] ip = Dns.GetHostAddresses(nomeMaquina);
                //ipMaquina = ip[1].ToString();
                ipMaquina = "127.0.0.1";

                usuarioLogado = biz.EfetuaLoginSistema(email, senha, ipMaquina, nomeMaquina);

                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(EnumTipoAcao.Login.ToString(), email);
            }
            catch (InvalidDataException)
            {
                GeraErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                GeraErro(HttpStatusCode.Forbidden, biz.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return usuarioLogado;
        }
    }
}