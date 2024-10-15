using AutoMapper;
using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using static QRCoder.PayloadGenerator;

namespace Domain.Dashboard
{
    public class DashboardService : IDashboardService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DashboardService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DeviceStockByStatusDto>>> GetDsDeviceStockStatus()
        {
            try
            {
                var repoResult = await _uow.VwDsDeviceStockByStatus.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<DeviceStockByStatusDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<DeviceAllocatedDto>>> GetDsDeviceAllocated()
        {
            try
            {
                var repoResult = await _uow.VwDsDeviceAllocated.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<DeviceAllocatedDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<DeviceStockByCategoryDto>>> GetDsDeviceStockCategory()
        {
            try
            {
                var repoResult = await _uow.VwDsDeviceStockByCategory.Set().ToListAsync();

                var resultGroups = repoResult.OrderByDescending(m => m.DeviceCount).GroupBy(m => new { m.Order, m.DeviceCategory });

                var result = new List<DeviceStockByCategoryDto>();
                foreach (var resultGroup in resultGroups.OrderBy(m => m.Key.Order))
                {
                    var dataToAdd = new DeviceStockByCategoryDto()
                    {
                        Order = resultGroup.Key.Order,
                        DeviceCategory = resultGroup.Key.DeviceCategory,
                        Device = resultGroup.Select(m => new DeviceGeneralDto()
                        {
                            Parameter = m.DeviceStatus,
                            ParameterUI = AssetStatus.GetAssetStatusUI(m.DeviceStatus),
                            Count = m.DeviceCount
                        })
                    };

                    result.Add(dataToAdd);
                }

                return Result.Ok(result.AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<DeviceStockByBrandDto>>> GetDsDeviceStockBrand()
        {
            try
            {
                var repoResult = await _uow.VwDsDeviceStockByBrand.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<DeviceStockByBrandDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<DeviceYoYDto>>> GetDsDeviceYoY()
        {
            try
            {
                var repoResult = await _uow.VwDsDeviceYoY.Set().ToListAsync();

                var resultGroups = repoResult.OrderByDescending(m => m.Year).GroupBy(m => m.Year);

                var result = new List<DeviceYoYDto>();
                foreach (var resultGroup in resultGroups)
                {
                    var dataToAdd = new DeviceYoYDto()
                    {
                        Year = resultGroup.Key,
                        Device = resultGroup.Select(m => new DeviceGeneralDto()
                        {
                            Parameter = m.DeviceType,
                            Count = m.DeviceCount,
                            ChartColor = m.ChartColor
                        })
                    };

                    result.Add(dataToAdd);
                }

                var lastYear = result.FirstOrDefault().Year;

                for (int i = 0; i < 5; i++)
                {
                    var yearCheck = lastYear - i;
                    var resultYear = result.FirstOrDefault(m => m.Year == yearCheck);
                    if (resultYear == null)
                    {
                        var dataToAdd = new DeviceYoYDto()
                        {
                            Year = yearCheck,
                            Device = new List<DeviceGeneralDto>()
                        };

                        result.Add(dataToAdd);
                    }
                }

                return Result.Ok(result.OrderBy(m => m.Year).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<DeviceEndPeriodDto>>> GetDsDeviceEndPeriod()
        {
            try
            {
                var repoResult = await _uow.VwDsDeviceEndPeriod.Set().ToListAsync();

                var resultGroups = repoResult.OrderByDescending(m => m.DeviceCount).GroupBy(m => m.BranchName);

                var result = new List<DeviceEndPeriodDto>();
                foreach (var resultGroup in resultGroups)
                {
                    var dataToAdd = new DeviceEndPeriodDto()
                    {
                        BranchName = resultGroup.Key,
                        Device = resultGroup.Select(m => new DeviceGeneralDto()
                        {
                            Parameter = m.DeviceType,
                            Count = m.DeviceCount,
                            ChartColor = m.ChartColor
                        })
                    };

                    result.Add(dataToAdd);
                }

                return Result.Ok(result.AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<DeviceEndPeriodYearDto>>> GetDsDeviceEndPeriodYear()
        {
            try
            {
                var repoResult = await _uow.VwDsDeviceEndPeriodYear.Set().ToListAsync();

                var resultGroups = repoResult.OrderByDescending(m => m.DeviceCount).GroupBy(m => m.Month);

                var result = new List<DeviceEndPeriodYearDto>();
                foreach (var resultGroup in resultGroups)
                {
                    var dataToAdd = new DeviceEndPeriodYearDto()
                    {
                        Month = resultGroup.Key,
                        Device = resultGroup.Select(m => new DeviceGeneralDto()
                        {
                            Parameter = m.DeviceType,
                            Count = m.DeviceCount,
                            ChartColor = m.ChartColor
                        })
                    };

                    result.Add(dataToAdd);
                }

                if (result.Any())
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var resultMonth = result.FirstOrDefault(m => m.Month == i);
                        if (resultMonth == null)
                        {
                            var dataToAdd = new DeviceEndPeriodYearDto()
                            {
                                Month = i,
                                Device = new List<DeviceGeneralDto>()
                            };

                            result.Add(dataToAdd);
                        }
                    }
                }

                return Result.Ok(result.OrderBy(m => m.Month).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<DescriptionGroupDto>>> GetDescriptionGroupsAsync(string categoryDesc = null)
        {
            var categoryResult = await (from category in _uow.MstCategory.Set()
                                        .Where(a => a.Tag == "TN" && a.IsActive && a.Description.ToLower() == categoryDesc.ToLower())
                                        .Include(c => c.CategoryDetails)
                                        .ThenInclude(cd => cd.Brand)
                                        from categoryDetail in category.CategoryDetails
                                        //join model in _uow.MstModel.Set()
                                        //    on categoryDetail.BrandCode equals model.BrandCode
                                        where !category.IsDelete 
                                        select new
                                        {
                                            category.Code,
                                            category.Description,
                                            Brand = _mapper.Map<BrandWithFeaturesDto>(categoryDetail.Brand)
                                        })
                .Distinct()
                .ToListAsync();

            var groupedDescriptions = categoryResult
                .GroupBy(r => new { r.Code, r.Description })
                .Select(g => new DescriptionGroupDto
                {
                    Code = g.Key.Code,
                    Description = g.Key.Description,
                    Brands = g.Select(r => r.Brand)
                              .Distinct()
                              .ToList()
                })
                .ToList() as IEnumerable<DescriptionGroupDto>;

            return Result.Ok(groupedDescriptions);
        }

        public async Task<Result<BrandWithFeaturesDto>> GetBrandFeaturesAsync(string brandName)
        {
            // Fetch brand features from database or static data
            var brand = await _uow.MstBrand.Set()
                .Where(b => b.Name == brandName)
                .Select(b => new BrandWithFeaturesDto
                {
                    Code = b.Code,
                    Name = b.Name,
                    BrandImage = b.BrandImage,
                    Country = b.Country,
                    Features = new List<FeatureDto>
                    {
                        new FeatureDto { Name = "Productivity Calculator", IconClass = "fa-calculator", Url = "calculator-url", IsAvailable = true },
                        new FeatureDto { Name = "Product Specification", IconClass = "fa-list", Url = "specification-url", IsAvailable = true },
                        new FeatureDto { Name = "Product Comparison", IconClass = "fa-repeat", Url = "comparison-url", IsAvailable = true },
                        new FeatureDto { Name = "Application Handbook", IconClass = "fa-book-open-reader", Url = "handbook-url", IsAvailable = false }
                    }
                })
                .FirstOrDefaultAsync();

            return brand != null
                ? Result.Ok(brand)
                : Result.Fail<BrandWithFeaturesDto>("Brand not found.");
        }
    }
}
