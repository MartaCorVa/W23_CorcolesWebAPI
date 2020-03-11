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
    [RoutePrefix("api/Player")]
    public class PlayerController : ApiController
    {
        [HttpPost]
        [Route("InsertNewPlayer")]
        public IHttpActionResult InsertNewPlayer(PlayerModel player)
        {
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "INSERT INTO dbo.Player (Id, Name, Email, BirthDay, State) " +
                    $"VALUES ('{player.Id}','{player.Name}','{player.Email}','{player.BirthDay}', 'correct')";

                try
                {
                    con.Execute(sql);

                }
                catch (Exception e)
                {
                    return BadRequest("Error inserting player in database, " + e.Message);
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("Info")]
        public PlayerModel GetPlayerInfo()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT Id, Name, Email, BirthDay, State FROM dbo.Player " +
                    $"WHERE Id LIKE '{authenticatedAspNetUserId}'";
                var player = cnn.Query<PlayerModel>(sql).FirstOrDefault();
                return player;
            }
        }

        [HttpGet]
        [Route("BanPlayer/{id}")]
        public IHttpActionResult BanPlayer(string id)
        {
            PlayerModel player;
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT * FROM dbo.Player WHERE Id LIKE '{id}%'";

                try
                {
                    player = con.Query<PlayerModel>(sql).FirstOrDefault();

                }
                catch (Exception e)
                {
                    throw new Exception("Error inserting player in database, " + e.Message);
                }

                string query = $"UPDATE dbo.Player SET State = 'banned' WHERE Id = '{player.Id}'";
                try
                {
                    con.Execute(query);

                }
                catch (Exception e)
                {
                    throw new Exception("Error inserting player in database, " + e.Message);
                }
            }
            return Ok();
        }

    }
}
