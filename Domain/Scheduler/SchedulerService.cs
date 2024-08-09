using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Domain.ExtServices;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Domain.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IIoTService _iotSvc;
        private readonly IUnitOfWork _uow;

        public SchedulerService(
            IIoTService iotSvc,
            IUnitOfWork uow)
        {
            _iotSvc = iotSvc;
            _uow = uow;
        }

        public Task<Result> AssetEndPeriod()
        {
            throw new NotImplementedException();
        }

        // public async Task<Result> AssetEndPeriod()
        // {
        //     try
        //     {
        //         var assetList = await _uow.MstAsset.Set().Where(m => !m.IsEndPeriod && m.PeriodTo.Value.Date < DateTime.Now.Date).ToListAsync();

        //         foreach (var asset in assetList)
        //         {
        //             asset.IsEndPeriod = true;

        //             var checkTrans = await _uow.TransDeployDevice.Set().FirstOrDefaultAsync(m => m.AssetId == asset.Id && m.UndeployDate == null);
        //             if (checkTrans == null)
        //             {
        //                 try
        //                 {
        //                     if (!string.IsNullOrEmpty(asset.IoTDeviceToken))
        //                     {
        //                         var iotRequest = new IoTDeleteRequest()
        //                         {
        //                             device_token = asset.IoTDeviceToken
        //                         };

        //                         var resp = await _iotSvc.DeleteDevice(iotRequest);

        //                         if (resp.status.ToLower() == "success")
        //                         {
        //                             asset.IoTDeviceToken = null;
        //                         }
        //                     }
        //                 }
        //                 catch { }
        //             }
        //         }

        //         _uow.MstAsset.UpdateRange(assetList);
        //         await _uow.CompleteAsync();

        //         return Result.Ok();
        //     }
        //     catch (Exception ex)
        //     {
        //         return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //     }
        // }
    }
}
