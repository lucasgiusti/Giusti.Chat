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
    public class LogBusiness : BusinessBase
    {
        public IList<Log> RetornaLogs()
        {
            LimpaValidacao();
            IList<Log> RetornoAcao = new List<Log>();
            if (IsValid())
            {
                using (LogData data = new LogData())
                {
                    RetornoAcao = data.RetornaLogs();
                }
            }

            return RetornoAcao;
        }
        public IList<Log> RetornaLogs(int? empresaId)
        {
            LimpaValidacao();
            IList<Log> RetornoAcao = new List<Log>();
            if (IsValid())
            {
                using (LogData data = new LogData())
                {
                    RetornoAcao = data.RetornaLogs(empresaId);
                }
            }

            return RetornoAcao;
        }

        public void SalvaLog(Log itemGravar)
        {
            ValidateService(itemGravar);
            ValidaRegrasNegocioLog(itemGravar);
            if (IsValid())
            {
                using (LogData data = new LogData())
                {
                    data.SalvaLog(itemGravar);
                }
            }
        }

        private void ValidaRegrasNegocioLog(Log itemGravar)
        {
        }
    }
}
