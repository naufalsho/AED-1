using System.Linq.Expressions;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    [TypeFilter(typeof(AuthFilter))]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BasePageController : Controller
    {
        public string GetModelStateErrors<T>(T data)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    if (errors.Any())
                    {
                        return errors.First().ErrorMessage;
                    }
                }
            }

            return string.Empty; // Mengembalikan string kosong jika tidak ada kesalahan
        }

    }
}
