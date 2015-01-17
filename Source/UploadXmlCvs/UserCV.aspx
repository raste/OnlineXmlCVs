<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCV.aspx.cs" Inherits="UploadXmlCvsToSite.UserCV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Styles.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="form1" runat="server">


     <div class="links" style="text-align:left; margin-top: 30px; margin-bottom:10px; width:740px;">
       
        <asp:HyperLink ID="hlUploadCV" runat="server" NavigateUrl="~/UploadCV.aspx">Upload CV</asp:HyperLink>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
        <asp:HyperLink ID="hlAllUsers" runat="server" NavigateUrl="~/AllCVs.aspx">All CVs</asp:HyperLink>
    </div>

    <div class="CVinfo">
    
        <div class="clearfix2">

            <asp:Image ID="imgUser" runat="server" ImageAlign="Left" CssClass="clearfix" 
                ImageUrl="~/images/user.png" Width="100px" />
            
            <div style="text-align:center; margin-right:40px;">
            <asp:Label ID="lblUserName" runat="server" Text="User Name" 
            style="font-family:'Calibri';font-size:24px;font-weight:bold; color:White;"></asp:Label>
            </div>
            <br />

            Email: <asp:Label ID="lblEmail" runat="server" Text="asd@asd.com" style="font-weight:bold;"></asp:Label>
            <br />

        </div>

        <div class="clearfix2">
        <p class="header"> Personal Details </p>
      
        <div class="leftColumn">
        
       
        First Name:<br />
        Last Name:<br />
       
        <p class="section">
        Gender:<br />
        Year Born:<br />
        Nationality:
        </p>

        <p class="section">
        I live in:
        </p>
        
        
        
        
        
        </div>
        <div class="rightColumn">

        
        <asp:Label ID="lblFirstName" runat="server" Text="Georgi" style="font-weight:bold;"></asp:Label><br />
            <asp:Label ID="lblLastName" runat="server" Text="Kolev" style="font-weight:bold;"></asp:Label><br />
            
        

        <p class="section">
        <asp:Label ID="lblGender" runat="server" Text="male" style="font-weight:bold;"></asp:Label><br />
        <asp:Label ID="lblYear" runat="server" Text="12345" style="font-weight:bold;"></asp:Label><br />
        <asp:Label ID="lblNationality" runat="server" Text="asdfgdf" style="font-weight:bold;"></asp:Label>
        </p>

        <p class="section">
        <asp:Label ID="lblCity" runat="server" Text="some" style="font-weight:bold;"></asp:Label>
        </p>
            
            
            
        
        </div>

        </div>

        <asp:Panel ID="pnlEducation" runat="server">

        <p class="header"> Education </p>
      

            <asp:PlaceHolder ID="phEducation" runat="server"></asp:PlaceHolder>

        </asp:Panel>
       
        <asp:Panel ID="pnlWorkExperiance" runat="server">

        <p class="header"> Work Experience </p>
        

            <asp:PlaceHolder ID="phWorkExperiance" runat="server"></asp:PlaceHolder>

        </asp:Panel>

         <asp:Panel ID="pnlSkills" runat="server">

         <p class="header"> Skills </p>
        

            <asp:PlaceHolder ID="phSkills" runat="server"></asp:PlaceHolder>

        </asp:Panel>

        <asp:Panel ID="pnlDetails" CssClass="clearfix2" runat="server">
        <p class="header"> Additional Details </p>
    
    <div class="leftColumn">
    
    Additional Details:
    
    </div>
    <div class="rightColumn">
    
        <asp:Label ID="lblDetails" runat="server" Text="Details"></asp:Label>
    
    </div>

       </asp:Panel>

    
    </div>
    </form>
</body>
</html>
