﻿namespace Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration
{
    /// <summary>
    /// Contains the details for configuring authorization
    /// </summary>
    public class AuthorizationConfig
    {
        /// <summary>
        /// Gets or sets the domain
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the key file
        /// </summary>
        public string KeyFilePath { get; set; }
    }
}

