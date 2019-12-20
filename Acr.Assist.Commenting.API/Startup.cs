using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Acr.Assist.Commenting.Infrastructure;
using Acr.Assist.CommentMicroService;
using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Email;
using Acr.Assist.CommentMicroService.Core.Integrations;
using Acr.Assist.CommentMicroService.Core.Services;
using Acr.Assist.CommentMicroService.Data;
using Acr.Assist.CommentMicroService.Infrastructure;
using Acr.Assist.CommentMicroService.Service;
using Acr.Assist.CommentMicroService.Service.Validator;
using Acr.Assist.CommentMicroService.SignalR;
using ACR.Assist.CommentMicroService.Integrations;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

namespace Acr.Assist.Commenting.API
{
    public class Startup
    {
        /// <summary>
        /// Program starts here
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the hosting environment.
        /// </summary>
        public IWebHostEnvironment HostingEnvironment { get; }

        /// <summary>
        /// The swagger schema name
        /// </summary>
        private readonly string swaggerSchemaName = "Bearer";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            CommentMicroService.Data.Startup.Configure();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            var authConfig = Configuration.GetSection("AuthorizationConfig").Get<AuthorizationConfig>();
            var connectionString = Configuration["MongoConnection:ConnectionString"];
            var mongoDBName = Configuration["MongoConnection:DataBase"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = authConfig.Issuer,
                       ValidAudience = authConfig.Audience,
                       IssuerSigningKey = GetKey(authConfig.KeyFilePath)
                   };

                   options.Events = new JwtBearerEvents
                   {
                       OnMessageReceived = context =>
                       {
                           var accessToken = context.Request.Query["token"];
                           if (!string.IsNullOrEmpty(accessToken) &&
                                (context.Request.Path.ToString().StartsWith("/commenting/api/v1/Notifications")))
                           {
                               context.Token = accessToken;
                           }

                           return Task.CompletedTask;
                       },
                       OnAuthenticationFailed = context =>
                       {
                           var te = context.Exception;
                           return Task.CompletedTask;
                       }
                   };
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserIdExists",
                    policy => policy.Requirements.Add(new UserIdRequirement("UserId")));
            });

            services.AddSignalR();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMvc();

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(GetMimeTypesForCompression());
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.SetIsOriginAllowed((host) => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ICommentNotificationService, CommentNotificationService>();
            services.AddTransient<ICommentQueryableService, CommentQueryableService>();
            services.AddTransient<ICommentSuggestionService, CommentSuggestionService>();
            services.AddTransient<ICommentViewService, CommentViewService>();
            services.AddTransient<IUserCommentService, UserCommentService>();
            services.AddTransient<IModuleCommentService, ModuleCommentService>();
            services.AddTransient<INotificationSenderService, NotificationSenderService>();
            services.AddTransient<ICommentRepository>(s => new CommentRepository(connectionString, mongoDBName));
            services.AddTransient<ICommentViewRepository>(s => new CommentViewRepository(connectionString, mongoDBName));
            services.AddTransient<ICommentSuggestionRepository>(s => new CommentSuggestionRepository(connectionString, mongoDBName));
            services.AddTransient<ICommentNotificationRepository>(s => new CommentNotificationRepository(connectionString, mongoDBName));
            services.AddTransient<ICommentQueryableRepository>(s => new CommentQueryableRepository(connectionString, mongoDBName));
            services.AddTransient<IUserCommentRepository>(s => new UserCommentRepository(connectionString, mongoDBName));
            services.AddTransient<IModuleCommentRepository>(s => new ModuleCommentRepository(connectionString, mongoDBName));
            services.AddSingleton<IAuthorizationHandler, UserIdExistsRequirementHandler>();
            services.AddTransient<IAuthorizationMicroService, AuthorizationMicroService>();
            services.AddTransient<IEmailTemplateManager, EmailTemplateManager>();
            services.AddTransient<IEmailNotificationMicroService, EmailNotificationMicroService>();
            services.AddTransient<IConfigurationManager, ConfigurationManager>();
            services.AddSingleton<AuthorizationConfig>(authConfig);
            services.AddTransient<IDataValidator<AddCommentEntry>, AddCommentEntryValidator>();
            services.AddTransient<IDataValidator<CommentIDDetails>, CommentIDDetailsValidator>();
            services.AddTransient<IDataValidator<CommentImplementEntry>, CommentImplementEntryValidator>();
            services.AddTransient<IDataValidator<CommentNotificationEntry>, CommentNotificationEntryValidator>();
            services.AddTransient<IDataValidator<CommentsFilter>, CommentsFilterValidator>();
            services.AddTransient<IDataValidator<CommentUserEntry>, CommentUserEntryValidator>();
            services.AddTransient<IDataValidator<UserCommentsViewEntry>, UserCommentsViewEntryValidator>();
            services.AddTransient<IDataValidator<DeleteComment>, DeleteCommentValidator>();

            services.AddSingleton<IWebHostEnvironment>(HostingEnvironment);
            services.AddSingleton<Serilog.ILogger>(Log.Logger);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["Version"], new OpenApiInfo { Title = Configuration["Title"], Version = Configuration["Version"] });
                c.IncludeXmlComments(@"App_Data\api-comments.xml");
                c.AddSecurityDefinition(swaggerSchemaName, GetSwaggerSecurityScheme());
                c.OperationFilter<SecurityRequirementsOperationFilter>(swaggerSchemaName);
            });

            services.AddMvc().AddNewtonsoftJson();
            services.AddMvcCore().AddApiExplorer();

            services.AddControllers();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseCors("AllowAllOrigins");

            app.UseRouting(); 
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = Configuration["Environment:SwaggerRoutePrefix"];
                c.DocumentTitle = Configuration["Title"] + " " + Configuration["Version"];
                c.SwaggerEndpoint(Configuration["Environment:ApplicationURL"] + "/swagger/" + Configuration["Version"] + "/swagger.json", Configuration["Title"] + " " + Configuration["Version"]);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationsHub>("/commenting/api/v1/Notifications");
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="keyFilePath">The key file path.</param>
        /// <returns></returns>
        private X509SecurityKey GetKey(string keyFilePath)
        {
            X509Certificate2 certificate;
            var certificatePath = HostingEnvironment.WebRootPath + keyFilePath;
            certificate = new X509Certificate2(certificatePath);
            return new X509SecurityKey(certificate);
        }
        
        /// <summary>
         /// Gets the swagger security scheme.
         /// </summary>
         /// <returns></returns>
        private OpenApiSecurityScheme GetSwaggerSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header. Example: " + "{token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT"
            };
        }

        /// <summary>
        /// Gets the MIME types for compression.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetMimeTypesForCompression()
        {
            return new[]
            {
                "application/json",
                "image/png",
                "image/jpeg",
                "image/gif",
                "image/tiff",
                "image/webp"
            };
        }
    }
}
