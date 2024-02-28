
using AutoMapper;
using FindJob.Core;
using FindJob.Core.Utilities;
using FindJob.CustomPolicy.RoleHandler;
using FindJob.Data.Entities;
using FindJob.Data.Repositories.JobRepository;
using FindJob.Data.Repositories.JobsRepository;
using FindJob.Data.Repositories.PermissionRepository;
using FindJob.Data.Repositories.RoleRepository;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add MVCServices to the container.
builder.Services.AddControllersWithViews();

// Add DatabaseSettings to the container.

//var findJobDatabaseSettings = builder.Configuration.GetSection(nameof(FindJobDatabaseSettings)).Get<FindJobDatabaseSettings>();
//builder.Services.Configure<FindJobDatabaseSettings>( builder.Configuration.GetSection("FindJobDatabaseSettings"));
//         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
var findJobDatabaseSettings = new FindJobDatabaseSettings();
builder.Configuration.Bind(nameof(FindJobDatabaseSettings), findJobDatabaseSettings);
builder.Services.AddSingleton(findJobDatabaseSettings);


// Add Identity to the container.
builder.Services.AddIdentity<AppUser, AppRole>()
   .AddMongoDbStores<AppUser, AppRole, ObjectId>(findJobDatabaseSettings.ConnectionString, findJobDatabaseSettings.DatabaseName );

// Add Identity to the container.
//builder.Services.AddScoped<IAuthorizationHandler, AllowUsersHandler>();


//builder.Services.AddAuthentication()
//        .AddGoogle(opts =>
//        {
//            opts.ClientId = "717469225962-3vk00r8tglnbts1cgc4j1afqb358o8nj.apps.googleusercontent.com";
//            opts.ClientSecret = "babQzWPLGwfOQVi0EYR-7Fbb";
//            opts.SignInScheme = IdentityConstants.ExternalScheme;
//        });
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("CustomRolePolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new CustomRoleAuthorizationRequirement());
    });
});

//builder.Services.AddScoped<IAuthorizationHandler, CustomRoleHandler>();



// Add JobsRepository to the container
//builder.Services.AddScoped(typeof(IRepository<>) , typeof(MongoRepository<>));
builder.Services.AddScoped<IJobsRepository, JobsRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
// Add Automapper to the container
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile<MyMapProfile>();
});

builder.Services.AddAutoMapper(typeof(MyMapProfile));



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
