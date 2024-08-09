using Core.Helpers;
using Domain.Report;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("report/endperiod")]
    public class ReportEndPeriodController : BasePageController
    {
        private readonly IReportService _rptSvc;

        public ReportEndPeriodController(
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
            var ret = await _rptSvc.RptEndOfPeriod(reqData);

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
