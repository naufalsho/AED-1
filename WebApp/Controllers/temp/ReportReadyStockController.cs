using Core.Helpers;
using Domain.Report;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("report/readystock")]
    public class ReportReadyStockController : BasePageController
    {
        private readonly IReportService _rptSvc;

        public ReportReadyStockController(
            IReportService rptSvc)
        {
            _rptSvc = rptSvc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var ret = await _rptSvc.RptDeviceAvailable();

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
