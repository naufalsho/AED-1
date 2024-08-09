using Core.Helpers;
using Domain.Report;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("report/deviceuser")]
    public class ReportDeviceUserController : BasePageController
    {
        private readonly IReportService _rptSvc;

        public ReportDeviceUserController(
            IReportService rptSvc)
        {
            _rptSvc = rptSvc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetList(ReportRequestDto reqData)
        {
            var ret = await _rptSvc.RptDeviceOnUser(reqData);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }
    }
}
