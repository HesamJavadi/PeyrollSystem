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

namespace PayrollSystem.Domain.LOC;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString, string OutConnectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<ApplicationUser, IdentityRole>()
     .AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();


        services.AddTransient<IDbConnection>(provider => new SqlConnection(
         OutConnectionString));

        services.AddSingleton<IMapper>(AutoMapperConfiguration.Configure());


        services.AddScoped<IWebServiceManagementService, WebServiceManagementService>();
        services.AddScoped<IWebServiceManagementRepository, WebServiceManagementRepository>();


        services.AddScoped<IPayStubService, PayStubService>();
        services.AddScoped<IPayStubRepository, PayStubRepository>();

        services.AddScoped<IPayStatementService, PayStatementService>();
        services.AddScoped<IPayStatementRepository, PayStatementRepository>();


        services.AddScoped<IJwtTokenService, JwtTokenService>();
    }
}
