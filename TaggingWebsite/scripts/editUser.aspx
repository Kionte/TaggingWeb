<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editUser.aspx.cs" Inherits="TaggingWebsite.scripts.editUser" %>

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
    <div style="width:50%; margin-left:auto; margin-right:auto; margin-top:100px">
        <table style="margin-left:auto; margin-right:auto">
            <tr>
                <td style="font-size:28px">Edit User</td>
            </tr>
            
 
            <tr>
                <td>Username</td>
                <td>
                    <input runat="server" id ="fillUsername" />
                </td>
            </tr>
            <tr>
                <td>Password</td>
                <td>
                    <input id="fillPassword" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Email</td>
                <td>
                    <input id="fillEmail" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <button id="resetFormButton" runat="server" onserverclick="resetForm">Reset Form</button>
                </td>
                <td>
                    <button id ="deleteUserButton" runat="server" onserverclick="deleteUser">Delete User</button>
                    <button id="submitUser" runat="server" style="margin-left:250px " onserverclick="updateUser">Update</button>
                </td>
                
            </tr>
        </table>
    </div>
    </form>
</body>
</html>