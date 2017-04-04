using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaggingWebsite.scripts
{
    public partial class newUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// send information to central database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void createUser(object sender, EventArgs e)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("username", fillUsername.Value);
            parameters.Add("password", fillPassword.Value);
            parameters.Add("email", fillEmail.Value);
            parameters.Add("employed", "1");
            WebClient wc = new WebClient();
            string insertStr = "http://tagging.inlandpower.com/insertUser.php";
            byte[] by = wc.UploadValues(insertStr, parameters);
            string byt = Encoding.UTF8.GetString(by);
            Response.Redirect("viewAllUsers.aspx",false);
        }

        /// <summary>
        /// empty text fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void resetForm(object sender, EventArgs e)
        {
            fillUsername.Value = "";
            fillPassword.Value = "";
            fillEmail.Value = "";
        }
    }
}