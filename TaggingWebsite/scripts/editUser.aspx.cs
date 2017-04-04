using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaggingWebsite.scripts
{
    public partial class editUser : System.Web.UI.Page
    {
        string user_id;
        string hashPass;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["Username"] != null)
                {
                    fillUsername.Value = Request.QueryString["Username"];
                    fillPassword.Value = Request.QueryString["Password"];
                    fillEmail.Value = Request.QueryString["Email"];
                    user_id = Request.QueryString["User_id"];
                    hashPass = Request.QueryString["hashPass"];
                }
            }
        }
        /// <summary>
        /// empty the text boxes on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void resetForm(object sender, EventArgs e)
        {
            fillUsername.Value = "";
            fillPassword.Value = "";
            fillEmail.Value = "";
        }

        /// <summary>
        /// send updated info to cdb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updateUser(object sender, EventArgs e)
        {
            NameValueCollection parameters = new NameValueCollection();//give values to paramaters that we will pass to php
            user_id = Request.QueryString["User_id"];
            parameters.Add("Username", fillUsername.Value);
            parameters.Add("Password", fillPassword.Value);
            parameters.Add("hashPass", fillPassword.Value);
            parameters.Add("Email", fillEmail.Value);
            parameters.Add("User_id", user_id);
            WebClient client = new WebClient();
            Uri insertStr = new Uri("http://tagging.inlandpower.com/updateUser.php");
            byte[] by = client.UploadValues(insertStr, parameters);
            string byt = Encoding.UTF8.GetString(by);
            Response.Redirect("viewAllUsers.aspx", false);
        }

        /// <summary>
        /// delete the user with the username
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void deleteUser(object sender, EventArgs e)
        {
            user_id = Request.QueryString["User_id"];
            WebClient client = new WebClient();
            Uri insertStr = new Uri("http://tagging.inlandpower.com/deleteUser.php");
            NameValueCollection parameters = new NameValueCollection();//give values to paramaters that we will pass to php
            parameters.Add("Username", fillUsername.Value);
            parameters.Add("Password", fillPassword.Value);
            parameters.Add("Email", fillEmail.Value);
            parameters.Add("User_id", user_id);
            parameters.Add("Employed", "1");
            Debug.WriteLine("~" + user_id);
            byte[] by = client.UploadValues(insertStr, "POST", parameters);
            string byt = Encoding.UTF8.GetString(by);
            Debug.WriteLine(Encoding.UTF8.GetString(by));
            Response.Redirect("viewAllUsers.aspx", false);
        }
            
    }
}