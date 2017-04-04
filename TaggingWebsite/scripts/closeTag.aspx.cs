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

namespace TaggingWebsite.scripts
{
    public partial class closeTag : System.Web.UI.Page
    {
        string queryStr;// = "";
        string isAsc = "false";


        private string[] closedTagList;
        protected void Page_Load(object sender, EventArgs e)
        {
            string lastRefresh = DateTime.Now.ToString();
            lastRefreshed.InnerText = "Last Refreshed: " + lastRefresh;
            if (Request.QueryString["bool"] != null)
                if (Request.QueryString["bool"] == "true")
                {
                    isAsc = "fasle";
                }
                else
                {
                    isAsc = "true";
                }
            try
            {
                TableHeaderRow header = new TableHeaderRow();
                TableHeaderCell[] cells = new TableHeaderCell[7];
                for (int i = 0; i < cells.Length; i++)
                {
                    cells[i] = new TableHeaderCell();
                }
                cells[0].Text = "Tag Num";
                cells[1].Text = "Date";
                cells[2].Text = "Pole 1";
                cells[3].Text = "Pole2";
                cells[4].Text = "Requested By";
                cells[5].Text = "Type";
                cells[6].Text = "View";

                string[] sortArray = {
                        "<a href=\"closeTag.aspx?orderBy=TagNum&bool=" + isAsc + "\">TagNum</a>",
                        "<a href=\"closeTag.aspx?orderBy=Date&bool=" + isAsc + "\">Date</a>",
                        "<a href=\"closeTag.aspx?orderBy=Pole1&bool=" + isAsc + "\">Pole 1</a>",
                        "<a href=\"closeTag.aspx?orderBy=Pole2&bool=" + isAsc + "\">Pole 2</a>",
                        "<a href=\"closeTag.aspx?orderBy=RequestedBy&bool=" + isAsc + "\">Requested By</a>",
                        "<a href=\"closeTag.aspx?orderBy=Type&bool=" + isAsc + "\">Type</a>" };


                for (int i = 0; i < cells.Length; i++)
                {
                    if (i < sortArray.Length)
                        cells[i].Controls.Add(new LiteralControl(sortArray[i]));
                    header.Cells.Add(cells[i]);

                }
                closedTable.Rows.Add(header);
                start();
                for (int i = 0; i < closedTagList.Length - 1; i++) // goes through each row of the array 
                {

                    TableRow trow = new TableRow();
                    TableCell[] tcell = new TableCell[7];
                    for (int j = 0; j < tcell.Length; j++)
                    {
                        tcell[j] = new TableCell();
                    }

                    if (GetDataValue(closedTagList[i], "tagNum ") != null) // double check to make sure the array hasinformaoiton 
                    {
                        /**
                         * all this will do is get the correct daqta from the array and set that to the correct attribute in the tag object 
                         * */
                        tcell[0].Text = GetDataValue(closedTagList[i], "tagNum ");
                        tcell[1].Text = GetDataValue(closedTagList[i], "date");
                        tcell[2].Text = GetDataValue(closedTagList[i], "pole1");
                        tcell[3].Text = GetDataValue(closedTagList[i], "pole2");
                        tcell[4].Text = GetDataValue(closedTagList[i], "requestedBy");
                        tcell[5].Text = GetDataValue(closedTagList[i], "type");
                    }

                    LinkButton editButton = new LinkButton();
                    //editButton.ToolTip = ;
                    editButton.ToolTip.PadRight(15);
                    editButton.Text = "View";
                    editButton.Enabled = true;
                    editButton.Click += new System.EventHandler(this.editFunc);
                    tcell[6].Controls.Add(editButton);

                    trow.Cells.Add(tcell[0]);
                    trow.Cells.Add(tcell[1]);
                    trow.Cells.Add(tcell[2]);
                    trow.Cells.Add(tcell[3]);
                    trow.Cells.Add(tcell[4]);
                    trow.Cells.Add(tcell[5]);
                    trow.Cells.Add(tcell[6]);
                    closedTable.Rows.Add(trow);
                }

            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// parsing function
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
        ///send info to the view page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void editFunc(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            Response.Write(b.Parent.Parent);
            TableRow tr = (TableRow)b.Parent.Parent;

            Response.Redirect("view.aspx?TagNum=" + tr.Cells[0].Text + " &Date=" + tr.Cells[1].Text
                                + " &Pole1=" + tr.Cells[2].Text + " &Pole2=" + tr.Cells[3].Text + " &requested="
                                + tr.Cells[4].Text + " &type=" + tr.Cells[5].Text + "&Notifications=" + tr.Cells[6].Text + "&View=closed");
        }




        /// <summary>
        /// 
        /// parsingfunctions
        /// </summary>
        public void start()
        {
            Regex r = new Regex("(.)"); // it will retrieve everything in the php script

            if (Request.QueryString["orderBy"] != null)
            {
                if (isAsc == "true")
                {
                    queryStr = string.Format("http://tagging.inlandpower.com/closedTags.php?orderBy=" + Request.QueryString["orderBy"] + "&bool=" + Request.QueryString["bool"]);    //changed ip to scotts computer. also hacked yours and stole opentags.php
                    isAsc = "false";
                }
                else
                {
                    queryStr = string.Format("http://tagging.inlandpower.com/closedTags.php?orderBy=" + Request.QueryString["orderBy"] + "&bool=" + Request.QueryString["bool"]);    //changed ip to scotts computer. also hacked yours and stole opentags.php
                    isAsc = "true";
                }

            }
            else
            {
                queryStr = string.Format("http://tagging.inlandpower.com/closedTags.php");    //changed ip to scotts computer. also hacked yours and stole opentags.php

            }
            var request = System.Net.HttpWebRequest.Create(queryStr); //creates an http webrequest for the url above

            request.Method = System.Net.WebRequestMethods.Http.Get; //sets the method to get rather than post. i wonder if it works as post??

            var response = request.GetResponse(); //get the response from the page

            System.IO.StreamReader str = new System.IO.StreamReader(response.GetResponseStream()); //convert response into something we can work with

            string tagToString = str.ReadToEnd(); // convert database to string

            Match mtag = r.Match(tagToString); // uses the regex to parse the db( in this case it gets everything)
            if (mtag.Success) // if the parsing succeeds
            { // if the review data is compatabl with regex then init review list to data

                closedTagList = tagToString.Split(new string[] { "~`^Y" }, StringSplitOptions.None); // this puts the the string in the array seperating each part of the array by a sequence that should not be typed
            }
        }
    }
}



// Create the ToolTip and associate with the Form container.
// ToolTip toolTip1 = new ToolTip();

// Set up the delays for the ToolTip.
//   toolTip1.AutoPopDelay = 5000;
//  toolTip1.InitialDelay = 1000;
//    toolTip1.ReshowDelay = 500;
// Force the ToolTip text to be displayed whether or not the form is active.
//    toolTip1.ShowAlways = true;

// Set up the ToolTip text for the Button and Checkbox.
//  toolTip1.SetToolTip(this.button1, "My button1");

//      tcell[7].Text = "Close Tag";
