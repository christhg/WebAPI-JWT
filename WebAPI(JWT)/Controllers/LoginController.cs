using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//
using System.DirectoryServices;
using System.Net.Sockets;

namespace WebAPI_JWT_.Controllers
{
    public class LoginController : ApiController
    {

        [HttpGet]
        [Route("api/login/{username}/{password}")]
        public IHttpActionResult Get(string username,string password)
        {
            bool result = Authenticate.Instance.ValidateUser(username, password);
            if (result)
                return Ok();
            else
                return BadRequest();
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }

    ///// <summary>
    ///// SSOSite認證類:提供認證方法
    ///// </summary>
    //public class Authenticate
    //{
    //    /// <summary>
    //    /// LDAP 基本資料
    //    /// </summary>
    //    private string ltpatoken_name = ConfigurationManager.AppSettings["DOMINO_LTPATOKEN"];        //0.設定LtpaToken Cookie Name
    //    private string cookie_domain = ConfigurationManager.AppSettings["COOKIE_DOMAIN"];            //1.設定Cookie Domain
    //    private string ldapserver; // = ConfigurationManager.AppSettings["LDAP_SERVER"];             // 2.設定LDAP服務器
    //    private string ldapserver1 = ConfigurationManager.AppSettings["LDAP_SERVER1"];              // 2.設定LDAP服務器
    //    private string ldapserver2 = ConfigurationManager.AppSettings["LDAP_SERVER2"];              // 2.設定LDAP服務器
    //    private string ldapport = ConfigurationManager.AppSettings["LDAP_PORT"];                    // 3.設定LDAP服務器
    //    private bool isAlive = true;                                                                // Master,Slave Ldap Server is Alive or Not.
    //    private string form_auth_timeout = ConfigurationManager.AppSettings["FORM_AUTH_TIMEOUT"];   // 4.設定表單逾期時間:分鐘 
    //    private List<string> ldapbasedn = new List<string>() { "o=gbm", "o=walsin", "o=hsb" };      // 5.設定認證OU
    //    private string ad_ldapbasedn = "CN=Users,DC=deer-group,DC=com,DC=cn";
    //    private string ad_domain = ConfigurationManager.AppSettings["AD_DOMAIN"];
    //    private AuthType auth_type = AuthType.AD; //採用AD認證


    //    /// <summary>
    //    /// 實例化對象
    //    /// </summary>
    //    public static Authenticate Instance = new Authenticate();

    //    /// <summary>
    //    /// 構造函數:初始化,驗證LDAP是否Alive
    //    /// </summary>
    //    public Authenticate()
    //    {
    //        if (isLdapServerConnected(ldapserver1, null))
    //        {
    //            ldapserver = ldapserver1;
    //            isAlive = true;
    //        }
    //        else if (isLdapServerConnected(ldapserver2, null))
    //        {
    //            ldapserver = ldapserver2;
    //            isAlive = true;
    //        }
    //        else
    //        {
    //            //isAlive = false;
    //            isAlive = false;
    //        }
    //    }


    //    /// <summary>
    //    /// 驗證LDAP帳密是否正確,成功返回true,out:nonce_id,uid,pub_company_id
    //    /// </summary>
    //    /// <param name="username"></param>
    //    /// <param name="password"></param>
    //    /// <param name="nonce_id">一次性id</param>
    //    /// <param name="uid">用戶代號</param>
    //    /// <param name="pub_company_id">公司別代號</param>
    //    /// <returns></returns>
    //    public bool ValidateUser(string username, string password)
    //    {
    //        return isAuthenticatedAD(ldapserver, username, password);
    //    }

    //    /// <summary>
    //    /// 驗證AD用戶帳密,成功返回true.
    //    /// </summary>
    //    /// <param name="ldapserver"></param>
    //    /// <param name="username"></param>
    //    /// <param name="pwd"></param>
    //    /// <returns></returns>
    //    private bool isAuthenticatedAD(string ldapserver, string username, string pwd)
    //    {
    //        //string path = "LDAP://192.168.201.2:389/cn=Users,dc=deer-group,dc=com,dc=cn";
    //        string path = "LDAP://" + ldapserver + "/" + ad_ldapbasedn;
    //        //string username = "sengyi";
    //        //pwd = "seng1234";
    //        //string username = userdn;
    //        string domain = ad_domain;
    //        string domainAndUsername = domain + @"\" + username;
    //        bool authenticated = false;
    //        DirectoryEntry entry = new DirectoryEntry(path, username, pwd);

    //        try
    //        {
    //            //Bind to the native AdsObject to force authentication.
    //            object obj = entry.NativeObject;

    //            DirectorySearcher search = new DirectorySearcher(entry);

    //            search.Filter = "(SAMAccountName=" + username + ")";
    //            search.PropertiesToLoad.Add("cn");
    //            SearchResult result = search.FindOne();

    //            if (null == result)
    //            {
    //                authenticated = false;
    //            }
    //            else
    //            {
    //                authenticated = true;
    //            }

    //            //Update the new path to the user in the directory.
    //            //_path = result.Path;
    //            //_filterAttribute = (string)result.Properties["cn"][0];
    //        }
    //        catch (Exception ex)
    //        {
    //            //throw new Exception("Error authenticating user. " + ex.Message);
    //            authenticated = false;
    //        }

    //        return authenticated;
    //    }

    //    /// <summary>
    //    /// 驗證LDAP Server 是否 Alive
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool isLdapServerConnected(string serverip, string ldapport)
    //    {
    //        //bool result = Authenticate.Instance.isLdapServerConnected();
    //        bool validated = false;
    //        int port;
    //        if (string.IsNullOrEmpty(ldapport))
    //        {
    //            port = 389;
    //        }
    //        else
    //        {
    //            int.TryParse(ldapport, out port);
    //        }

    //        var client = new TcpClient();
    //        try
    //        {
    //            var result = client.BeginConnect(serverip, port, null, null);

    //            result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2)); //wait 2 second
    //            if (client.Connected)
    //            {
    //                //throw new Exception("Failed to connect.");
    //                validated = true;
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            validated = false;
    //        }
    //        finally
    //        {
    //            // we have connected
    //            //client.EndConnect(result);
    //            client.Close();
    //        }



    //        return validated;
    //    }

    //}

    ///// <summary>
    ///// SSO認證類型:LDAP or AD
    ///// </summary>
    //public enum AuthType
    //{
    //    /// <summary>
    //    /// 認證for LDAP / Notes
    //    /// </summary>
    //    LDAP,
    //    /// <summary>
    //    /// 認證 for Microsoft AD
    //    /// </summary>
    //    AD
    //}
}

