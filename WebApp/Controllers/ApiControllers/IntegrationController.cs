// using Core.Helpers;
// using Domain.Integration;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;

// namespace WebApp.Controllers.ApiControllers
// {
//     [ApiController]
//     [Route("api")]
//     public class IntegrationController : ControllerBase
//     {
//         private readonly IIntegrationService _integrationSvc;

//         public IntegrationController(
//             IIntegrationService integrationSvc)
//         {
//             _integrationSvc = integrationSvc;
//         }

//         [HttpPost("organization/{type}")]
//         [SwaggerOperation(Summary = "API endpoint for Organization data")]
//         [SwaggerResponse(200, "Success")]
//         [SwaggerResponse(500, "Internal Server Error")]
//         public async Task<IActionResult> Organization(OrganizationType type, OrganizationDto data)
//         {
//             var ret = await _integrationSvc.Organizations(type.ToString().ToUpper(), data);

//             if (ret.IsSuccess)
//             {
//                 var retVal = new IntegrationResponseDto()
//                 {
//                     Status = "success",
//                     Message = ""
//                 };

//                 return Ok(retVal);
//             }
//             else
//             {
//                 var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

//                 var retVal = new IntegrationResponseDto()
//                 {
//                     Status = "failed",
//                     Message = resp.Message
//                 };

//                 return StatusCode(int.Parse(resp.StatusCode), retVal);
//             }
//         }

//         [HttpPost("employee")]
//         [SwaggerOperation(Summary = "API endpoint for Employee data")]
//         [SwaggerResponse(200, "Success")]
//         [SwaggerResponse(500, "Internal Server Error")]
//         public async Task<IActionResult> Employee(EmployeeDto data)
//         {
//             var ret = await _integrationSvc.Employees(data);

//             if (ret.IsSuccess)
//             {
//                 var retVal = new IntegrationResponseDto()
//                 {
//                     Status = "success",
//                     Message = ""
//                 };

//                 return Ok(retVal);
//             }
//             else
//             {
//                 var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

//                 var retVal = new IntegrationResponseDto()
//                 {
//                     Status = "failed",
//                     Message = resp.Message
//                 };

//                 return StatusCode(int.Parse(resp.StatusCode), retVal);
//             }
//         }

//         [HttpPost("employeeout")]
//         [SwaggerOperation(Summary = "API endpoint for Employee Out Date data")]
//         [SwaggerResponse(200, "Success")]
//         [SwaggerResponse(400, "Bad Request")]
//         [SwaggerResponse(500, "Internal Server Error")]
//         public async Task<IActionResult> Resigns(ResignationDto data)
//         {
//             var ret = await _integrationSvc.Resigns(data);

//             if (ret.IsSuccess)
//             {
//                 var retVal = new IntegrationResponseDto()
//                 {
//                     Status = "success",
//                     Message = ""
//                 };

//                 return Ok(retVal);
//             }
//             else
//             {
//                 var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

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
