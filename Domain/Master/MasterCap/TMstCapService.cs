using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Domain.Master.MasterCategory;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.EntityFrameworkCore;




namespace Domain.Master.Cap
{
    public class TMstCapService : IMstCapService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TMstCapService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<TMstCapDto>> Create(UserClaimModel user, TMstCapCreatedDto data)
        {
            try
            {

                var checkCode = await _uow.MstCap.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                if (checkCode != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

                var param = _mapper.Map<TMstCap>(data);
                param.Name = data.Name;
                param.UpdatedDate = null;
                param.CreatedBy = user.NameIdentifier;
                param.CreatedDate = DateTime.Now;

                await _uow.MstCap.Add(param);
                await _uow.CompleteAsync();
                
                var result = _mapper.Map<TMstCapDto>(param);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstCapDto>> Delete(UserClaimModel user, string id)
        {
            try
            {
                var repoResult = await _uow.MstCap.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.MstCap.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstCapDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }


        public async Task<Result<IEnumerable<TMstCapDto>>> GetAll()
        {
            try
            {
                var repoResult = await _uow.MstCap.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<TMstCapDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Code).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            throw new NotImplementedException();
        }

        public async Task<Result<TMstCapDto>> GetById(string id)
        {
            try
            {
                var repoResult = await _uow.MstCap.Set().FirstOrDefaultAsync(m => m.Code == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<TMstCapDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }   
        }

        public Task<Result<TMstCapDto>> GetByParam(TMstCapDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetLastCode()
        {
            try
            {
                var lastNum = await _uow.MstCap.Set().OrderByDescending(b => b.CreatedDate).FirstOrDefaultAsync();

                if (lastNum == null)
                    return Result.Ok("Cap001");


                // Assuming the code format is always something like 'CapXXXXX' (e.g., Cap00001, Cap00002, etc.)
                string lastCode = lastNum.Code;

                // Extract the prefix, in this case 'Cap'
                string prefix = new string(lastCode.TakeWhile(char.IsLetter).ToArray());

                // Extract the numeric part after the prefix, and increment it
                string numericPart = new string(lastCode.SkipWhile(char.IsLetter).ToArray());
                int lastNumber = int.Parse(numericPart);

                // Increment the number and format it with leading zeros (4 digits)
                lastNumber++;
                string nextCode = $"{prefix}{lastNumber:D3}";

                // Return the next code
                return Result.Ok(nextCode);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }

            throw new NotImplementedException();
        }

        public Task<Result<TMstCapDto>> GetListByParam(TMstCapDto param)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TMstCapDto>> Update(UserClaimModel user, TMstCapCreatedDto data)
        {
            try
            {
                var repoResult = await _uow.MstCap.Set().FirstOrDefaultAsync(m => m.Code == data.Code);

                string CreatedBy = repoResult.CreatedBy;



                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _mapper.Map(data, repoResult);
                repoResult.Name= data.Name;
                repoResult.CreatedBy = CreatedBy;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.MstCap.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<TMstCapDto>(repoResult);

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
