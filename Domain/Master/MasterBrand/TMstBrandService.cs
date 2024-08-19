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
using Domain.Master.MasterModel;
using Domain.MasterYardArea;
using Domain.Transaction.SpecValues;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;




namespace Domain.Master
{
    public class TMstBrandService : IMstBrandService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TMstBrandService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<TMstBrandDto>> Create(UserClaimModel user, TMstBrandCreateDto data)
        {
            try
            {
                var check = await _uow.MstBrand.Set().FirstOrDefaultAsync(m => m.Code == data.Code);
                var checkName = await _uow.MstBrand.Set().FirstOrDefaultAsync(m => m.Name == data.Name);

                if (check.Code != null) return Result.Fail(ResponseStatusCode.BadRequest + ": Code not available. Please change the code!");
                if (check.Name != null) return Result.Fail(ResponseStatusCode.BadRequest + ": Name of brand available. Please change the code!");


                var param = _mapper.Map<TMstBrand>(data);
                param.Name = data.Name;
                param.Country= data.Country;
                param.UpdatedDate = null;
                param.CreatedBy = user.NameIdentifier;
                param.CreatedDate = DateTime.Now;


                // Cek apakah file dikirim dari form
                if (data.BrandImage != null && data.BrandImage.Length > 0)
                {
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "product");

                    if (!string.IsNullOrEmpty(param.BrandImage))
                    {
                        var previousFilePath = Path.Combine(folderPath, param.BrandImage);
                        if (File.Exists(previousFilePath))
                        {
                            File.Delete(previousFilePath);
                        }
                    }

                    var fileExtension = Path.GetExtension(data.BrandImage.FileName);

                    string fileName = $"{data.Code}{fileExtension}";


                    param.BrandImage = fileName;

                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.BrandImage.CopyToAsync(stream);
                    }
                }


                await _uow.MstBrand.Add(param);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstBrandDto>(param);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<TMstBrandDto>>> GetByCategory(string categoryCode)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("CategoryCode", categoryCode),
                };
                var repoResult = await _uow.MstBrand.ExecuteStoredProcedure("sp_GetBrandByCategory", parameters);


                var result = _mapper.Map<IEnumerable<TMstBrandDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<TMstBrandDto>> Delete(UserClaimModel user, string id)
        {
            try
            {
                var repoResult = await _uow.MstBrand.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.MstBrand.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstBrandDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }


        public async Task<Result<IEnumerable<TMstBrandDto>>> GetAll()
        {
            try
            {
                var repoResult = await _uow.MstBrand.Set().Where(c => c.Flag == null).ToListAsync();

                var result = _mapper.Map<IEnumerable<TMstBrandDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<TMstBrandDto>> GetById(string id)
        {
            try
            {
                var repoResult = await _uow.MstBrand.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<TMstBrandDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }   
        }

        public Task<Result<TMstBrandDto>> GetByParam(TMstBrandDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetLastCode()
        {
            try
            {
                var lastBrandCode = await _uow.MstBrand.Set().OrderByDescending(b => b.CreatedDate).FirstOrDefaultAsync();

                if (lastBrandCode == null)
                    return "B00001";

                string prefix = lastBrandCode.Code.Substring(0, 1); 
                int lastNumber = int.Parse(lastBrandCode.Code.Substring(1));

                lastNumber++;

                string nextCode = $"{prefix}{lastNumber:D5}"; 

                Console.WriteLine(nextCode);

                
                return Result.Ok(nextCode);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }

            throw new NotImplementedException();
        }

        public Task<Result<TMstBrandDto>> GetListByParam(TMstBrandDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TMstBrandDto>> Update(UserClaimModel user, TMstBrandCreateDto data)
        {
            try
            {
                var repoResult = await _uow.MstBrand.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                string CreatedBy = repoResult.CreatedBy;
                string previousImageFileName = repoResult.BrandImage;

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _mapper.Map(data, repoResult);
                repoResult.Name= data.Name;
                repoResult.Country= data.Country;
                repoResult.CreatedBy = CreatedBy;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;
                if (data.BrandImage != null && data.BrandImage.Length > 0)
                {
                    if (!string.IsNullOrEmpty(previousImageFileName))
                    {
                        var previousImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "product", previousImageFileName);
                        if (File.Exists(previousImagePath))
                        {
                            File.Delete(previousImagePath);
                        }
                    }


                    var fileExtension = Path.GetExtension(data.BrandImage.FileName);

                    string fileName = $"{data.Code}{fileExtension}";
                    repoResult.BrandImage = fileName;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "product", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.BrandImage.CopyToAsync(stream);
                    }
                }
                else
                {
                    repoResult.BrandImage = previousImageFileName;
                }

                _uow.MstBrand.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstBrandDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<TMstBrandDto>>> GetByClass(string classCode)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("ClassCode", classCode),
                };
                var repoResult = await _uow.MstBrand.ExecuteStoredProcedure("sp_GetBrandByClass", parameters);


                var result = _mapper.Map<IEnumerable<TMstBrandDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }
    }

}
