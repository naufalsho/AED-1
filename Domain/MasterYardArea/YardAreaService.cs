using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Domain.MasterYardArea;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FluentResults;





namespace Domain.Account
{
    public class YardAreaService : IYardAreaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public YardAreaService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<YardAreaDto>> Create(UserClaimModel user, YardAreaDto data)
        {
            try
            {
                var checkCode = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.CodeArea == data.CodeArea);

                if (checkCode != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Code not available. Please change the code!");

                var param = _mapper.Map<TtMstYardArea>(data);
                param.CodeArea = data.CodeArea;
                param.Name = data.Name;
                param.CurrentOccupancy =  data.CurrentOccupancy;
                param.Capacity = data.Capacity;
                param.UpdatedDate = null;
                param.CreatedBy = user.NameIdentifier;
                param.CreatedDate = DateTime.Now;
                param.YardQRCode = GenerateQRCode(data);

                await _uow.MstYardArea.Add(param);
                await _uow.CompleteAsync();

                return Result.Ok(data);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            
        }

        public async Task<Result<YardAreaDto>> Delete(UserClaimModel user, int id)
        {
            try
            {
                var repoResult = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.MstYardArea.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<YardAreaDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<YardAreaDto>>> GetAll()
        {
            try
            {
                
                var repoResult = await _uow.MstYardArea.ExecuteStoredProcedure("sp_GetMstYardArea");

                // var repoResult = await _uow.MstYardArea.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<YardAreaDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Name).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<YardAreaDto>> GetById(int id)
        {
            try
            {
                var repoResult = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<YardAreaDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }        
        }

        public async Task<Result<YardAreaDto>> GetByParam(YardAreaFilterDto param)
        {
            try
            {


            dynamic parameters = new
            {
                IsDelete = param.IsDelete,
                Id = param.Id
            };

            var result = await _uow.MstYardArea.ExecuteStoredProcedure("YourStoredProcedureName", parameters).FirstOrDefaultAsync();

            return Result.Ok();

            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }        
        }


        // public async Task<Result<YardAreaDto>> GetByParam(YardAreaFilterDto param)
        // {
        //     try
        //     {
        //         var query = _uow.MstYardArea.Set().AsQueryable();

        //         // Apply filtering based on the parameters in YardAreaFilterDto
        //         if (param.Id.HasValue)
        //         {
        //             query = query.Where(m => m.Id == param.Id);
        //         }

        //         if (!string.IsNullOrEmpty(param.Name))
        //         {
        //             query = query.Where(m => m.Name.Contains(param.Name));
        //         }

        //         // Add more filter conditions as needed

        //         var repoResult = await query.FirstOrDefaultAsync();

        //         if (repoResult == null)
        //             return Result.Fail<ResponseStatusCode>("Data not found!");

        //         var result = _mapper.Map<YardAreaDto>(repoResult);

        //         return Result.Ok(result);
        //     }
        //     catch (Exception ex)
        //     {
        //         return Result.Fail<ResponseStatusCode>(ResponseStatusCode.InternalServerError, ex.GetMessage());
        //     }
        // }



        // public async Task<Result<YardAreaDto>> GetByParam(YardAreaFilterDto param)
        // {
        //     try
        //     {
        //         var repoResult = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.Id == id);

        //         // var query =_uow.MstYardArea.Set();
        //         // Expression<Func<YardAreaFilterDto, bool>> whereCond = c => true;


        //         // if(param.Id.HasValue)
        //         // {
        //         //     whereCond = whereCond.And(m => m.Id == param.Id);
        //         // }


        //         // if(param.IsDelete.HasValue)
        //         // {
        //         //     whereCond = whereCond.And(m => m.IsDelete == param.IsDelete);
        //         // }

        //         // var repoResult = await query.Where(whereCond).FirstOrDefaultAsync();






        //         // if (repoResult == null)
        //         //     return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

        //         // var result = _mapper.Map<YardAreaDto>(repoResult);

        //         return Result.Ok(result);
        //     }
        //     catch (Exception ex)
        //     {
        //         return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //     }
        // }

        public async Task<Result<IEnumerable<YardAreaDto>>> GetList()
        {
            try
            {
                var repoResult = await _uow.MstYardArea.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<YardAreaDto>>(repoResult);

                return Result.Ok(result.OrderBy(m => m.Name).AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<YardAreaDto>> Update(UserClaimModel user, YardAreaDto data)
        {
            try
            {
                // Verifikasi model
                var validationContext = new ValidationContext(data, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(data, validationContext, validationResults, validateAllProperties: true))
                {
                    var errorMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                    return Result.Fail(ResponseStatusCode.BadRequest + ":" + errorMessage);
                }

                // Gunakan ekspresi lambda yang lebih efisien
                var repoResult = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                // Memperbarui properti yang diperlukan saja
                repoResult.Name = data.Name;
                repoResult.Capacity = data.Capacity;
                repoResult.CurrentOccupancy = data.CurrentOccupancy;
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate= DateTime.Now;

                _uow.MstYardArea.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<YardAreaDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }



        //public async Task<Result<YardAreaDto>> Update(UserClaimModel user, YardAreaDto data)
        //{
        //    try
        //    {
        //        var repoResult = await _uow.MstYardArea.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

        //        string YardQRCode = repoResult.YardQRCode;
        //        string CreatedBy = repoResult.CreatedBy;
        //        DateTime CreatedDate = repoResult.CreatedDate;

        //        //"a4fe46dc-686d-4678-8347-4deae2c0cadf.png"

        //        if (repoResult == null)
        //            return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

        //        _mapper.Map(data, repoResult);
        //        repoResult.YardQRCode = YardQRCode;
        //        repoResult.CreatedDate= CreatedDate;
        //        repoResult.CreatedBy= CreatedBy;
        //        repoResult.UpdatedBy = user.NameIdentifier;
        //        repoResult.UpdatedDate = DateTime.Now;

        //        _uow.MstYardArea.Update(repoResult);
        //        await _uow.CompleteAsync();

        //        var result = _mapper.Map<YardAreaDto>(repoResult);

        //        return Result.Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
        //}


        private string GenerateQRCode(YardAreaDto data)
        {
            var qrWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    Height = 300,
                    Width = 300
                }
            };

            var pixelData = qrWriter.Write(data.CodeArea.ToString());
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                        pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }


                // Simpan gambar sebagai file PNG
                string filename = Guid.NewGuid().ToString() + ".png";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets/images/qrcode", filename);
                bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                // Kembalikan URL gambar QR code
                return filename;


            }
        }




    }

}
