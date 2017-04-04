using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Net;

namespace TaggingWebsite.scripts
{
    public partial class index : System.Web.UI.Page
    {
        
        string isAsc = "false";//sort ascending or descending
        int i;  
        private string[] tagList; //tags after pull down and split

        /// <summary>
        /// on page load perform following
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string lastRefresh = DateTime.Now.ToString();
            lastRefreshed.InnerText = "Last Refreshed: " + lastRefresh;
            if (Request.QueryString["bool"] != null)//if we are sorting
                if(Request.QueryString["bool"] == "true")//if we need to sort by descending
                {
                    isAsc = "false";
                }
                else
                {
                    isAsc = "true";
                }
                
            {
                try
                {
                    TableHeaderRow header = new TableHeaderRow();
                    TableHeaderCell[] cells = new TableHeaderCell[8];
                    for(int i = 0; i<cells.Length;i++)
                    {
                        cells[i] = new TableHeaderCell();
                    }
                    cells[0].Text = "Tag Num";
                    cells[1].Text = "Date";
                    cells[2].Text = "Pole 1";
                    cells[3].Text = "Pole2";
                    cells[4].Text = "Requested By";
                    cells[5].Text = "Type";
                    cells[6].Text = "Close Tag";
                    cells[7].Text = "View/Edit";

                    string[] sortArray = {//set the array for sorting  that is strings that redirect to the same page with sorting info in query string
                        "<a href=\"index.aspx?orderBy=TagNum&bool=" +isAsc+"\">TagNum</a>",
                        "<a href=\"index.aspx?orderBy=Date&bool=" + isAsc + "\">Date</a>",
                        "<a href=\"index.aspx?orderBy=Pole1&bool=" + isAsc + "\">Pole 1</a>",
                        "<a href=\"index.aspx?orderBy=Pole2&bool=" + isAsc + "\">Pole 2</a>",
                        "<a href=\"index.aspx?orderBy=RequestedBy&bool=" + isAsc + "\">Requested By</a>",
                        "<a href=\"index.aspx?orderBy=Type&bool=" + isAsc + "\">Type</a>" };

                    for (int i = 0; i < cells.Length; i++)
                    {
                        if(i < sortArray.Length)
                            cells[i].Controls.Add(new LiteralControl(sortArray[i]));    //add the links to the header columns
                        header.Cells.Add(cells[i]);
                        
                    }
                    myTable.Rows.Add(header);
                    start();
                    for (i = 0; i < tagList.Length - 1; i++) // goes through each row of the array 
                    {
                        
                        TableRow trow = new TableRow();
                        TableCell[] tcell = new TableCell[8];
                        for(int j = 0; j<tcell.Length;j++)
                        {
                            tcell[j] = new TableCell(); //init the table cells
                        }
                        
                        if (GetDataValue(tagList[i], "tagNum ") != null) // double check to make sure the array hasinformaoiton 
                        {
                            /**
                             * all this will do is get the correct daqta from the array and set that to the correct attribute in the tag object 
                             * */
                            tcell[0].Text = GetDataValue(tagList[i], "tagNum ");//populate cells with data
                            tcell[1].Text = GetDataValue(tagList[i], "date");
                            tcell[2].Text = GetDataValue(tagList[i], "pole1");
                            tcell[3].Text = GetDataValue(tagList[i], "pole2");
                            tcell[4].Text = GetDataValue(tagList[i], "requestedBy");
                            tcell[5].Text = GetDataValue(tagList[i], "type");
                        }

                          
                        LinkButton closeButton = new LinkButton(); //close tag button/link
                        myTable.BorderWidth = 10;
                        myTable.BorderColor = System.Drawing.Color.Crimson;
                        myTable.BorderStyle = BorderStyle.Groove;
                        closeButton.Text = "Close Tag";
                        closeButton.Enabled = true;
                        closeButton.Click += new System.EventHandler(this.closeTag);

                        LinkButton editButton = new LinkButton();   //view edit button/link
                        editButton.ToolTip.PadRight(15);
                        editButton.Text = "View/Edit";
                        editButton.Enabled = true;
                        editButton.Click += new System.EventHandler(this.editFunc);
                        
                        tcell[6].Controls.Add(closeButton);
                        tcell[7].Controls.Add(editButton);
                        trow.Cells.Add(tcell[0]); // addign the tcells from above to the trow array 
                        trow.Cells.Add(tcell[1]);
                        trow.Cells.Add(tcell[2]);
                        trow.Cells.Add(tcell[3]);
                        trow.Cells.Add(tcell[4]);
                        trow.Cells.Add(tcell[5]);
                        trow.Cells.Add(tcell[6]);
                        trow.Cells.Add(tcell[7]);
                        myTable.Rows.Add(trow);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("\n\n\n\n\n\n\n" +ex.ToString());
                }


                //Populating a DataTable from database.
               
            }
        }
        /// <summary>
        /// helper for parsing response from php webrequests
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
        /// <summary>
        /// convert string to bool
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool getBool(string s)
        {
            if (s == "0")    //converting strings to booleans
                return false;

            return true;
        }
        /// <summary>
        /// send the tag information to the view page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void editFunc(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            TableRow tr = (TableRow)b.Parent.Parent;
            Table t = (Table)tr.Parent;

