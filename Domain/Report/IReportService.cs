using FluentResults;

namespace Domain.Report
{
    public interface IReportService
    {
        Task<Result<IEnumerable<ReportDeviceDto>>> RptDeviceAvailable();
        Task<Result<IEnumerable<ReportTransDeviceDto>>> RptDeviceOnUser(ReportRequestDto reqData);
        Task<Result<IEnumerable<ReportTransDeviceDto>>> RptEndOfPeriod(ReportRequestDto reqData);
        Task<Result<IEnumerable<ReportDeviceDto>>> RptHistorical(ReportRequestDto reqData);
        Task<Result<IEnumerable<ReportDeviceHistoryDto>>> RptHistoricalDetail(int assetId);
    }
}
