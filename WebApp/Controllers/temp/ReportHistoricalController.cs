using Core;
using Core.Helpers;
using Domain.Report;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("report/history")]
    public class ReportHistoricalController : BasePageController
    {
        private readonly IReportService _rptSvc;

        public ReportHistoricalController(
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
            var ret = await _rptSvc.RptHistorical(reqData);

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

        [HttpGet("detail/{assetId}")]
        public async Task<IActionResult> GetDetail(int assetId)
        {
            var ret = await _rptSvc.RptHistoricalDetail(assetId);

            if (ret.IsSuccess)
            {
                var assetData = ret.Value.FirstOrDefault();

                ViewData[ViewDataType.ModalTitle] = $"Asset History - {assetData.ProductBrandName} {assetData.ProductTypeName} - {assetData.SerialNumber}";

                return View(ViewPath.HistoryDetail, ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }
    }
}
