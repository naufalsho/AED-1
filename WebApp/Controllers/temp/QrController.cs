using Domain.AccessUser;
using Domain.Common;
using Domain.Report;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("master")]
    public class QrController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IAccessUserService _auSvc;

        public QrController(
            ICommonService commonSvc,
            IAccessUserService auSvc)
        {
            _commonSvc = commonSvc;
            _auSvc = auSvc;
        }

        [HttpGet("qr")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
