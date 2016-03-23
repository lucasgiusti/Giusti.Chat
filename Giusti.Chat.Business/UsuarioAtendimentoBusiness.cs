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
    public class UsuarioAtendimentoBusiness : BusinessBase
    {
        public void SalvaUsuario(Usuario itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar);
            if (IsValid())
            {
                using (AtendimentoData data = new AtendimentoData())
                {
                    data.SalvaUsuario(itemGravar);
                    IncluiSucessoBusiness("Usuario_SalvaUsuarioOK");
                }
            }
        }

        public void ValidaRegrasSalvar(Usuario itemGravar)
        {
            if (IsValid())
            {
                if (itemGravar.Id.HasValue)
                {
                    UsuarioBusiness biz = new UsuarioBusiness();
                    Usuario itemBase = biz.RetornaUsuario_Id((int)itemGravar.Id);
                    biz.ValidaExistencia(itemBase);
                    if (IsValid())
                    {
                        if (itemGravar.Disponivel.Value)
                        {
                            if (itemGravar.UsuarioAtendimentos == null || (itemGravar.UsuarioAtendimentos != null && itemGravar.UsuarioAtendimentos.Count == 0))
                            {

                                itemGravar.UsuarioAtendimentos = new List<UsuarioAtendimento>();

                                itemGravar.UsuarioAtendimentos.Add(new UsuarioAtendimento() { Situacao = (int)EnumSituacaoAtendimento.Livre, DataInclusao = DateTime.Now });
                                itemGravar.UsuarioAtendimentos.Add(new UsuarioAtendimento() { Situacao = (int)EnumSituacaoAtendimento.Livre, DataInclusao = DateTime.Now });
                                itemGravar.UsuarioAtendimentos.Add(new UsuarioAtendimento() { Situacao = (int)EnumSituacaoAtendimento.Livre, DataInclusao = DateTime.Now });
                                itemGravar.UsuarioAtendimentos.Add(new UsuarioAtendimento() { Situacao = (int)EnumSituacaoAtendimento.Livre, DataInclusao = DateTime.Now });
                            }
                            else
                                itemGravar.UsuarioAtendimentos.ToList().ForEach(a => { a.Situacao = (int)EnumSituacaoAtendimento.Livre; a.DataAlteracao = DateTime.Now; });
                        }
                        else
                            itemGravar.UsuarioAtendimentos.ToList().ForEach(a => { a.Situacao = (int)EnumSituacaoAtendimento.Indisponivel; a.DataAlteracao = DateTime.Now; });

                        itemGravar.Senha = itemBase.Senha;
                    }
                }
            }
        }
    }
}
