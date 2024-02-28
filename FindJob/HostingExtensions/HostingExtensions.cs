//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Options;
//using MongoDB.Bson.Serialization.Serializers;
//using MongoDB.Bson.Serialization;
//using MongoDB.Bson;
//using MongoDB.Driver;

//namespace FindJob.HostingExtensions
//{
//    internal static class HostingExtensions
//    {

//        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
//        {
//            var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

//            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
//                .AddRoleManager<RoleManager<ApplicationRole>>()
//                .AddMongoDbStores<ApplicationUser, ApplicationRole, ObjectId>
//                    (connectionString: mongoDbSettings.ConnectionString, databaseName: mongoDbSettings.DatabaseName);

//            builder.Services.Configure<MongoDbConfig>(
//                builder.Configuration.GetSection(nameof(MongoDbConfig)));
//            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer());
//            builder.Services.AddScoped<RoleManager<ApplicationRole>>();

//            var mapperConfig = new MapperConfiguration(mc =>
//            {
//                mc.AddProfile<MyMapProfile>();
//            });

//            builder.Services.AddAutoMapper(typeof(HostingExtensions));
//            //   builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();
//            builder.Services.AddSingleton<IMongoDbConfig>(sp =>
//                sp.GetRequiredService<IOptions<MongoDbConfig>>().Value);

//            builder.Services.AddSingleton<IMongoClient>(
//                new MongoClient(builder.Configuration.GetValue<string>("MongoDbConfig:ConnectionString")));

//            builder.Services
//                .AddIdentityServer(options =>
//                {
//                    options.Events.RaiseErrorEvents = true;
//                    options.Events.RaiseInformationEvents = true;
//                    options.Events.RaiseFailureEvents = true;
//                    options.Events.RaiseSuccessEvents = true;
//                    options.EmitStaticAudienceClaim = true;
//                })
//                .AddInMemoryIdentityResources(Config.IdentityResources)
//                .AddInMemoryApiScopes(Config.ApiScopes)
//                .AddInMemoryClients(Config.Clients)
//                .AddAspNetIdentity<ApplicationUser>();

//            builder.Services.AddAuthentication()
//                .AddGoogle(options =>
//                {
//                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
//                    options.ClientId = "copy client ID from Google here";
//                    options.ClientSecret = "copy client secret from Google here";
//                });
//            builder.Services.AddAuthorization(option =>
//            {
//                option.AddPolicy("RequiredSuperAdminRole",
//                    policy => policy.RequireRole("SuperAdmin"));
//                option.AddPolicy("RequiredAgencyRole",
//                    policy => policy.RequireRole("Admin", "Agency"));
//                option.AddPolicy("RequiredAdminRole",
//                    policy => policy.RequireRole("Admin"));
//            }
//            );
//            //builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//            builder.Services.AddRazorPages();
//            builder.Services.AddControllersWithViews();
//        }
