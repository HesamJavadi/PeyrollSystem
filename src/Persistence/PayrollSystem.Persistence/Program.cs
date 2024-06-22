using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PayrollSystem.Domain.LOC;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using PayrollSystem.Persistence.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using PayrollSystem.Persistence.Authorization.Handlers;
using PayrollSystem.Persistence.Middleware;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var OutConnectionString = builder.Configuration.GetConnectionString("OutConnection");


builder.Services.AddInfrastructure(ConnectionString, OutConnectionString);
builder.Services.AddControllers();
builder.Host.UseSerilog((ctx, lc) => lc
                                    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
                                    .WriteTo.MSSqlServer(connectionString: "Server=.;Database=PayrollSystem;User Id=sa;Password=123Hesam@@;TrustServerCertificate=true;Encrypt=true;"
, sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs" })
                                    .ReadFrom.Configuration(ctx.Configuration)
                                    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Define JWT Bearer token authentication scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = " ",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});

var jwtIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer");
var jwtAudience = builder.Configuration.GetValue<string>("Jwt:Audience");
var jwtKey = builder.Configuration.GetValue<string>("Jwt:Key");


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddSingleton<IAuthorizationHandler, DynamicClaimHandler>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminRead", policy =>
        policy.RequireRole("Admin").RequireClaim("PermissionAdmin", "Read"));
    options.AddPolicy("AdminCreate", policy =>
        policy.RequireRole("Admin").RequireClaim("PermissionAdmin", "Create"));
    options.AddPolicy("AdminUpdate", policy =>
        policy.RequireRole("Admin").RequireClaim("PermissionAdmin", "Update"));
    options.AddPolicy("AdminDelete", policy =>
        policy.RequireRole("Admin").RequireClaim("PermissionAdmin", "Delete"));

    options.AddPolicy("UserRead", policy =>
        policy.RequireRole("User").RequireClaim("PermissionUser", "Read"));
    options.AddPolicy("UserCreate", policy =>
        policy.RequireRole("User").RequireClaim("PermissionUser", "Create"));
    options.AddPolicy("UserUpdate", policy =>
        policy.RequireRole("User").RequireClaim("PermissionUser", "Update"));
    options.AddPolicy("UserDelete", policy =>
        policy.RequireRole("User").RequireClaim("PermissionUser", "Delete"));

    options.AddPolicy("PayStatementRead", policy =>
        policy.RequireRole("PayStatement").RequireClaim("PermissionPayStatement", "Read"));
    options.AddPolicy("PayStatementCreate", policy =>
        policy.RequireRole("PayStatement").RequireClaim("PermissionPayStatement", "Create"));
    options.AddPolicy("PayStatementUpdate", policy =>
        policy.RequireRole("PayStatement").RequireClaim("PermissionPayStatement", "Update"));
    options.AddPolicy("PayStatementDelete", policy =>
        policy.RequireRole("PayStatement").RequireClaim("PermissionPayStatement", "Delete"));

    options.AddPolicy("PersonnelRead", policy =>
        policy.RequireRole("Personnel").RequireClaim("PermissionPersonnel", "Read"));
    options.AddPolicy("PersonnelCreate", policy =>
        policy.RequireRole("Personnel").RequireClaim("PermissionPersonnel", "Create"));
    options.AddPolicy("PersonnelUpdate", policy =>
        policy.RequireRole("Personnel").RequireClaim("PermissionPersonnel", "Update"));
    options.AddPolicy("PersonnelDelete", policy =>
        policy.RequireRole("Personnel").RequireClaim("PermissionPersonnel", "Delete"));

    options.AddPolicy("PaySlipRead", policy =>
        policy.RequireRole("PaySlip").RequireClaim("PermissionPaySlip", "Read"));
    options.AddPolicy("PaySlipCreate", policy =>
        policy.RequireRole("PaySlip").RequireClaim("PermissionPaySlip", "Create"));
    options.AddPolicy("PaySlipUpdate", policy =>
        policy.RequireRole("PaySlip").RequireClaim("PermissionPaySlip", "Update"));
    options.AddPolicy("PaySlipDelete", policy =>
        policy.RequireRole("PaySlip").RequireClaim("PermissionPaySlip", "Delete"));


    options.AddPolicy("SettingRead", policy =>
        policy.RequireRole("Setting").RequireClaim("PermissionSetting", "Read").Requirements.Add(new DynamicClaimRequirement("PermissionSetting", "Read")));
    options.AddPolicy("SettingCreate", policy =>
        policy.RequireRole("Setting").RequireClaim("PermissionSetting", "Create").Requirements.Add(new DynamicClaimRequirement("PermissionSetting", "Create")));
    options.AddPolicy("SettingUpdate", policy =>
        policy.RequireRole("Setting").RequireClaim("PermissionSetting", "Update").Requirements.Add(new DynamicClaimRequirement("PermissionSetting", "Update")));
    options.AddPolicy("SettingDelete", policy =>
        policy.RequireRole("Setting").RequireClaim("PermissionSetting", "Delete").Requirements.Add(new DynamicClaimRequirement("PermissionSetting", "Delete")));
});


var app = builder.Build();

// Seed roles and claims
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        await services.SeedData();
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while seeding roles and claims.");
//    }
//}

app.UseCors("AllowSpecificOrigins");
app.UseMiddleware<ApiExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("Server");
    await next();
});

app.Run();
