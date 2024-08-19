using FluentResults;

namespace Domain.Dashboard
{
    public interface IDashboardService
    {
        Task<Result<IEnumerable<DeviceStockByStatusDto>>> GetDsDeviceStockStatus();
        Task<Result<IEnumerable<DeviceStockByBrandDto>>> GetDsDeviceStockBrand();
        Task<Result<IEnumerable<DeviceStockByCategoryDto>>> GetDsDeviceStockCategory();
        Task<Result<IEnumerable<DeviceAllocatedDto>>> GetDsDeviceAllocated();
        Task<Result<IEnumerable<DeviceEndPeriodDto>>> GetDsDeviceEndPeriod();
        Task<Result<IEnumerable<DeviceEndPeriodYearDto>>> GetDsDeviceEndPeriodYear();
        Task<Result<IEnumerable<DeviceYoYDto>>> GetDsDeviceYoY();
        Task<Result<IEnumerable<DescriptionGroupDto>>> GetDescriptionGroupsAsync();
        Task<Result<BrandWithFeaturesDto>> GetBrandFeaturesAsync(string brandName);
    }
}
