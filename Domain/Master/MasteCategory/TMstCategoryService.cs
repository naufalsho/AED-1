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
                    .Where(a => !a.IsDelete && a.Tag != "TN").ToListAsync();


                var result = _mapper.Map<IEnumerable<TMstCategoryDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<TMstCategoryDto>>> GetAll(string type)
        {
            try
            {
                var repoResult = await _uow.MstCategory.Set().
                    Include(c => c.CategoryDetails)
                    .ThenInclude(cd => cd.Brand)
                    .Where(a => !a.IsDelete && a.Tag == "TN").ToListAsync();


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


                var categoryDetails = data.BrandCode.Select(brandCode => new TMstCategoryDetail
                {
                    CategoryCode = data.Code,
                    BrandCode = brandCode
                }).ToList();

                repoResult.CategoryDetails = categoryDetails;

                _mapper.Map(data, repoResult);
                repoResult.Tag = data.Tag;
                repoResult.Description = data.Description;
                repoResult.CreatedBy = createdBy;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;
                

                _uow.MstCategory.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstCategoryDto>(repoResult);

                return Result.Ok(result);
            }catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //public async Task<Result<IEnumerable<DescriptionGroupDto>>> GetDescriptionGroups()
        //{
        //    // Fetch all categories using the existing GetAll() method
        //    var categoryResult = await GetAll();

        //    if (!categoryResult.IsSuccess)
        //    {
        //        return Result.Fail<IEnumerable<DescriptionGroupDto>>(categoryResult.Reasons.First().Message);
        //    }

        //    // Map TMstCategoryDto to DescriptionGroupDto

        //    var groupedDescriptions = categoryResult.Value
        //        .GroupBy(c => new { c.Description, c.DescriptionImage })
        //        .Select(g => new DescriptionGroupDto
        //        {
        //            Description = g.Key.Description,
        //            DescriptionImage = g.Key.DescriptionImage,
        //            Brand = g.SelectMany(c => c.CategoryDetails)
        //                      .Select(cd => cd.Brand)
        //                      .ToList()
        //        })
        //        .ToList() as IEnumerable<DescriptionGroupDto>;

        //    return Result.Ok(groupedDescriptions);
        //}
    }

}
