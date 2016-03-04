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
    /// EsqueciSenha
    /// </summary>
    public class EsqueciSenhaController : ApiBase
    {
        EmpresaBusiness biz = new EmpresaBusiness();

        /// <summary>
        /// Gera nova senha de uma determinada empresa
        /// </summary>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public int? Post([FromBody]Empresa itemSalvar)
        {
            try
            {
                //API
                biz.GeraNovaSenha(itemSalvar);
                if (!biz.IsValid())
                    throw new InvalidDataException();

                if (itemSalvar != null)
                {
                    itemSalvar.SenhaUsuarioAdm = null;
                    itemSalvar.SenhaUsuarioAdmConfirmacao = null;
                }
            }
            catch (InvalidDataException)
            {
                GeraErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                GeraErro(HttpStatusCode.Unauthorized, biz.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return itemSalvar.Id;
        }
    }
}