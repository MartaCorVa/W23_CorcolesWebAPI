using Dapper;
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
    [RoutePrefix("api/Online")]
    public class OnlineController : ApiController
    {
        [HttpPost]
        [Route("InsertOnline")]
        public IHttpActionResult InsertOnline(OnlineModel online)
        {
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = "INSERT INTO dbo.Online (IdPlayer, DateOnline) " +
                    $"VALUES ('{online.IdPlayer}','{DateTime.Now}')";

                try
                {
                    con.Execute(sql);
                }
                catch (Exception e)
                {
                    return BadRequest("Error insert online in database, " + e.Message);
                }
            }
            return Ok();
        }

        [HttpPost]
        [Route("DeleteOnline")]
        public IHttpActionResult DeleteOnline(OnlineModel online)
        {
            using (IDbConnection con = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"DELETE FROM dbo.Online WHERE IdPlayer = '{online.IdPlayer}'";

                try
                {
                    con.Execute(sql);
                }
                catch (Exception e)
                {
                    return BadRequest("Error delete online in database, " + e.Message);
                }
            }
            return Ok();
        }
    }
}
