using System.Web;
using System.Web.Mvc;

namespace Giusti.Chat.Web.Adm
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
