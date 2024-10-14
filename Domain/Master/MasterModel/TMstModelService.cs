using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.EntityFrameworkCore;




namespace Domain.Master.MasterModel
{
    public class TMstModelService : IMstModelService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TMstModelService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<TMstModelDto>> Create(UserClaimModel user, TMstModelCreatedDto data)
        {
            try
            {
                var checkCode = await _uow.MstModel.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                if (checkCode != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

                var param = _mapper.Map<TMstModel>(data);
                param.CreatedBy = user.NameIdentifier;
                param.CreatedDate = DateTime.Now;

                // Cek apakah file dikirim dari form
                if (data.ModelImage != null && data.ModelImage.Length > 0)
                {
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "product");

                    if (!string.IsNullOrEmpty(param.ModelImage))
                    {
                        var previousFilePath = Path.Combine(folderPath, param.ModelImage);
                        if (File.Exists(previousFilePath))
                        {
                            File.Delete(previousFilePath);
                        }
                    }

                    var fileExtension = Path.GetExtension(data.ModelImage.FileName);

                    string fileName = $"{data.Code}{fileExtension}";


                    param.ModelImage = fileName;

                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.ModelImage.CopyToAsync(stream);
                    }
                }

                await _uow.MstModel.Add(param);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstModelDto>(param);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstModelDto>> Delete(UserClaimModel user, string id)
        {
            try
            {
                var repoResult = await _uow.MstModel.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.MstModel.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstModelDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }


        public async Task<Result<IEnumerable<TMstModelDto>>> GetAll()
        {
            try
            {
                var repoResult = await _uow.MstModel.Set().Where(m => !m.IsDelete)
                    .Include(m => m.Brand)
                    .Include(c => c.Classes)
                    .ToListAsync();

                var result = _mapper.Map<IEnumerable<TMstModelDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstModelDto>> GetById(string id)
        {
            try
            {
                var repoResult = await _uow.MstModel.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<TMstModelDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }   
        }

        public Task<Result<TMstModelDto>> GetByParam(TMstModelDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetLastCode()
        {
            try
            {
                var lastBrandCode = await _uow.MstModel.Set().OrderByDescending(b => b.CreatedDate).FirstOrDefaultAsync();

                if (lastBrandCode == null)
                    return "M00001";

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

        public async Task<Result<IEnumerable<TMstModelDto>>> GetByCategory(string categoryCode)
        {
            try
            {

				var repoResult = await _uow.MstModel.Set()
	                .Where(m => m.IsActive && !m.IsDelete &&
				                m.Brand.CategoryDetails.Any(cd => cd.Category.Code == categoryCode && m.Type == MasterModelType.Unit))
	                .Include(m => m.Brand)
		                .ThenInclude(b => b.CategoryDetails)
			                .ThenInclude(cd => cd.Category)
	                .Include(m => m.Classes)
	                .ToListAsync();




				var result = _mapper.Map<IEnumerable<TMstModelDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }


        public async Task<Result<IEnumerable<TMstModelDto>>> GetByBrand(string brandCode)
        {
            try
            {
                //
				var repoResult = await _uow.MstModel.Set().Where(m => m.BrandCode == brandCode && m.Type == MasterModelType.Unit).ToListAsync();

				var result = _mapper.Map<IEnumerable<TMstModelDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        public async Task<Result<IEnumerable<TMstModelDto>>> GetByBrandTN(string brandCode)
        {
            try
            {
                //
				var repoResult = await _uow.MstModel.Set().Where(m => m.BrandCode == brandCode && m.Type == MasterModelType.Unit && EF.Functions.Like(m.Distributor.ToUpper(), "%TRAKTOR NUSANTARA%") && m.IsActive && !m.IsDelete).ToListAsync();

				var result = _mapper.Map<IEnumerable<TMstModelDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<TMstModelDto>> Update(UserClaimModel user, TMstModelCreatedDto data)
        {
            try
            {
                var repoResult = await _uow.MstModel.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                string previousImageFileName = repoResult.ModelImage;
                string createdBy = repoResult.CreatedBy;

                _mapper.Map(data, repoResult);

                if (data.ModelImage != null && data.ModelImage.Length > 0)
                {
                    if (!string.IsNullOrEmpty(previousImageFileName))
                    {
                        var previousImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "product", previousImageFileName);
                        if (File.Exists(previousImagePath))
                        {
                            File.Delete(previousImagePath);
                        }
                    }


                    var fileExtension = Path.GetExtension(data.ModelImage.FileName);

                    string fileName = $"{data.Code}{fileExtension}";
                    repoResult.ModelImage= fileName;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "product", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.ModelImage.CopyToAsync(stream);
                    }
                }
                else
                {
                    repoResult.ModelImage = previousImageFileName;
                }
                repoResult.CreatedBy = createdBy;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.MstModel.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstModelDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }





    }

}
