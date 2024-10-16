﻿using AutoMapper;
using Core;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;
using Core.Models.Entities.Views;
using Core.Models.Entities.Views.Transaction;
using Domain.AccessMenu;
using Domain.AccessRole;
using Domain.AccessUser;
using Domain.Account;
using Domain.Dashboard;
using Domain.Master;
using Domain.Master.Class;
using Domain.Master.ClassValue;
using Domain.Master.MasterCategory;
using Domain.Master.MasterModel;
using Domain.Master.MasterSpecItem;
using Domain.MasterComparisonType;
using Domain.MasterEmployee;
using Domain.MasterGeneral;
using Domain.MasterYardArea;
using Domain.Report;
using Domain.Transaction.Implement;
using Domain.Transaction.SpecValues;
using Domain.UnitSpec;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Domain
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
        
            #region Authentication
            CreateMap<TAccUser, UserSessionDto>();
            CreateMap<TAccRole, UserRoleDto>();
            #endregion

            #region Access Management
            CreateMap<TAccMenuGroup, MenuGroupDto>().ReverseMap();

            CreateMap<TAccMenu, MenuDto>()
                .ForMember(dest => dest.MenuGroupName, opt => opt.MapFrom(x => x.MenuGroup.Name));
            CreateMap<MenuDto, TAccMenu>();
            CreateMap<MenuUpdateDto, TAccMenu>();

            CreateMap<TAccRole, RoleDto>().ReverseMap();
            CreateMap<RoleUpdateDto, TAccRole>();

            CreateMap<RoleMenuDto, TAccMRoleMenu>();

            CreateMap<TAccUser, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(x => x.Role.Name));
            CreateMap<UserDto, TAccUser>();
            CreateMap<UserUpdateDto, TAccUser>();
            #endregion

            #region Master Data
            CreateMap<TMstGeneral, MstGeneralDto>()
                .ForMember(dest => dest.ParentCode, opt => opt.MapFrom(x => x.Parent.Code))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(x => x.Parent.Name));
            CreateMap<MstGeneralDto, TMstGeneral>()
                .ForMember(dest => dest.ChartColor, opt => opt.MapFrom(x => x.ChartColor.Contains('#') ? x.ChartColor : "#" + x.ChartColor));
            CreateMap<MstGeneralUpdateDto, TMstGeneral>()
                .ForMember(dest => dest.ChartColor, opt => opt.MapFrom(x => x.ChartColor.Contains('#') ? x.ChartColor : "#" + x.ChartColor));

            CreateMap<VwMstEmployee, MstEmployeeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name.Trim().ToUpper()));
            CreateMap<TMstEmployee, MstEmployeeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name.Trim().ToUpper()));
            CreateMap<MstEmployeeDto, TMstEmployee>()
                .ForMember(dest => dest.NRP, opt => opt.MapFrom(x => x.NRP.Trim()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name.Trim().ToUpper()));
            CreateMap<MstEmployeeUpdateDto, TMstEmployee>()
                .ForMember(dest => dest.NRP, opt => opt.MapFrom(x => x.NRP.Trim()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name.Trim().ToUpper()));

            CreateMap<TtMstYardArea, YardAreaDto>();
            CreateMap<YardAreaDto, TtMstYardArea>();

            CreateMap<MasterComparisonTypeDto, TMstComparisonType>();
            CreateMap<TMstComparisonType, MasterComparisonTypeDto>();




            #endregion

            #region Master Data
            // Master Brand
            
            CreateMap<TMstBrand, TMstBrandDto>();
            CreateMap<TMstBrandDto, TMstBrand>();
            CreateMap<TMstBrandCreateDto, TMstBrand>();
            CreateMap<TMstBrand, TMstBrandCreateDto>();

            //Category

            CreateMap<TMstCategoryCreatedDto, TMstCategory>();
            CreateMap<TMstCategoryUpdatedDto, TMstCategory>();
            CreateMap<TMstCategory, TMstCategoryCreatedDto>();
            CreateMap<TMstCategory, TMstCategoryDto>()
                .ForMember(dest => dest.CategoryDetails, opt => opt.MapFrom(src => src.CategoryDetails));


            // Category Detail

            CreateMap<TMstCategoryDetail, TMstCategoryDetailDto>();

            CreateMap<TMstCategoryDetailDto, TMstBrandDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.BrandCode));

            CreateMap<TMstCategoryDetailDto, TMstCategoryDetail>();



            // Class 

            


            CreateMap<TMstClassCreatedDto, TMstClass>();
            CreateMap<TMstClass, TMstClassDto>();
            CreateMap<TMstClassDto, TMstClass>();

            // Class Value
            CreateMap<TMstClassCreatedDto, TMstClassValue>();            
            CreateMap<TMstClassValue, TMstClassValueDto>();
            CreateMap<TMstClassValueDto, TMstClassValue>();


            // Model
            CreateMap<TMstModel, TMstModelDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Classes, opt => opt.MapFrom(src => src.Classes));

            CreateMap<TMstModelCreatedDto,TMstModel>();
            CreateMap<TMstModelCreatedDto, TMstModelDto>();


            // Spec Item
            CreateMap<TMstSpecItem, TMstSpecItemsDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<TMstSpecItemsCreatedDto, TMstSpecItem>();

            #endregion

            #region Transaction

            // Spec Values
            CreateMap<VwSpecValueMatriks, TTrnSpecValuesDto>();
            CreateMap<TTrnSpecValues, TTrnSpecValuesDto>();
            CreateMap<TTrnSpecValuesCreatedDto, TTrnSpecValues>()
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .ForMember(dest => dest.SubItems, opt => opt.Ignore());

            CreateMap<TTrnSpecValues, TTrnSpecValuesCreatedDto>();

            //Impelement
            CreateMap<TTrnImplement, TTrnImplementDto>();
            CreateMap<TTrnImplementDto, TTrnImplement>();
            //.ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            //.ForMember(dest => dest.ModelAttach, opt => opt.MapFrom(src => src.ModelAttach))
            //.ForMember(dest => dest.ClassValue, opt => opt.MapFrom(src => src.ClassValue));

            // Unit Spec
            CreateMap<VwUnitSpec, UnitSpecDto>();


            




            #endregion

            #region Report
            CreateMap<VwMstAsset, ReportDeviceDto>();
            CreateMap<VwMstAsset, ReportTransDeviceDto>();
            CreateMap<VwTransDeployDevice, ReportDeviceDto>();
            CreateMap<VwTransDeployDevice, ReportTransDeviceDto>();
            CreateMap<VwTransDeployDevice, ReportDeviceHistoryDto>();
            #endregion

            #region Dashboard
            CreateMap<VwDsDeviceStockByBrand, DeviceStockByBrandDto>();
            CreateMap<VwDsDeviceStockByStatus, DeviceStockByStatusDto>()
                .ForMember(dest => dest.DeviceStatusUI, opt => opt.MapFrom(x => AssetStatus.GetAssetStatusUI(x.DeviceStatus)));
            CreateMap<VwDsDeviceAllocated, DeviceAllocatedDto>();
            #endregion

            #region Upload
            #endregion
        }
    }
}
