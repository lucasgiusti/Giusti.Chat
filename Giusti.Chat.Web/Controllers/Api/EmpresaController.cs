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
    /// Empresa
    /// </summary>
    public class EmpresaController : ApiBase
    {
        EmpresaBusiness biz = new EmpresaBusiness();

        /// <summary>
        /// Retorna todas as empresas
        /// </summary>
        /// <returns></returns>
        public List<Empresa> Get()
        {
            List<Empresa> ResultadoBusca = new List<Empresa>();
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeEmpresaConsulta, Constantes.FuncionalidadeNomeEmpresaConsulta, biz);
                bool acessoEmpresaConsultaTodas = VerificaAutenticacaoUnica(Constantes.FuncionalidadeEmpresaConsultaTodas, Constantes.FuncionalidadeNomeEmpresaConsultaTodas, biz);

                //API
                if(acessoEmpresaConsultaTodas)
                    ResultadoBusca = new List<Empresa>(biz.RetornaEmpresas());
                else
                    ResultadoBusca = new List<Empresa>(biz.RetornaEmpresas(RetornaEmpresaIdAutenticado()));

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
        /// Retorna a empresa com id solicidado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Empresa Get(int id)
        {
            Empresa ResultadoBusca = new Empresa();
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeEmpresaConsulta, Constantes.FuncionalidadeNomeEmpresaConsulta, biz);
                bool acessoEmpresaConsultaTodas = VerificaAutenticacaoUnica(Constantes.FuncionalidadeEmpresaConsultaTodas, Constantes.FuncionalidadeNomeEmpresaConsultaTodas, biz);

                //API
                if(acessoEmpresaConsultaTodas)
                    ResultadoBusca = biz.RetornaEmpresa_Id(id);
                else
                    ResultadoBusca = biz.RetornaEmpresa_Id(id, RetornaEmpresaIdAutenticado());

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
        /// Inclui uma empresa
        /// </summary>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public int? Post([FromBody]Empresa itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeEmpresaInclusao, Constantes.FuncionalidadeNomeEmpresaInclusao, biz);

                //API
                biz.SalvaEmpresa(itemSalvar);
                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(Constantes.FuncionalidadeNomeEmpresaInclusao, RetornaEmailAutenticado(), itemSalvar.Id);
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
        /// Altera uma determinada empresa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public Empresa Put(int id, [FromBody]Empresa itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeEmpresaEdicao, Constantes.FuncionalidadeNomeEmpresaEdicao, biz);

                //API
                itemSalvar.Id = id;
                biz.SalvaEmpresa(itemSalvar);

                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(Constantes.FuncionalidadeNomeEmpresaEdicao, RetornaEmailAutenticado(), itemSalvar.Id);
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
        /// Exclui uma determinada empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage retorno = null;
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeEmpresaExclusao, Constantes.FuncionalidadeNomeEmpresaExclusao, biz);

                //API
                Empresa itemExcluir = biz.RetornaEmpresa_Id(id);

                biz.ExcluiEmpresa(itemExcluir);
                if (!biz.IsValid())
                    throw new InvalidDataException();

                retorno = RetornaMensagemOk(biz.serviceResult);

                GravaLog(Constantes.FuncionalidadeNomeEmpresaExclusao, RetornaEmailAutenticado(), itemExcluir.Id);
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