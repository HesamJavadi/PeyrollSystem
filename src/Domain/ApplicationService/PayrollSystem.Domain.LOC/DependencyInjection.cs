using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayrollSystem.Domain.ApplicationService.Common;
using PayrollSystem.Domain.ApplicationService.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Data.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Service.Management.WebServiceManagement;
using PayrollSystem.Infrastructure.SQL.Common;
using PayrollSystem.Infrastructure.SQL.Management.WebServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;
using System.Data;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Infrastructure.Dapper.PayStub;
using PayrollSystem.Domain.Contracts.Service.Personnel.PayStub;
using PayrollSystem.Domain.ApplicationService.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStatement;
using PayrollSystem.Infrastructure.Dapper.PayStatement;
using PayrollSystem.Domain.ApplicationService.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Service.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.InfraService;
using PayrollSystem.Infrastructure.Service.AuthService;
using PayrollSystem.Infrastructure.SQL.Management.Setting;
using PayrollSystem.Infrastructure.Service.SmsService;
using PayrollSystem.Domain.Contracts.Data.Management.Setting;
using PayrollSystem.Domain.Core.Entities.Management.Setting;
using PayrollSystem.Domain.Contracts.Service.Management.Setting;
using PayrollSystem.Domain.ApplicationService.Management.Setting;
using PayrollSystem.Infrastructure.Service.UploadFiles;
using PayrollSystem.Domain.ApplicationService.Management.Roles;
using PayrollSystem.Domain.Contracts.Service.Roles;
using PayrollSystem.Infrastructure.SQL.SeedData;
using PayrollSystem.Domain.Contracts.Service.User;
using PayrollSystem.Domain.Contracts.Translator;
using PayrollSystem.Infrastructure.Service.TranslationService;
using PayrollSystem.Infrastructure.SQL.Logger;
using PayrollSystem.Domain.Contracts.Data.Logger;
using PayrollSystem.Domain.Contracts.Service.Logger;
using PayrollSystem.Domain.ApplicationService.Logger;
using PayrollSystem.Infrastructure.Dapper.User;

namespace PayrollSystem.Domain.LOC;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString, string OutConnectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(180)
            ));

        services.AddIdentity<ApplicationUser, ApplicationRole>().AddRoles<ApplicationRole>()
     .AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();


        services.AddScoped<IDbConnection>(provider => new SqlConnection(
         OutConnectionString));

        services.AddSingleton<IMapper>(AutoMapperConfiguration.Configure());



        #region AppService
        services.AddScoped<IWebServiceManagementService, WebServiceManagementService>();
        services.AddScoped<IPayStubService, PayStubService>();
        services.AddScoped<IPayStatementService, PayStatementService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISerilogLoggerService, SerilogLoggerService>();
        #endregion
        #region infra Repo 
        services.AddScoped<IPayStatementRepository, PayStatementRepository>();
        services.AddScoped<IPayStubRepository, PayStubRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWebServiceManagementRepository, WebServiceManagementRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<ISerilogLogger, SerilogLogger>();
        #endregion
        #region infra service 
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ISendSms, SendSms>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUploadFileHandlerService, UploadFileHandlerService>();
        services.AddSingleton<ITranslator, TranslationService>();
        #endregion
    }

    //public static async Task SeedData(this IServiceProvider serviceProvider)
    //{
        //var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        //await SeedRolesAndClaims.Initialize(serviceProvider, userManager);
        //await SeedRolesAndClaims.AdminSeeds(serviceProvider, userManager,roleManager);
    //}
}
