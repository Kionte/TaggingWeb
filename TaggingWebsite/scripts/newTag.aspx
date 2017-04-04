<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newTag.aspx.cs" Inherits="TaggingWebsite.scripts.newTag" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="indexStyle.css" />
    <title></title>
</head>
<body>
    <form id="formNewTag" runat="server">
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
        <div style="text-align:center">
            <table style="width:50%; margin-top: 100px; margin-left:auto; margin-right:auto">
                <tr>
                    <td style="font-size: 20px;text-align:center">New Tag</td>
                </tr>
                <tr>
                    <td>TagNum</td>
                    <td id="tagNumText" runat="server" />
                </tr>
                <tr>
                    <td>Type</td>
                    <td>
                        <input runat="server" name="type" id="clearCheck" type="radio" value="clearance" checked="true"/>Clearance
                        <input runat="server" name="type" id="cautionCheck" type="radio" value="caution" />Caution
                        <input runat="server" name="type" id="holdCheck" type="radio" value="hold"/>Hold

                    </td>
                </tr>
                <tr>
                    <td>Time:</td>
                    <td id="dateNewTag" runat="server"></td>

                </tr>
                <tr>
                    <td>Issued By</td>
                    <td> 
                        <input id="IssuedByTextNew" runat="server" type ="text"/>
                    </td>
                </tr>
                <tr>
                    <td>User:</td>
                    <td>
                        <asp:DropDownList ID ="dropdownlist" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Requested By:</td>
                    <td> 
                        <input id="RequestedByTextNew" type ="text" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td>Truck #:</td>
                    <td> 
                        <input id="truckTextNew" runat="server" type ="text"/>
                    </td>
                </tr>
                <tr>
                    <td>Requested For</td>
                    <td> 
                        <input id="requestedForTextNew" type ="text" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td>Purpose:</td>
                    <td>
                        <select runat="server" id ="purposeDropDown">
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
                        <select runat="server" id="equipmentDropDown">
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
                        <input runat="server" id="pole1Text"/>
                        -
                        <input runat="server" id="pole2Text"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">Required Notifications</td>
                    <td>
                        <input  id="noneCheck" name="Notifications" runat="server" type="radio" checked="true" />None<br />
                        <input  id="bpaCheck" name="Notifications" runat="server" type="radio" />BPA (MCC Operator)<br />
                        <input  id="avistaCheck"  name="Notifications" runat="server" type="radio" />Avista (Operator)<br />
                        <input  id="popudCheck" name="Notifications" runat="server" type="radio" />POPUD (Dispatch)<br />
                        <input  id="grantCheck" name="Notifications" runat="server" type="radio" />Grant Co. PUD (Dispatch)
                    </td>
                </tr>
                <tr>
                    <td>Comments:</td>
                    <td>
                        <textarea id="commentsText" runat="server" style="margin: 0px; height: 140px; width: 350px;"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                        <button align="center" onserverclick="resetForm" runat="server">Reset Form</button>
                    </td>
                    <td>
                       <button id="submitButton" runat="server" onserverclick="submitOnClick" align="center" >Submit</button>
                    </td>
                    
                </tr>
               
                


                

            </table>
        </div>
    </form>
</body>
</html>
