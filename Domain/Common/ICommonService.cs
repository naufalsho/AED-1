using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Common
{
    public interface ICommonService
    {
        Task<IEnumerable<SelectListItem>> SLGetCategory();
        Task<IEnumerable<SelectListItem>> SLGetClassValue();
        Task<IEnumerable<SelectListItem>> SLGetClass();
        Task<IEnumerable<SelectListItem>> SLGetClassByBrand(string brand, string distributor);
        Task<IEnumerable<SelectListItem>> SLGetBrand();
        Task<IEnumerable<SelectListItem>> SLGetGroupMenu();
        Task<IEnumerable<SelectListItem>> SLGetRole();

		Task<IEnumerable<SelectListItem>> SLGetModel(string type = null);
		Task<IEnumerable<SelectListItem>> SLGetModelProductTN(string type = null);


		IEnumerable<SelectListItem> SLGetEmployeeStatus();
    }
}
