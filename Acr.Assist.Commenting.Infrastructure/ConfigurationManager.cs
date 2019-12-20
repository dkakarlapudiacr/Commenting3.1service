using System;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace Acr.Assist.CommentMicroService.Infrastructure
{
    /// <summary>
    /// Configuration manager for managing configuration related information
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration.IConfigurationManager" />
    public class ConfigurationManager : IConfigurationManager
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManager"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ConfigurationManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets or sets the connection string
        /// </summary>
        public string ConnectionString => configuration.GetConnectionString("MarvalDatabase");

        /// <summary>
        /// Get Application Title
        /// </summary>
        public string Title => configuration["Title"];

        /// <summary>
        /// Get Applciation Version
        /// </summary>
        public string Version => configuration["Version"];

        /// <summary>
        /// Get Applciation URL
        /// </summary>
        public string ApplicationURL => configuration["Environment:ApplicationURL"];

        /// <summary>
        /// Get Route to Access Swagger
        /// </summary>
        public string SwaggerRoutePrefix => configuration["Environment:SwaggerRoutePrefix"];

        /// <summary>
        /// Gets the application files path.
        /// </summary>
        public string ApplicationFilesPath => configuration["Environment:ApplicationFilesPath"];

        /// <summary>
        /// Get Root Directory path
        /// </summary>
        public string RootPath => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Gets or sets the authorization micro service URL
        /// </summary>
        public string AuthorizationMicroServiceUrl => configuration["Integrations:AuthorizationService"];

        /// <summary>
        /// Gets the email notification micro service URL.
        /// </summary>
        public string EmailNotificationMicroServiceUrl => configuration["Integrations:EmailNotificationService"];
    }
}