            for(i = 0; i < t.Rows.Count; i++)
            {
                string s1 = t.Rows[i].Cells[0].Text;
                string s2 = tr.Cells[0].Text;
                
                if (t.Rows[i].Cells[0].Text == tr.Cells[0].Text)
                {
                    break;
                }
            }

            
            // filling in the view page 
            Response.Redirect("view.aspx?TagNum="+tr.Cells[0].Text
                                +"&Date=" + tr.Cells[1].Text
                                +"&Pole1=" + tr.Cells[2].Text
                                +"&Pole2=" +tr.Cells[3].Text
                                +"&requested="+ tr.Cells[4].Text
                                +"&type=" + tr.Cells[5].Text 
                                +"&Notifications=" + tr.Cells[6].Text
                                +"&User="+GetDataValue(tagList[i-1],"user")
                                +"&VirtTagNum=" +GetDataValue(tagList[i-1],"virtTagNum"));
        }
       
        /// <summary>
        /// close a tag that was clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void closeTag(Object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            Response.Write(b.Parent.Parent);
            TableRow tr = (TableRow)b.Parent.Parent;
            WebClient client = new WebClient();

            Uri insertStr = new Uri("http://tagging.inlandpower.com/closeATag.php");

            NameValueCollection parameters = new NameValueCollection();//give values to paramaters that we will pass to php
            parameters.Add("TagNum", tr.Cells[0].Text);
            parameters.Add("isClosed", "1");
            parameters.Add("hasChanged", "1");

            try
            {
                client.UploadValues(insertStr, "POST", parameters);
            }
            catch(Exception ee)
            {

            }
            Response.Redirect("index.aspx"); // clean the page so that it wont delte multiple times 

        }
        /// <summary>
        /// move to new tag page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void newTag(object sender,EventArgs e)
        {
            Response.Redirect("newTag.aspx");
        }

        string queryStr;
        /// <summary>
        /// begginning of the parsing actions
        /// </summary>
        public void start()
        {
            Regex r = new Regex("(.)"); // it will retrieve everything in the php script

            if(Request.QueryString["orderBy"] != null) // if we are sorting 
            {
                if(isAsc == "true")// if we are sorting by ascending 
                {
                    queryStr = string.Format("http://tagging.inlandpower.com/openTags.php?orderBy=" + Request.QueryString["orderBy"] + "&bool=" + Request.QueryString["bool"]);    //changed ip to scotts computer. also hacked yours and stole opentags.php
                    isAsc = "false"; // switch to decs next time 
                }
                else
                {
                    queryStr = string.Format("http://tagging.inlandpower.com/openTags.php?orderBy=" + Request.QueryString["orderBy"] + "&bool=" + Request.QueryString["bool"]);    //changed ip to scotts computer. also hacked yours and stole opentags.php
                    isAsc = "true"; // whitch to asc next time 
                }

            }
            else
            {
                queryStr = string.Format("http://tagging.inlandpower.com/openTags.php");    //changed ip to scotts computer. also hacked yours and stole opentags.php

            }
            var request = System.Net.HttpWebRequest.Create(queryStr); //creates an http webrequest for the url above

            request.Method = System.Net.WebRequestMethods.Http.Get; //sets the method to get rather than post. i wonder if it works as post??

            var response = request.GetResponse(); //get the response from the page

            System.IO.StreamReader str = new System.IO.StreamReader(response.GetResponseStream()); //convert response into something we can work with

            string tagToString = str.ReadToEnd(); // convert database to string

            Match mtag = r.Match(tagToString); // uses the regex to parse the db( in this case it gets everything)
            if (mtag.Success) // if the parsing succeeds
            { // if the review data is compatabl with regex then init review list to data

                tagList = tagToString.Split(new string[] { "~`^Y" }, StringSplitOptions.None); // this puts the the string in the array seperating each part of the array by a sequence that should not be typed
            }
            //transferDataTag(false); // moves data from tag list array to the to a actual tag object. 
            //eturn TaggingList.getItems();//tried returning just to debug. right now this doesnt do much at all.
        }

        /// <summary>
        /// redirect to the closed tags page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void viewClosed(object sender, EventArgs e)
        {
            Response.Redirect("closeTag.aspx");
        }

        /// <summary>
        /// redirect to new user page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void newUser(object sender, EventArgs e)
        {
            Response.Redirect("newUser.aspx");
        }

        /// <summary>
        /// redirect to the all Users page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void viewUsers(object sender, EventArgs e)
        {
            Response.Redirect("viewAllUsers.aspx");
        }
    }
}