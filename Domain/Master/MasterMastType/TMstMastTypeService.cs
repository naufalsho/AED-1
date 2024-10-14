using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Domain.Master.MasterCategory;
using Domain.Master.MastType;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.EntityFrameworkCore;




namespace Domain.Master.Class
{
    public class TMstMastTypeService : IMstMastTypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TMstMastTypeService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<TMstMastTypeDto>> Create(UserClaimModel user, TMstMastTypeCreatedDto data)
        {
            try
            {

                var checkCode = await _uow.MstMastType.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                if (checkCode != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

                var param = _mapper.Map<TMstMastType>(data);
                param.Name = data.Name;
                param.UpdatedDate = null;
                param.CreatedBy = user.NameIdentifier;
                param.CreatedDate = DateTime.Now;

                await _uow.MstMastType.Add(param);
                await _uow.CompleteAsync();
                
                var result = _mapper.Map<TMstMastTypeDto>(param);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstMastTypeDto>> Delete(UserClaimModel user, string id)
        {
            try
            {
                var repoResult = await _uow.MstMastType.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.MstMastType.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstMastTypeDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }


        public async Task<Result<IEnumerable<TMstMastTypeDto>>> GetAll()
        {
            try
            {
                var repoResult = await _uow.MstMastType.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<TMstMastTypeDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstMastTypeDto>> GetById(string id)
        {
            try
            {
                var repoResult = await _uow.MstMastType.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<TMstMastTypeDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }   
        }

        public Task<Result<TMstMastTypeDto>> GetByParam(TMstMastTypeDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetLastCode()
        {
            try
            {
                var lastBrandCode = await _uow.MstMastType.Set().OrderByDescending(b => b.CreatedDate).FirstOrDefaultAsync();

                if (lastBrandCode == null)
                    return Result.Ok("MT0001");


                string prefix = lastBrandCode.Code.Substring(0, 2); // Misalnya, 'B' dari 'B000'
                int lastNumber = int.Parse(lastBrandCode.Code.Substring(2)); // Misalnya, 000 dari 'B000'

                // Menambahkan 1 pada bagian nomor
                lastNumber++;

                // Menggabungkan kembali bagian huruf dan bagian nomor yang telah diubah
                string nextCode = $"{prefix}{lastNumber:D5}"; // Format nomor agar selalu tiga digit

                Console.WriteLine(nextCode);

                
                return Result.Ok(nextCode);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }

            throw new NotImplementedException();
        }

        public Task<Result<TMstMastTypeDto>> GetListByParam(TMstMastTypeDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TMstMastTypeDto>> Update(UserClaimModel user, TMstMastTypeCreatedDto data)
        {
            try
            {
                var repoResult = await _uow.MstMastType.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                string CreatedBy = repoResult.CreatedBy;



                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _mapper.Map(data, repoResult);
                repoResult.Name= data.Name;
                repoResult.CreatedBy = CreatedBy;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.MstMastType.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstMastTypeDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }





    }

}
