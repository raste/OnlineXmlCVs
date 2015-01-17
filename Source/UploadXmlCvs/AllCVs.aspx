<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllCVs.aspx.cs" Inherits="UploadXmlCvsToSite.AllCVs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All CVs</title>
    <link href="Styles/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

   <div class="links" style="text-align:left; margin-top: 100px; margin-bottom:10px; width:740px;">
       <asp:HyperLink ID="hlUploadCv" runat="server" NavigateUrl="~/UploadCV.aspx">Upload CV</asp:HyperLink>
   </div>
    <div class="mainAllCVs">
    
        <asp:PlaceHolder ID="phUsers" runat="server"></asp:PlaceHolder>

    </div>
    </form>
</body>
</html>
