using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Threading;
using System.Collections.Specialized;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace TaggingWebsite.scripts
{
    public partial class view : System.Web.UI.Page
    {
        private string isClo;
        private string haCha;
        private string virTa;
        private string lat;
        private string lo;
        private string[] openTagList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //MySqlConnection conn = new MySqlConnection("Server=192.168.1.33;Uid=testUser;Database=test;Password=t@gg!NGapp");
                try
                {
                    start();//parse response from php script
                    int i = 0;
                    for(i=0; i<openTagList.Length - 1; i++)
                    {
                    //    Debug.Write("\n\n\n\n\n\n" + GetDataValue(openTagList[i], "tagNum ") + " = " + Request.QueryString["TagNum"].ToString());
                        if(GetDataValue(openTagList[i], "tagNum ") == Request.QueryString["TagNum"].ToString().Trim())
                        {
                            break;
                        }
                    }
                        tagNumText.InnerText = GetDataValue(openTagList[i],"tagNum ");
                        dateTag.InnerText = GetDataValue(openTagList[i], "date");
                        pole1.Value = GetDataValue(openTagList[i], "pole1");
                        pole2.Value = GetDataValue(openTagList[i], "pole2");
                        RequestedByText.Value = GetDataValue(openTagList[i], "requestedBy");
                        truckText.Value = GetDataValue(openTagList[i], "truckNum");
                        requestedForText.Value = GetDataValue(openTagList[i], "requestedFor");
                        purposeDrop.Value = GetDataValue(openTagList[i], "purpose");
                        equipmentDrop.Value = GetDataValue(openTagList[i], "equipment");
                        commentsBox.Value = GetDataValue(openTagList[i], "comment");
                        isClo = GetDataValue(openTagList[i], "isClosed").ToString();
                        haCha = GetDataValue(openTagList[i], "hasChanged").ToString();
                        lat = GetDataValue(openTagList[i], "lat").ToString();
                        lo = GetDataValue(openTagList[i], "lon").ToString();
                        virTa = GetDataValue(openTagList[i], "virtTagNum").ToString();
                        if (Request.QueryString["type"] == "Clearance")
                        {
                            cautionCheck.Checked = false;
                            holdCheck.Checked = false;
                            clearCheck.Checked = true;
                        }
                        else if (Request.QueryString["type"] == "Caution")
                        {
                            holdCheck.Checked = false;
                            clearCheck.Checked = false;
                            cautionCheck.Checked = true;
                        }
                        else
                        {
                            clearCheck.Checked = false;
                            cautionCheck.Checked = false;
                            holdCheck.Checked = true;
                        }
                        string s = GetDataValue(openTagList[0], "Notifications");
                        switch (s)
                        {
                            case "None":
                                {
                                    bpaCheckBox.Checked = false;
                                    avistaCheckBox.Checked = false;
                                    popudCheckBox.Checked = false;
                                    grantCheckBox.Checked = false;
                                    noneCheckBox.Checked = true;
                                    break;
                                }
                            case "BPA (MCC Operator)":
                                {
                                    avistaCheckBox.Checked = false;
                                    popudCheckBox.Checked = false;
                                    grantCheckBox.Checked = false;
                                    noneCheckBox.Checked = false;
                                    bpaCheckBox.Checked = true;
                                    break;
                                }
                            case "Avista (Operator)":
                                {
                                    popudCheckBox.Checked = false;
                                    grantCheckBox.Checked = false;
                                    noneCheckBox.Checked = false;
                                    bpaCheckBox.Checked = false;
                                    avistaCheckBox.Checked = true;
                                    break;
                                }
                            case "POPUD (Dispatch)":
                                {

                                    grantCheckBox.Checked = false;
                                    noneCheckBox.Checked = false;
                                    bpaCheckBox.Checked = false;
                                    avistaCheckBox.Checked = false;
                                    popudCheckBox.Checked = true;
                                    break;
                                }
                            case "Grant Co. PUD (Dispatch)":
                                {

                                    grantCheckBox.Checked = false;
                                    noneCheckBox.Checked = false;
                                    bpaCheckBox.Checked = false;
                                    avistaCheckBox.Checked = false;
                                    popudCheckBox.Checked = false;
                                    grantCheckBox.Checked = true;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                   

                    // conn.Close();
                    //}
                }
                catch (Exception ex)
                {

                }

            }
            string queryStr = "http://tagging.inlandpower.com/selectAllUsers.php";
            var request = HttpWebRequest.Create(queryStr);
            request.Method = WebRequestMethods.Http.Get;
            var response = request.GetResponse();
            StreamReader str = new StreamReader(response.GetResponseStream());
            string users = str.ReadToEnd();
            string[] userList = users.Split(new string[] { "(8#$" }, StringSplitOptions.None);

            
            for (int j = 0; j < userList.Length - 1; j++)
            {
                string username = GetDataValue(userList[j], "username");
                dropdownlist.Items.Insert(j, new ListItem(username));
            }
            
            dropdownlist.Items.Insert(0, new ListItem(Request.QueryString["User"]));
        }

        /// <summary>
        /// submit the edited tag to the central database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void editSubmit(object sender, EventArgs e)
        {
           // MySqlConnection conn = new MySqlConnection("Server=192.168.1.33;Uid=testUser;Database=test;Password=t@gg!NGapp");
            try
            {
                virTa = Request.QueryString["VirtTagNum"];

                WebClient client = new WebClient();
                Uri insertStr = new Uri("http://tagging.inlandpower.com/updateTag.php");
                NameValueCollection parameters = new NameValueCollection();//give values to paramaters that we will pass to php
                parameters.Add("TagNum", tagNumText.InnerText);
                parameters.Add("Date", dateTag.InnerText);
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
                parameters.Add("RequestedBy", RequestedByText.Value);
                parameters.Add("TruckNum", truckText.Value);
                parameters.Add("RequestedFor", requestedForText.Value);
                parameters.Add("Purpose", purposeDrop.Value);
                parameters.Add("Equipment", equipmentDrop.Value);
                parameters.Add("Pole1", pole1.Value);
                parameters.Add("Pole2", pole2.Value);
                parameters.Add("Comments", commentsBox.Value);
                if (noneCheckBox.Checked)
                {
                    Debug.WriteLine(noneCheckBox.Checked);
                    //myCommand.Parameters.AddWithValue("@Notifications", "None");
                    parameters.Add("Notifications", "None");
                }
                else if (bpaCheckBox.Checked)
                {
                    Debug.WriteLine(bpaCheckBox.Checked);
                    parameters.Add("Notifications", "BPA (MCC Operator");

                    //myCommand.Parameters.AddWithValue("@Notifications", "BPA (MCC Operator)");
                }
                else if (avistaCheckBox.Checked)
                {
                    Debug.WriteLine(avistaCheckBox.Checked);
                    parameters.Add("Notifications", "Avista (Operator)");

                    //myCommand.Parameters.AddWithValue("@Notifications", "Avista (Operator)");
                }
                else if (popudCheckBox.Checked)
                {
                    Debug.WriteLine(popudCheckBox.Checked);
                    parameters.Add("Notifications", "POPUD (Dispatch)");

                    //myCommand.Parameters.AddWithValue("@Notifications", "POPUD (Dispatch)");
                }
                else if (grantCheckBox.Checked)
                {
                    Debug.WriteLine(grantCheckBox.Checked);
                    parameters.Add("Notifications", "Grant Co. PUD (Dispatch)");
                    //myCommand.Parameters.AddWithValue("@Notifications", "Grant Co. PUD (Dispatch)");
                }

                parameters.Add("isClosed", isClo);

                parameters.Add("VirtTagNum", virTa);
                parameters.Add("hasChanged", "1");
                parameters.Add("Lat", lat);
                parameters.Add("Lon", lo);
                parameters.Add("User", dropdownlist.SelectedValue);
                client.UploadValues(insertStr, "POST", parameters);    //upload the values to the given php script
                //  conn.Close();

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            Response.Redirect("index.aspx", false);
            //Context.ApplicationInstance.CompleteRequest();
        }




        /// <summary>
        /// empty text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void resetFunc(object sender, EventArgs e)
        {
            tagNumText.InnerText = "";
            dateTag.InnerText = "";
            pole1.Value = "";
            pole2.Value = "";
            RequestedByText.Value = "";
            truckText.Value = "";
            requestedForText.Value = "";
            purposeDrop.Value = "";
            equipmentDrop.Value ="";
            commentsBox.Value ="";
            clearCheck.Checked = true;
            noneCheckBox.Checked = true;
        }

        /// <summary>
        /// a function designed to help parse data from php response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetDataValue(string data, string index)
        {
            string value = data.Substring(data.IndexOf(index) + index.Length);//split the different variables in tagging on a sequence of random characters
            if (value.Contains("|é| +"))
            {
                value = value.Remove(value.IndexOf("|é| +"));
            }
            return value;
        }



        string queryStr;

        /// <summary>
        /// 
        /// begin the parsing process
        /// </summary>
        public void start()
        {
            Regex r = new Regex("(.)"); // it will retrieve everything in the php script
            //if(com,ing from open)
            queryStr = string.Format("http://tagging.inlandpower.com/openTags.php");    //changed ip to scotts computer. also hacked yours and stole opentags.php
            //else{ do that }
            if (Request.QueryString["View"] != null)
            {
                 queryStr = string.Format("http://tagging.inlandpower.com/closedTags.php");
                editSubmitSubmit.Visible = false;
            }
            var request = System.Net.HttpWebRequest.Create(queryStr); //creates an http webrequest for the url above

            request.Method = System.Net.WebRequestMethods.Http.Get; //sets the method to get rather than post. i wonder if it works as post??

            var response = request.GetResponse(); //get the response from the page

            System.IO.StreamReader str = new System.IO.StreamReader(response.GetResponseStream()); //convert response into something we can work with

            string tagToString = str.ReadToEnd(); // convert database to string

            Match mtag = r.Match(tagToString); // uses the regex to parse the db( in this case it gets everything)
            if (mtag.Success) // if the parsing succeeds
            { // if the review data is compatabl with regex then init review list to data

                openTagList = tagToString.Split(new string[] { "~`^Y" }, StringSplitOptions.None); // this puts the the string in the array seperating each part of the array by a sequence that should not be typed
            }
            //transferDataTag(false); // moves data from tag list array to the to a actual tag object. 
            //eturn TaggingList.getItems();//tried returning just to debug. right now this doesnt do much at all.
        }
    }
}



