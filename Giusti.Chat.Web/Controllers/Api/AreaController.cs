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
    /// Area
    /// </summary>
    public class AreaController : ApiBase
    {
        AreaBusiness biz = new AreaBusiness();

        /// <summary>
        /// Retorna todas as áreas
        /// </summary>
        /// <returns></returns>
        public List<Area> Get()
        {
            List<Area> ResultadoBusca = new List<Area>();
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeAreaConsulta, Constantes.FuncionalidadeNomeAreaConsulta, biz);

                //API
                ResultadoBusca = new List<Area>(biz.RetornaAreas(RetornaEmpresaIdAutenticado()));

                if (!biz.IsValid())
                    throw new InvalidDataException();
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

            return ResultadoBusca;
        }

        /// <summary>
        /// Retorna a área com id solicidado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Area Get(int id)
        {
            Area ResultadoBusca = new Area();
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeAreaConsulta, Constantes.FuncionalidadeNomeAreaConsulta, biz);

                //API
                ResultadoBusca = biz.RetornaArea_Id(id, RetornaEmpresaIdAutenticado());

                if (!biz.IsValid())
                    throw new InvalidDataException();
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

            return ResultadoBusca;
        }

        /// <summary>
        /// Inclui uma área
        /// </summary>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public int? Post([FromBody]Area itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeAreaInclusao, Constantes.FuncionalidadeNomeAreaInclusao, biz);

                //API
                biz.SalvaArea(itemSalvar, RetornaEmpresaIdAutenticado());
                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(Constantes.FuncionalidadeNomeAreaInclusao, RetornaEmailAutenticado(), itemSalvar.Id);
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

        /// <summary>
        /// Altera uma determinada área
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public Area Put(int id, [FromBody]Area itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeAreaEdicao, Constantes.FuncionalidadeNomeAreaEdicao, biz);

                //API
                itemSalvar.Id = id;
                biz.SalvaArea(itemSalvar, RetornaEmpresaIdAutenticado());

                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(Constantes.FuncionalidadeNomeAreaEdicao, RetornaEmailAutenticado(), itemSalvar.Id);
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

        /// <summary>
        /// Exclui uma determinada área
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage retorno = null;
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeAreaExclusao, Constantes.FuncionalidadeNomeAreaExclusao, biz);

                //API
                Area itemExcluir = biz.RetornaArea_Id(id, RetornaEmpresaIdAutenticado());

                biz.ExcluiArea(itemExcluir, RetornaEmpresaIdAutenticado());
                if (!biz.IsValid())
                    throw new InvalidDataException();

                retorno = RetornaMensagemOk(biz.serviceResult);

                GravaLog(Constantes.FuncionalidadeNomeAreaExclusao, RetornaEmailAutenticado(), itemExcluir.Id);
            }
            catch (InvalidDataException)
            {
                retorno = RetornaMensagemErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                retorno = RetornaMensagemErro(HttpStatusCode.Unauthorized, biz.serviceResult);
            }
            catch (Exception ex)
            {
                retorno = RetornaMensagemErro(HttpStatusCode.BadRequest, ex);
            }

            return retorno;
        }


    }
}