<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view.aspx.cs" Inherits="TaggingWebsite.scripts.view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="indexStyle.css" />

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style ="margin-left:auto; margin-right:auto; height: 102px; width:65.8%;">
        <table  style ="width:100%; margin-left:auto;margin-right:auto; background:none;" > 
            <tr style="opacity:1; background-color:transparent" >
                <td class="imageBar"; style="text-align:center;opacity:1">
                    <a href="closeTag.aspx"><img src="https://pbs.twimg.com/media/CqEq3OoUsAAl03M.png" width="110" height="110"/></a>
                </td>
                <td class="imageBar"; style="text-align:center">
                    <a href="newUser.aspx"><img src="https://pbs.twimg.com/media/CqEq3OnUIAANaMl.png" width="100" height="100"/></a>
                </td>
                 <td class="imageBar"; style="text-align:center">
                    <a href="index.aspx"><img src="https://pbs.twimg.com/media/CqKM4mxVUAAhXKP.png" width="100" height="100"/></a>
                </td>
                <td class="imageBar"; style="text-align:center">
                    <a href="viewAllUsers.aspx"><img src="https://pbs.twimg.com/media/CqFADT2UEAAczbQ.png" width="120" height="120"/></a>
                </td>
                <td class="imageBar"; style="text-align:center">
                    <a href="newTag.aspx"><img src="https://pbs.twimg.com/media/CqEq3OoUMAAHuHY.png" width="110" height="110"/></a>

                </td>
            </tr>
        </table>

    </div>
        <div style="text-align:center ;margin-left:auto; margin-right:auto">
            <table style="width: 50%; margin-top: 100px ; margin-left:auto;margin-right:auto">
                <tr>
                     <td style="font-size: 30px;text-align:left">Edit    </td>
                    <td />
                 </tr>
                <tr>
                    <td  style="text-align: center">TagNum  </td>
                    <td runat="server" id="tagNumText" />
                </tr>
                <tr>
                    <td style="text-align: center">Type</td>
                    <td>
                        <input name ="type" runat="server" id="clearCheck" type="radio" />clearance
                        <input name ="type" runat="server" id="cautionCheck" type="radio" />caution
                        <input name ="type" runat="server" id="holdCheck" type="radio" />hold
                    </td>
                </tr>
                <tr>
                    <td>Date:</td>
                    <td runat="server" id="dateTag" runat="server"></td>

                </tr>
                <tr>
                    <td>Issued By:</td>
                    <td> 
                        <input runat="server" id="IssuedByText" type ="text"/>
                    </td>
                </tr>
                <tr>
                    <td>User</td>
                    <td>
                        <asp:DropDownList ID="dropdownlist" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Requested By:</td>
                    <td> 
                        <input runat="server" id="RequestedByText" type ="text"/>
                    </td>
                </tr>
                <tr>
                    <td>Truck #:</td>
                    <td> 
                        <input runat="server" id="truckText" type ="text"/>
                    </td>
                </tr>
                <tr>
                    <td>Requested For</td>
                    <td> 
                        <input runat="server" id="requestedForText" type ="text"/>
                    </td>
                </tr>
                <tr>
                    <td>Purpose:</td>
                    <td>
                        <select runat="server" id="purposeDrop">
                            <option value ="Please Select">Please Select</option>
                            <option value ="Maintenance">Maintenance</option>
                            <option value ="Construction">Construction</option>
                            <option value ="Trouble/Storm">Trouble/Storm</option>
                            <option value ="Assurance No Backfeed">Assurance No Backfeed</option>
                            <option value ="POPUD Mtr Exchange">POPUD Mtr Exchange</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>Equipment:</td>
                    <td>
                        <select runat="server" id="equipmentDrop">
                            <option value ="Please Select">Please Select</option>
                            <option value ="Recloser">Recloser</option>
                            <option value ="Regulator">Regulator</option>
                            <option value ="Switch/Fuse">Switch/Fuse</option>
                            <option value ="Jumper/Elbow">Jumper/Elbow</option>
                            <option value ="Circuit Switcher">Circuit Switcher</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>Poles:</td>
                    <td>
                        <input runat="server" id ="pole1"/>
                        -
                        <input runat="server" id="pole2"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">Required Notifications</td>
                    <td>
                        <input name="notifications" runat="server" id= "noneCheckBox" type="radio" checked="true"/>None<br />
                        <input name="notifications" type="radio" runat="server" id="bpaCheckBox"/>BPA (MCC Operator)<br />
                        <input name="notifications" type="radio" runat="server" id="avistaCheckBox"/>Avista (Operator)<br />
                        <input  name="notifications" type="radio" runat="server" id="popudCheckBox"/>POPUD (Dispatch)<br />
                        <input name="notifications" type="radio" runat="server" id="grantCheckBox"/>Grant Co. PUD (Dispatch)
                    </td>
                </tr>
                <tr>
                    <td>Comments:</td>
                    <td>
                        <textarea runat="server" id="commentsBox" style="margin: 0px; height: 140px; width: 350px;"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                        <button id="resetButton" runat="server" onserverclick="resetFunc" style="text-align:center">Reset Form</button>
                    </td>
                    <td>
                        <button id="editSubmitSubmit" runat="server" onserverclick="editSubmit" style="text-align:center">Submit</button>
                    </td>
                    
                </tr>
               
                


                

            </table>
        </div>
    </form>
</body>
</html>
