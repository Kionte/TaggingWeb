using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaggingWebsite.scripts
{
    public partial class viewAllUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string lastRefresh = DateTime.Now.ToString();
            lastRefreshed.InnerText = "Last Refreshed: " + lastRefresh;
            string queryStr = "http://tagging.inlandpower.com/selectAllUsers.php";
            var request = HttpWebRequest.Create(queryStr);
            request.Method = WebRequestMethods.Http.Get;
            var response = request.GetResponse();
            StreamReader str = new StreamReader(response.GetResponseStream());
            string users = str.ReadToEnd();
            string[] userList = users.Split(new string[] { "(8#$" }, StringSplitOptions.None);
            TableHeaderRow thRow = new TableHeaderRow();
            TableHeaderCell[] thcell = new TableHeaderCell[5];
            for (int i = 0; i < thcell.Length; i++)
            {
                thcell[i] = new TableHeaderCell();
            }
            thcell[0].Text = "Username";
            thcell[1].Text = "Password";
            thcell[2].Text = "E-Mail";
            thcell[4].Text = "Edit";
            thcell[3].Text = "Status";
            for (int i = 0; i < thcell.Length; i++)
            {
                thRow.Cells.Add(thcell[i]);
            }
            myUserTable.Rows.Add(thRow);


            for (int i = 0; i < userList.Length - 1; i++) // goes through each row of the array 
            {

                TableRow trow = new TableRow();
                TableCell[] tcell = new TableCell[7];
                for (int j = 0; j < tcell.Length; j++)
                {
                    tcell[j] = new TableCell(); //init the table cells
                }
                //trow.Cells.Add(tcell[0]);

                if (GetDataValue(userList[i], "username ") != null) // double check to make sure the array hasinformaoiton 
                {
                    tcell[0].Text = GetDataValue(userList[i], "username ");//populate cells with data
                    tcell[1].Text = GetDataValue(userList[i], "password");
                    tcell[2].Text = GetDataValue(userList[i], "email");
                    tcell[4].Text = GetDataValue(userList[i], "user_id");
                    tcell[4].Visible = false;
                    tcell[5].Text = GetDataValue(userList[i], "hashPass");
                    tcell[5].Visible = false;
                    if(GetDataValue(userList[i],"employed") == "0")
                    {
                        tcell[3].Text = "Not Employed";
                    }
                    else if(GetDataValue(userList[i], "employed") == "1")
                    {
                        tcell[3].Text = "Employed";
                    }
                    else
                    {
                        tcell[3].Text = "No Status";
                    }

                }
                LinkButton editButton = new LinkButton();
                editButton.Text = "Edit";
                editButton.Click += new EventHandler(this.editFunc);
                tcell[6].Controls.Add(editButton);
                
                trow.Cells.Add(tcell[0]);
                trow.Cells.Add(tcell[1]);
                trow.Cells.Add(tcell[2]);
                trow.Cells.Add(tcell[3]);
                trow.Cells.Add(tcell[4]);
                trow.Cells.Add(tcell[5]);
                trow.Cells.Add(tcell[6]);
                myUserTable.Rows.Add(trow);
            }
        }

        /// <summary>
        /// send edited user to update the cdb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void editFunc(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            TableRow tr = (TableRow)b.Parent.Parent;

            Response.Redirect("editUser.aspx?Username=" + tr.Cells[0].Text + "&Password=" + tr.Cells[1].Text + "&Email=" + tr.Cells[2].Text + "&User_id=" + tr.Cells[4].Text + "&hashPass=" + tr.Cells[5].Text);
            
        }

        //parse values from php response
        public string GetDataValue(string data, string index)
        {
            string value = data.Substring(data.IndexOf(index) + index.Length);//split the different variables in tagging on a sequence of random characters
            if (value.Contains("|é| +"))
            {
                value = value.Remove(value.IndexOf("|é| +"));
            }
            return value;
        }
    }
}