using Shoppite.Application.Commands;
using Shoppite.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shoppite.Core.DTOs;
using Shoppite.Infrastructure.Helpers;
using FirebaseNet.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Shoppite.Application.Responses;

namespace Shoppite.API.Controllers
{
   
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private int _maxIdCountAllowed = 800;
        private FCMClient _client;
        private readonly IMediator _mediator;
        public IConfiguration Configuration { get; }
        public NotificationsController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _client = new FCMClient(config.GetSection("Firebase")["FCM-ServerKey"]);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> SetUserFirebaseToken(FireBaseToken fireBaseToken)
        {
            return await _mediator.Send(new AddFirebaseToken(fireBaseToken));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UserRegistration([FromBody] CreateAuthCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> SendGeneralNotifications(string parameter1)
        {
            // parameter1 ~ NotificationId, parameter2 ~ userInfo
            string res = "success";
            try
            {
                int notifID = Convert.ToInt32(parameter1);
                List<NotificationsDataDTO> info = new List<NotificationsDataDTO>();
                info =await _mediator.Send(new GetNotificationDetails(notifID));
               
                foreach (var notifInfo in info)
                {
                    List<DeviceListDTO> devices = await _mediator.Send(new GetDeviceListToSendNotifications("General",0));
                    List<string> iosDevices = new List<string>(), androidDevices = new List<string>();
                    foreach (var d in devices)
                    {
                        if (d.IsIos) iosDevices.Add(d.Token);
                        else androidDevices.Add(d.Token);
                    }
                    bool iosOk = true, androidOk = true;
                    if (iosDevices.Count > 0)
                    { // iOS Send if there is at least 1 device  
                        // We have to run multiple if needed since max ids per payload is 1000. We set 800 just to be safe
                        int iosStartIndex = 0, iosIdsCount = 0;
                        int iosTotalRunCount = (int)(Math.Ceiling(iosDevices.Count * 1.0 / _maxIdCountAllowed));
                        int successCount = 0;
                        for (var i = 0; i < iosTotalRunCount; i++)
                        {
                            iosStartIndex = _maxIdCountAllowed * i;
                            iosIdsCount = (i == (iosTotalRunCount - 1)) ? (iosDevices.Count - iosStartIndex) : _maxIdCountAllowed;
                            var iosSubList = iosDevices.GetRange(iosStartIndex, iosIdsCount);
                            var message = new Message()
                            {
                                RegistrationIds = iosSubList,
                                Priority = MessagePriority.high,
                                Data = new Dictionary<string, string> {
                                    { "ID", notifID.ToString() },
                                    { "Title", notifInfo.Title },
                                    { "priority", "high" },
                                },
                                Notification = new IOSNotification()
                                {
                                    Body = notifInfo.Details,
                                    Title = notifInfo.Title,
                                    Sound = "default"
                                }
                            };
                            var result = await _client.SendMessageAsync(message);
                            long status = ((DownstreamMessageResponse)result).Failure;
                            if (status == 0) successCount++;
                        }
                        iosOk = (successCount == iosTotalRunCount);

                    }
                    if (androidDevices.Count > 0)
                    { // Android Send if there is at least 1 device
                        // We have to run multiple if needed since max ids per payload is 1000. We set 800 just to be safe
                        int androidStartIndex = 0, androidIdsCount = 0;
                        int androidTotalRunCount = (int)(Math.Ceiling(androidDevices.Count * 1.0 / _maxIdCountAllowed));
                        int successCount = 0;
                        for (var i = 0; i < androidTotalRunCount; i++)
                        {
                            androidStartIndex = _maxIdCountAllowed * i;
                            androidIdsCount = (i == (androidTotalRunCount - 1)) ? (androidDevices.Count - androidStartIndex) : _maxIdCountAllowed;
                            var androidSubList = androidDevices.GetRange(androidStartIndex, androidIdsCount);
                            var message = new Message()
                            {
                                RegistrationIds = androidSubList,
                                Priority = MessagePriority.high,
                                Data = new Dictionary<string, string> {
                                    { "ID", notifID.ToString() },
                                    { "Title", notifInfo.Title },
                                    { "priority", "high" }
                                },
                                Notification = new AndroidNotification()
                                {
                                    Body = notifInfo.Details,
                                    Title = notifInfo.Title,
                                    Sound = "default"
                                }
                            };
                            var result = await _client.SendMessageAsync(message);
                            long status = ((DownstreamMessageResponse)result).Failure;
                            if (status == 0) successCount++;
                        }
                        androidOk = (successCount == androidTotalRunCount);
                    }
                }
                await _mediator.Send(new UpdateNotificationStatus(notifID));
            }
            catch (Exception ex)
            {
               
            }
            return Ok(res);
        }
    }
}
