using System;
using System.Collections.Generic;
using System.Web.Http;
using System.IO;
using Giusti.Chat.Web.Library;
using System.Net.Http;
using System.Net;
using Giusti.Chat.Model;
using Giusti.Chat.Model.Dominio;
using System.Web;
using Giusti.Chat.Business;

namespace Giusti.Chat.Web.Controllers.Api
{
    /// <summary>
    /// Atendimento
    /// </summary>
    public class UsuarioAtendimentoController : ApiBase
    {
        UsuarioAtendimentoBusiness biz = new UsuarioAtendimentoBusiness();

        /// <summary>
        /// Altera um determinado usuário de atendimento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public Usuario Put(int id, [FromBody]Usuario itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeAtendimento, Constantes.FuncionalidadeNomeAtendimento, biz);

                //API
                itemSalvar.Id = id;
                biz.SalvaUsuario(itemSalvar);

                if (!biz.IsValid())
                    throw new InvalidDataException();

                if (itemSalvar != null)
                {
                    itemSalvar.Senha = null;
                    itemSalvar.NovaSenha = null;
                    itemSalvar.SenhaConfirmacao = null;
                }

                GravaLog(Constantes.FuncionalidadeNomeUsuarioEdicao, RetornaEmailAutenticado(), itemSalvar.Id);
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

            return itemSalvar;
        }
    }
}