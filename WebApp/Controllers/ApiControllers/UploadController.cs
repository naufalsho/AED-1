// using Core.Helpers;
// using Domain.Integration;
// using Domain.Upload;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;

// namespace WebApp.Controllers.ApiControllers
// {
//     [ApiExplorerSettings(IgnoreApi = true)]
//     [ApiController]
//     [Route("api")]
//     public class UploadController : ControllerBase
//     {
//         private readonly IUploadService _uplSvc;

//         public UploadController(
//             IUploadService uplSvc)
//         {
//             _uplSvc = uplSvc;
//         }

//         [HttpPost("upload/masterdata")]
//         [SwaggerResponse(200, "Success")]
//         [SwaggerResponse(500, "Internal Server Error")]
//         public async Task<IActionResult> MasterData(IFormFile file)
//         {
//             var ret1 = await _uplSvc.UploadMasterDataWithoutParent(file);
//             var ret2 = await _uplSvc.UploadMasterDataWithParent(file);
//             var ret3 = await _uplSvc.UploadMasterDataJobTItle(file);

//             if (ret1.IsSuccess && ret2.IsSuccess && ret3.IsSuccess)
//             {
//                 return Ok();
//             }
//             else
//             {
//                 var resp = ResponseHelper.CreateFailResult(ret1.Reasons.First().Message + " - " + ret2.Reasons.First().Message);

//                 var retVal = new IntegrationResponseDto()
//                 {
//                     Status = "failed",
//                     Message = resp.Message
//                 };

//                 return StatusCode(int.Parse(resp.StatusCode), retVal);
//             }
//         }

//         [HttpPost("upload/employee")]
//         [SwaggerResponse(200, "Success")]
//         [SwaggerResponse(500, "Internal Server Error")]
//         public async Task<IActionResult> Employee(IFormFile file)
//         {
//             var ret1 = await _uplSvc.UploadEmployee(file);

//             if (ret1.IsSuccess)
//             {
//                 return Ok();
//             }
//             else
//             {
//                 var resp = ResponseHelper.CreateFailResult(ret1.Reasons.First().Message);

//                 var retVal = new IntegrationResponseDto()
//                 {
//                     Status = "failed",
//                     Message = resp.Message
//                 };

//                 return StatusCode(int.Parse(resp.StatusCode), retVal);
//             }
//         }

//         [HttpPost("upload/asset")]
//         [SwaggerResponse(200, "Success")]
//         [SwaggerResponse(500, "Internal Server Error")]
//         public async Task<IActionResult> Asset(IFormFile file)
//         {
//             var ret1 = await _uplSvc.UploadAsset(file);

//             if (ret1.IsSuccess)
//             {
//                 return Ok();
//             }
//             else
//             {
//                 var resp = ResponseHelper.CreateFailResult(ret1.Reasons.First().Message);

//                 var retVal = new IntegrationResponseDto()
//                 {
//                     Status = "failed",
//                     Message = resp.Message
//                 };

//                 return StatusCode(int.Parse(resp.StatusCode), retVal);
//             }
//         }

//         [HttpPost("upload/deploy")]
//         [SwaggerResponse(200, "Success")]
//         [SwaggerResponse(500, "Internal Server Error")]
//         public async Task<IActionResult> Deploy(IFormFile file)
//         {
//             var ret1 = await _uplSvc.UploadDeploy(file);

//             if (ret1.IsSuccess)
//             {
//                 return Ok();
//             }
//             else
//             {
//                 var resp = ResponseHelper.CreateFailResult(ret1.Reasons.First().Message);

//                 var retVal = new IntegrationResponseDto()
//                 {
//                     Status = "failed",
//                     Message = resp.Message
//                 };

//                 return StatusCode(int.Parse(resp.StatusCode), retVal);
//             }
//         }
//     }
// }
