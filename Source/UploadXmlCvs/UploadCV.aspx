<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadCV.aspx.cs" Inherits="UploadXmlCvsToSite.UploadCV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload CV</title>
    <link href="Styles/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    
     <div class="links clearfix" style="margin-top: 100px; margin-bottom:10px;">

    <div style="float:left; width:500px;">
    <asp:HyperLink ID="hlUsers" runat="server" NavigateUrl="~/AllCVs.aspx">All CVs</asp:HyperLink>
    


    &nbsp;&nbsp;&nbsp;
    


    <asp:HyperLink ID="hlCvFormat" runat="server"  NavigateUrl="~/CvFormat.xml">XML Format</asp:HyperLink>

    </div>

    <div style="float:right; text-align:right">
    <span class="madeBy">Georgi Kolev</span>
    </div>
    </div>
    <div class="main">
   
   <div style="text-align:center;">
        
        <div style="padding:20px 0px 10px 0px;">
        <asp:Label ID="lblHeader" runat="server" Font-Names="Times New Roman" style=" color:White;"
            Font-Size="X-Large" Text="Upload CV in XML format"></asp:Label>
        </div>
        <br />
        <asp:FileUpload ID="fuCV" runat="server" />
&nbsp;<asp:Button ID="btnUpload" runat="server" onclick="btnUpload_Click" CssClass="uploadBtn"
            Text="Upload" />

         </div>



       <div style="text-align:left; margin-top:15px;">


        <asp:Label ID="lblError" runat="server" Text="" CssClass="error" 
               Visible="False"></asp:Label>
        <asp:Label ID="lblSucc" runat="server" Visible="False" Font-Bold="True" 
               ForeColor="White"></asp:Label>
        <br />
        </div>
   </div>
   
    
    </form>
</body>
</html>
