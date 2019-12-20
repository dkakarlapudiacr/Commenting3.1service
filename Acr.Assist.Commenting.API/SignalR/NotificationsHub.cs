using Acr.Assist.CommentMicroService.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.SignalR
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class NotificationsHub : Hub
    {
        /// <summary>
        /// The logger/
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The users
        /// </summary>
        private static Dictionary<string, string> _users = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsHub"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public NotificationsHub(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Sends the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public async Task Send(UserUnViewedComment comment)
        {
            await Clients.All.SendAsync("receive", comment);
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static List<string> GetConnections(string userId)
        {
            return _users.Where(v => v.Value == userId.ToLower()).Select(v => v.Key).ToList();
        }

        /// <summary>
        /// Subscribes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="modules">The modules.</param>
        public void Subscribe(string userId, List<string> modules)
        {
            logger.Information("On Subscribe Method Start");
            logger.Information("Connection ID : " + Context.ConnectionId);
            logger.Information("userId : " + userId);
            if (modules != null)
            {
                logger.Information("modules count : " + modules.Count);
            }
            else
            {
                logger.Information("modules : NULL ");
            }
            if (!_users.Any(i => i.Key == Context.ConnectionId))
            {
                _users.Add(Context.ConnectionId, userId.ToLower());
            }
            if (!Context.Items.Any(i => i.Key.ToString() == "modules"))
            {
                Context.Items.Add("modules", modules);
            }
            foreach (var module in modules)
            {

                Groups.AddToGroupAsync(Context.ConnectionId, module.ToLower());
            }
            logger.Information("On Subscribe Method End");

        }

        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            logger.Information("On Connected Method Start");
            logger.Information("Connection ID : " + Context.ConnectionId);
            await base.OnConnectedAsync();
            logger.Information("On Connected Method End");
        }

        /// <summary>
        /// Called when a connection with the hub is terminated.
        /// </summary>
        /// <param name="exception"></param>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            logger.Information("On Disconnected");
            logger.Information("Connection ID : " + Context.ConnectionId);
            var modules = Context.Items["modules"] as List<string>;
            _users.Remove(Context.ConnectionId);
            await UnSubscribe(modules);
            await base.OnDisconnectedAsync(exception);
            logger.Information(exception, "logging parameters in OnDisconnected");
            logger.Information("On Disconnected Method End");
        }

        /// <summary>
        /// Uns the subscribe.
        /// </summary>
        /// <param name="modules">The modules.</param>
        public async Task UnSubscribe(List<string> modules)
        {
            logger.Information("On UnSubscribe Method Start");
            logger.Information("Connection ID : " + Context.ConnectionId);
            if (modules != null)
            {
                logger.Information("modules count : " + modules.Count);
            }
            else
            {
                logger.Information("modules : NULL ");
            }
            if (modules != null && modules.Count > 0)
            {
                foreach (var module in modules)
                {

                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, module.ToLower());
                }
            }
            logger.Information("On UnSubscribe Method End");
        }
    }
}