using UsersManagement.Repository.IRepository;
using UsersManagement.Repository.Repository;
using UsersManagement.Services.IService;
using UsersManagement.Services.Service;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UsersManagement.Repository.Entities;
using UsersManagement.Services.DTO;
using AutoMapper.Extensions.ExpressionMapping;
using UsersManagement.Services.Mappings;
using UsersManagement.Repository;
using UsersManagement.Services.UOW;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureLogging(options =>
    {
        options.ClearProviders();
    })
    .UseNLog();

    // Add services to the container.
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddControllersWithViews();

    builder.Services.AddDbContext<UsersDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("UserManagementContext"));
    });

    builder.Services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped(typeof(IServiceAsync<BaseEntity, BaseEntityDto>), typeof(ServiceAsync<BaseEntity, BaseEntityDto>));
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IRoleService, RoleService>();
    builder.Services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); }, typeof(MappingProfile).Assembly);
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=User}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
}