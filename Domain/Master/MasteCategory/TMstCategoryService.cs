using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO.Pipelines;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;




namespace Domain.Master.MasterCategory
{
    public class TMstCategoryService : IMstCategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TMstCategoryService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<TMstCategoryDto>> Create(UserClaimModel user, TMstCategoryCreatedDto data)
        {
            try
            {
                var checkCode = await _uow.MstCategory.Set().FirstOrDefaultAsync(m => m.Code == data.Code);


                if (checkCode != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

                var param = _mapper.Map<TMstCategory>(data);
                param.CreatedBy = user.NameIdentifier;
                param.CreatedDate = DateTime.Now;

                // Cek apakah file dikirim dari form
                if (data.DescriptionImage != null && data.DescriptionImage.Length > 0)
                {
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "product");

                    if (!string.IsNullOrEmpty(param.DescriptionImage))
                    {
                        var previousFilePath = Path.Combine(folderPath, param.DescriptionImage);
                        if (File.Exists(previousFilePath))
                        {
                            File.Delete(previousFilePath);
                        }
                    }

                    var fileExtension = Path.GetExtension(data.DescriptionImage.FileName);

                    string fileName = $"{data.Code}{fileExtension}";


                    param.DescriptionImage = fileName;

                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.DescriptionImage.CopyToAsync(stream);
                    }
                }


                var categoryDetails = new List<TMstCategoryDetail>();
                if(data.BrandCode != null)
                { 
                    foreach (var brandCode in data.BrandCode)
                    {
                        var categoryDetail = new TMstCategoryDetail
                        {
                            CategoryCode = param.Code,
                            BrandCode = brandCode
                        };
                        categoryDetails.Add(categoryDetail);
                    }

                    param.CategoryDetails = categoryDetails;
                }

                await _uow.MstCategory.Add(param);


                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstCategoryDto>(param);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstCategoryDto>> Delete(UserClaimModel user, string id)
        {
            try
            {
                var repoResult = await _uow.MstCategory.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.MstCategory.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstCategoryDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<TMstCategoryDto>>> GetAll()
        {
            try
            {
                var repoResult = await _uow.MstCategory.Set().
                    Include(c => c.CategoryDetails)
                    .ThenInclude(cd => cd.Brand)
                    .Where(a => !a.IsDelete).ToListAsync();


                var result = _mapper.Map<IEnumerable<TMstCategoryDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstCategoryDto>> GetById(string id)
        {
            try
            {
                var repoResult = await _uow.MstCategory.Set()
                    .Include(c => c.CategoryDetails)
                    .ThenInclude(cd => cd.Brand).FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<TMstCategoryDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public Task<Result<TMstCategoryDto>> GetByParam(TMstCategoryDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetLastCode()
        {
            try
            {
                var lastBrandCode = await _uow.MstCategory.Set().OrderByDescending(b => b.CreatedDate).FirstOrDefaultAsync();

                if (lastBrandCode == null)
                    return Result.Ok("C00001");


                string prefix = lastBrandCode.Code.Substring(0, 1); 
                int lastNumber = int.Parse(lastBrandCode.Code.Substring(1)); 

                lastNumber++;

                string nextCode = $"{prefix}{lastNumber:D5}"; 



                return Result.Ok(nextCode);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<TMstCategoryDto>>> GetListByParam(TMstCategoryDto param)
        {
            try
            {

                IQueryable<TMstCategory> query = _uow.MstCategory.Set().Where(m => m.IsActive && !m.IsDelete);

                if (!string.IsNullOrEmpty(param.Type.ToString()))
                {
                    query = query.Where(m => m.Type == param.Type);
                }

                var repoResult = await query.ToListAsync();
                var result = _mapper.Map<IEnumerable<TMstCategoryDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<TMstCategoryDto>> Update(UserClaimModel user, TMstCategoryUpdatedDto data)
        {
            try
            {
                var repoResult = await _uow.MstCategory.Set()
                    .Include(c => c.CategoryDetails)
                    .FirstOrDefaultAsync(m => m.Code == data.Code);

                if (repoResult == null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");
                }

                string createdBy = repoResult.CreatedBy;
                string previousImageFileName = repoResult.DescriptionImage;


                var categoryDetails = data.BrandCode.Select(brandCode => new TMstCategoryDetail
                {
                    CategoryCode = data.Code,
                    BrandCode = brandCode
                }).ToList();

                repoResult.CategoryDetails = categoryDetails;

                _mapper.Map(data, repoResult);
                repoResult.Description = data.Description;
                repoResult.CreatedBy = createdBy;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;
                if (data.DescriptionImage != null && data.DescriptionImage.Length > 0)
                {
                    if (!string.IsNullOrEmpty(previousImageFileName))
                    {
                        var previousImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "product", previousImageFileName);
                        if (File.Exists(previousImagePath))
                        {
                            File.Delete(previousImagePath);
                        }
                    }


                    var fileExtension = Path.GetExtension(data.DescriptionImage.FileName);

                    string fileName = $"{data.Code}{fileExtension}";
                    repoResult.DescriptionImage = fileName;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "product", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.DescriptionImage.CopyToAsync(stream);
                    }
                }
                else
                {
                    repoResult.DescriptionImage = previousImageFileName;
                }

                _uow.MstCategory.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstCategoryDto>(repoResult);

                return Result.Ok(result);
            }catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<DescriptionGroupDto>>> GetDescriptionGroups()
        {
            try
            {
                // Fetch and filter categories based on the distributor
                var categoryResult = await (from category in _uow.MstCategory.Set()
                                                        .Include(c => c.CategoryDetails)
                                                        .ThenInclude(cd => cd.Brand)
                                            from categoryDetail in category.CategoryDetails
                                            join model in _uow.MstModel.Set()
                                                on categoryDetail.BrandCode equals model.BrandCode
                                            where !category.IsDelete && model.Distributor == "PT. Traktor Nusantara"
                                            select new
                                            {
                                                category.Description,
                                                category.DescriptionImage,
                                                Brand = categoryDetail.Brand
                                            })
                                            .Distinct()
                                            .ToListAsync();

                // Map TMstBrand to TMstBrandDto and group the results by Description and DescriptionImage
                var groupedDescriptions = categoryResult
                    .GroupBy(r => new { r.Description, r.DescriptionImage })
                    .Select(g => new DescriptionGroupDto
                    {
                        Description = g.Key.Description,
                        DescriptionImage = g.Key.DescriptionImage,
                        Brand = _mapper.Map<List<TMstBrandDto>>(g.Select(r => r.Brand).Distinct().ToList())
                    })
                    .ToList();

                // Wrap the grouped descriptions in a Result and return
                return Result.Ok(groupedDescriptions.AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail<IEnumerable<DescriptionGroupDto>>($"An error occurred: {ex.Message}");
            }
        }

    }

}
