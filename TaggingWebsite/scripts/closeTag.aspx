﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="closeTag.aspx.cs" Inherits="TaggingWebsite.scripts.closeTag" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="indexStyle.css" />

    <title>Closed Tag</title>
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
           
    <div style ="text-align:center" >
         <p style="font-size:25px" id="lastRefreshed" runat ="server"></p>
        <asp:table style="text-align:center; width:65.8%; border:2px; margin-left:auto;margin-right:auto" id="closedTable" runat="server" ></asp:table>
        <asp:PlaceHolder ID = "PlaceHolder1" runat="server" />

        
   
    </div>
    </form>
</body>
</html>
