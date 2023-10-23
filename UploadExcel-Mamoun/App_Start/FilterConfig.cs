using System.Web;
using System.Web.Mvc;

namespace UploadExcel_Mamoun
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
