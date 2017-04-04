using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace TaggingWebsite.scripts
{
    public partial class newTag : System.Web.UI.Page
    {
        
        public static int virTa = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tagNumText.InnerText = DateTime.Now.ToString("yyMMddhhmmssff");
                dateNewTag.InnerText = DateTime.Now.ToString("MM/dd/yyyy");
            }
            string queryStr = "http://tagging.inlandpower.com/selectAllUsers.php";
            var request = HttpWebRequest.Create(queryStr);
            request.Method = WebRequestMethods.Http.Get;
            var response = request.GetResponse();
            StreamReader str = new StreamReader(response.GetResponseStream());
            string users = str.ReadToEnd();
            string[] userList = users.Split(new string[] { "(8#$" }, StringSplitOptions.None);


            for(int i = 0; i<userList.Length-1; i++)
            {
                string username = GetDataValue(userList[i], "username");
                dropdownlist.Items.Insert(i, new ListItem(username));
            }

            dropdownlist.Items.Insert(0, new ListItem("Please Select"));
        }

        public string GetDataValue(string data, string index)
        {
            string value = data.Substring(data.IndexOf(index) + index.Length);//split the different variables in tagging on a sequence of random characters
            if (value.Contains("|é| +"))
            {
                value = value.Remove(value.IndexOf("|é| +"));
            }
            return value;
        }

        /// <summary>
        /// on submit send all info from the text boxes to the cdb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void submitOnClick(object sender, EventArgs e)
        {

            WebClient client = new WebClient();

            Uri insertStr = new Uri("http://tagging.inlandpower.com/insertTag.php");
            UriBuilder build = new UriBuilder("https", "tagging.inlandpower.com/insertTag.php");

            //changed ip to scotts computer. also hacked yours and stole opentags.php
            {
                NameValueCollection parameters = new NameValueCollection();//give values to paramaters that we will pass to php
                parameters.Add("TagNum", tagNumText.InnerText);
                parameters.Add("Date", dateNewTag.InnerText);
                if (clearCheck.Checked)
                {
                    Debug.WriteLine(clearCheck.Checked);
                    //  myCommand.Parameters.AddWithValue("@Type", "Clearance");
                    parameters.Add("Type", "Clearance");
                }
                else if (cautionCheck.Checked)
                {
                    Debug.WriteLine(cautionCheck.Checked);
                    //    myCommand.Parameters.AddWithValue("@Type", "Caution");
                    parameters.Add("Type", "Caution");

                }
                else if (holdCheck.Checked)
                {
                    Debug.WriteLine(holdCheck.Checked);
                    //  myCommand.Parameters.AddWithValue("@Type", "Hold");
                    parameters.Add("Type", "Hold");

                }
                parameters.Add("RequestedBy", RequestedByTextNew.Value);
                parameters.Add("TruckNum", truckTextNew.Value);
                parameters.Add("RequestedFor", requestedForTextNew.Value);
                parameters.Add("Purpose", purposeDropDown.Value);
                parameters.Add("Equipment", equipmentDropDown.Value);
                parameters.Add("Pole1", pole1Text.Value);
                parameters.Add("Pole2", pole2Text.Value);
                parameters.Add("Comments", commentsText.Value);
                if (noneCheck.Checked)
                {
                    Debug.WriteLine(noneCheck.Checked);
                    //myCommand.Parameters.AddWithValue("@Notifications", "None");
                    parameters.Add("Notifications", "None");
                }
                else if (bpaCheck.Checked)
                {
                    Debug.WriteLine(bpaCheck.Checked);
                    parameters.Add("Notifications", "BPA (MCC Operator");

                    //myCommand.Parameters.AddWithValue("@Notifications", "BPA (MCC Operator)");
                }
                else if (avistaCheck.Checked)
                {
                    Debug.WriteLine(avistaCheck.Checked);
                    parameters.Add("Notifications", "Avista (Operator)");

                    //myCommand.Parameters.AddWithValue("@Notifications", "Avista (Operator)");
                }
                else if (popudCheck.Checked)
                {
                    Debug.WriteLine(popudCheck.Checked);
                    parameters.Add("Notifications", "POPUD (Dispatch)");

                    //myCommand.Parameters.AddWithValue("@Notifications", "POPUD (Dispatch)");
                }
                else if (grantCheck.Checked)
                {
                    Debug.WriteLine(grantCheck.Checked);
                    parameters.Add("Notifications", "Grant Co. PUD (Dispatch)");
                    //myCommand.Parameters.AddWithValue("@Notifications", "Grant Co. PUD (Dispatch)");
                }
                parameters.Add("isClosed", "0");
                parameters.Add("VirtTagNum", virTa.ToString());
                virTa--;
                parameters.Add("hasChanged", "0");
                parameters.Add("Lat", "0");
                parameters.Add("Lon", "0");
                parameters.Add("User", dropdownlist.SelectedValue);
                client.UploadValues(insertStr, "POST", parameters);    //upload the values to the given php script
                                                                       //  conn.Close();

               

                //client.UploadValuesCompleted += client_UploadValuesCompleted;
                try
                {

                    client.UploadValuesAsync(insertStr, "POST", parameters);    //upload the values to the php script with post so thearent in url



                }
                catch (Exception)
                {

                }


                
                Response.Redirect("index.aspx", false);

                //catch(Exception ex)
                {
                   // Debug.Write(ex.ToString());
                }
            }

        }


        /// <summary>
        /// empty the text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void resetForm(object sender, EventArgs e)
        {
            cautionCheck.Checked = false;
            holdCheck.Checked = false;
            clearCheck.Checked = true;
            IssuedByTextNew.Value = "";
            RequestedByTextNew.Value = "";
            truckTextNew.Value = "";
            requestedForTextNew.Value = "";
            purposeDropDown.Value = "Please Select";
            equipmentDropDown.Value = "Please Select";
            pole1Text.Value = "";
            pole2Text.Value = "";
            bpaCheck.Checked = false;
            avistaCheck.Checked = false;
            popudCheck.Checked = false;
            grantCheck.Checked = false;
            noneCheck.Checked = true;
            commentsText.Value = "";
        }
    }
}