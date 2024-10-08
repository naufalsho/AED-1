using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.AccessMenu;
using Domain.Common;
using Domain.Comparison;
using Domain.Master.MasterModel;
using Domain.MasterComparisonType;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ZXing.QrCode.Internal;

namespace WebApp.Controllers.Transaction
{
    [Route("ImplementCompability")]
    public class ImplementCompabilityController : BasePageController
    {
        private readonly ICommonService _commonSvc;
        private readonly IImplementService _implementServices;

        public ImplementCompabilityController(ICommonService commonSvc, IImplementService implementServices)
        {
            _commonSvc = commonSvc;
            _implementServices = implementServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ret = new ImplementDto();
            ret.SLModelAttachment = await _commonSvc.SLGetModel(MasterModelType.Attachment);
            ret.SLModelUnit = await _commonSvc.SLGetModelProductTN(MasterModelType.Unit);
            return View(ViewPath.ImplementCompability, ret);
        }

        [HttpGet("GetImplement")]
        public async Task<IActionResult> GetList(ImplementDto param)
        {
            ViewBag.Type = "Implement";


            var ret = await _implementServices.GetImplement(param);

            if (ret.IsFailed)
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }






                return View(ViewPath.ImplementCompabilityDetail, ret.Value);

            // var ret = awaiValue.ImplementsService.GetList();

            // if (ret.IsSuccess)
            // {
            //     return Ok(ret.Value);
            // }
            // else
            // {
            //     var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

            //     return StatusCode(int.Parse(resp.StatusCode), resp.Message);

        }
         
        [HttpGet("GetProductModel")]
        public async Task<IActionResult> GetProductModel(ImplementDto param)
        {
            ViewBag.Type = "ProductModel";

            var ret = await _implementServices.GetProductModel(param);

            return View(ViewPath.ImplementCompabilityDetail, ret.Value);

            // var ret = await _mstComparisonService.GetList();

            // if (ret.IsSuccess)
            // {
            //     return Ok(ret.Value);
            // }
            // else
            // {
            //     var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

            //     return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            // }
        }
        

    }
}
