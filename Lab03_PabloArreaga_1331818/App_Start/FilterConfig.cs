using System.Web;
using System.Web.Mvc;

namespace Lab03_PabloArreaga_1331818
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
