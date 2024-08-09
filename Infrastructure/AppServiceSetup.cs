using Core.Interfaces;
using Core.Interfaces.IRepositories.Tables.Master;
using Data;
using Domain.AccessMenu;
using Domain.AccessRole;
using Domain.AccessUser;
using Domain.Account;
using Domain.Common;
using Domain.Comparison;
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
using Domain.Scheduler;
using Domain.Transaction.Implement;
using Domain.Transaction.SpecValues;
using Domain.UnitSpec;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class AppServiceSetup
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<ISchedulerService, SchedulerService>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccessMenuService, AccessMenuService>();
            services.AddTransient<IAccessRoleService, AccessRoleService>();
            services.AddTransient<IAccessUserService, AccessUserService>();

            services.AddTransient<IMasterEmployeeService, MasterEmployeeService>();
            services.AddTransient<IMasterGeneralService, MasterGeneralService>();


            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IDashboardService, DashboardService>();

            #region  Master Data 
            services.AddTransient<IMstBrandService, TMstBrandService>();
            services.AddTransient<IMstCategoryService, TMstCategoryService>();
            services.AddTransient<IMstClassService, TMstClassService>();
            services.AddTransient<IMstClassValueService, TMstClassValueService>();
            services.AddTransient<IMstModelService, TMstModelService>();
            services.AddTransient<ITMstSpecItemsService, TMstSpecItemsService>();
            services.AddTransient<IMasterComparisonService, MasterComparisonTypeService>();

			#endregion


			#region Transaction
	
            services.AddTransient<ITrnSpecValuesService, TTrnSpecValuesService>();
            services.AddTransient<ITrnImplementService, TTrnImplementsService>();
            services.AddTransient<IUnitSpecService, UnitSpecService>();
            services.AddTransient<IComparisonService, ComparisonService>();
            services.AddTransient<IImplementService, ImplementService>();

            #endregion

            return services;
        }
    }
}
