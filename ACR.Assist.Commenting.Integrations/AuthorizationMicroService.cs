using System;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Configuration;
using Acr.Assist.CommentMicroService.Core.Integrations;
using Acr.Assist.CommentMicroService.Core.Integrations.DTO;
using Newtonsoft.Json;
using Serilog;

namespace ACR.Assist.CommentMicroService.Integrations
{
    /// <summary>
    /// Business micro service for the authorization notifcation microservice
    /// </summary>
    public class AuthorizationMicroService : IAuthorizationMicroService
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
        /// Initializes a new instance of the <see cref="AuthorizationMicroService"/> class.
        /// </summary>
        /// <param name="configurationManager">The configuration manager.</param>
        /// <param name="logger">The logger.</param>
        public AuthorizationMicroService(IConfigurationManager configurationManager, ILogger logger)
        {
            this.configurationManager = configurationManager;
            this.logger = logger;
        }

        /// <summary>
        /// Checks if the user with  user account is rejecetd
        /// </summary>
        /// <param name="accountRejectionStatusRequest"> Contains the details for making a request to get the account status rejection</param>
        /// <returns>True if the User is rejected else it returns alse </returns>
        public async Task<bool> CheckIfUserIsRejected(AccountRejectionStatusRequest accountRejectionStatusRequest)
        {
            bool checkIfUserIsRejected = true;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = string.Format("{0}/users/{1}/accountStatus/checkIsRejected", configurationManager.AuthorizationMicroServiceUrl,
                           accountRejectionStatusRequest.UserId);
                    client.DefaultRequestHeaders.Add("Authorization", accountRejectionStatusRequest.AccessToken);
                    using (HttpResponseMessage res = await client.GetAsync(url))
                    {
                        if (res.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();
                                if (data != null)
                                {
                                    var result = JsonConvert.DeserializeObject<AccountRejectionStatus>(data);
                                    checkIfUserIsRejected = result.IsAccountInRejectedState;
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "AuthorizationMicroService:CheckIfUserIsRejected");
            }

            return checkIfUserIsRejected;
        }
    }
}
