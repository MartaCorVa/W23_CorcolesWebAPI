using Dapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using W23_CorcolesWebAPI.Models;

namespace W23_CorcolesWebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
        [HttpPost]
        [Route("InsertNewMessage")]
        public IHttpActionResult InsertNewMessage(MessageModel message)
        {
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "INSERT INTO dbo.Message (IdPlayer, Content, DateMessage) " +
                    $"VALUES ('{message.IdPlayer}','{message.Content}','{DateTime.Now}')";

                try
                {
                    con.Execute(sql);
                }
                catch (Exception e)
                {
                    return BadRequest("Error inserting message in database, " + e.Message);
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetMessages")]
        public List<MessageModel> GetMessages()
        {
            List<MessageModel> messages;
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "SELECT * FROM dbo.Message WHERE " +
                    $"DateMessage >= (SELECT DateOnline FROM dbo.Online WHERE IdPlayer = '{authenticatedAspNetUserId}')";

                try
                {
                    messages = con.Query<MessageModel>(sql).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("Error inserting message in database, " + e.Message);
                }
            }
            return messages;
        }
    }
}
