using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//--------------------------------------
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAPI_JWT_.Controllers
{
    public class TokenController : ApiController
    {
        // 自定义秘钥
        // jwt 的生成和解析都需要使用
        const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        [HttpGet]
        [Route("api/token/{username}/{password}")]
        public IHttpActionResult Get(string username,string password)
        {
            bool isAuth = Authenticate.Instance.ValidateUser(username, password);
            string token = "";
            if (isAuth)
            {
                token = GenerateToken(username);
                return Ok(token);
            }
            else
            {
                return BadRequest("帳號或密碼錯誤");
            }
        }

        [HttpPost]
        [Route("api/token")]
        //一般型別
        public IHttpActionResult Post([FromBody]string token)
        {
            var decode = DecodeToken(token);
            JObject jo = (JObject)JsonConvert.DeserializeObject(decode);
            return Ok(jo);//return Json(jo);
        }

        //[HttpPost]
        //[Route("api/token")]
        ////複雜型別
        //public IHttpActionResult Post([FromBody]TokenClass t)
        //{
        //    var decode = DecodeToken(t.Token);
        //    JObject jo = (JObject)JsonConvert.DeserializeObject(decode);
        //    return Ok(jo);//return Json(jo);
        //}


        private string GenerateToken(string username)
        {
            // 資料 Payload
            //var payload = new Dictionary<string, object>
            //{
            //    { "username", username }
            //};
            // 使用 JwtBuilder 来生成 token
            string token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm()) // 使用算法
                .WithSecret(secret) // 使用秘钥
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                .AddClaim("claim2", "claim2-value")
                .AddClaim("username", username)
                .Build();

            return token;
        }

        private string DecodeToken(string token)
        {
            try
            {
                string json = new JwtBuilder()
                    .WithSecret(secret)
                    .MustVerifySignature()
                    .Decode(token);
                return json;
            }
            catch (TokenExpiredException)
            {
                return "{}";
            }
            catch (SignatureVerificationException)
            {
                return "{}";
            }

        }
    }

    public class TokenClass
    {
        public string Token { get; set; }
    }
}
