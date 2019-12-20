using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;
using Acr.Assist.CommentMicroService.Core.Integrations;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ACR.Assist.CommentMicroService.Integrations
{
    /// <summary>
    /// Business micro service for the email notifcation microservice
    /// </summary>
    public class EmailNotificationMicroService : IEmailNotificationMicroService
    {
        /// <summary>
        /// The configuration manager
        /// </summary>
        private readonly IConfigurationManager configurationManager;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationMicroService"/> class.
        /// </summary>
        /// <param name="configurationManager">The configuration manager.</param>
        /// <param name="logger">The logger.</param>
        public EmailNotificationMicroService(IConfigurationManager configurationManager, ILogger logger)
        {
            this.configurationManager = configurationManager;
            this.logger = logger;
        }

        /// <summary>
        /// Checks if user is rejected.
        /// </summary>
        /// <param name="emailMessage">The email message.</param>
        /// <returns></returns>
        public async Task<Guid> SendEmailNotification(EmailMessage emailMessage)
        {
            Guid recordId = Guid.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = string.Format("{0}/send/notification", configurationManager.EmailNotificationMicroServiceUrl);
                    var param = JsonConvert.SerializeObject(emailMessage);
                    HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage res = await client.PostAsync(url, contentPost))
                    {
                        if (res.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();
                                if (data != null)
                                {
                                    var id = JsonConvert.DeserializeObject<string>(await content.ReadAsStringAsync());
                                    recordId = Guid.Parse(id);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "SendEmailNotification:SendFailed");
            }

            return recordId;
        }
    }
}