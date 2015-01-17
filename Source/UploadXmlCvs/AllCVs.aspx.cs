// Online XML Cvs (https://github.com/raste/OnlineXmlCVs)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UploadXmlCvsToSite
{
    public partial class AllCVs : System.Web.UI.Page
    {
        private CVsEntities objectContext = new CVsEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowCvs();
        }

        private void ShowCvs()
        {
            phUsers.Controls.Clear();

            GetCVs getCvs = new GetCVs();
            List<User> users = getCvs.GetAllUsers(objectContext);

            if (users != null && users.Count > 0)
            {

                List<CV> userCVs = new List<CV>();

                foreach (User user in users)
                {
                    userCVs = getCvs.GetAllUserCVs(objectContext, user);

                    Panel newPnl = new Panel();
                    phUsers.Controls.Add(newPnl);
                    newPnl.CssClass = "userCVs";

                    Panel userPnl = new Panel();
                    newPnl.Controls.Add(userPnl);
                    userPnl.CssClass = "userPnl";

                    Image img = new Image();
                    userPnl.Controls.Add(img);
                    img.ImageUrl = "~/images/circle.png";

                    Label username = new Label();
                    userPnl.Controls.Add(username);
                    username.CssClass = "username";
                    username.Text = string.Format("{0} {1}", user.firstName, user.lastName);

                    Panel cvPnl = new Panel();
                    newPnl.Controls.Add(cvPnl);
                    cvPnl.CssClass = "cvPnl";

                    if (userCVs != null && userCVs.Count > 0)
                    {

                        foreach (CV cv in userCVs)
                        {
                            Panel cvLinkPnl = new Panel();
                            cvPnl.Controls.Add(cvLinkPnl);

                            if (cvPnl.Controls.Count > 1)
                            {
                                cvLinkPnl.Attributes.CssStyle.Add(HtmlTextWriterStyle.MarginTop,"3px");
                            }

                            HyperLink cvLink = new HyperLink();
                            cvLinkPnl.Controls.Add(cvLink);
                            cvLink.Text = cv.description;
                            cvLink.NavigateUrl = string.Format("UserCV.aspx?CV={0}", cv.ID);
                        }

                    }
                    else
                    {
                        cvPnl.Attributes.CssStyle.Add(HtmlTextWriterStyle.TextAlign, "center");

                        Label noCvs = new Label();
                        cvPnl.Controls.Add(noCvs);
                        noCvs.CssClass = "error";
                        noCvs.Text = "No uploaded CVs";
                    }

                }


            }
            else
            {
                Panel newPnl = new Panel();
                phUsers.Controls.Add(newPnl);
                newPnl.HorizontalAlign = HorizontalAlign.Center;

                Label newLbl = new Label();
                newPnl.Controls.Add(newLbl);
                newLbl.Text = "No uploaded CVs!";
                newLbl.ForeColor = System.Drawing.Color.White;
            }

        }


    }
}