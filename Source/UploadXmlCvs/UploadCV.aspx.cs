// Online XML Cvs (https://github.com/raste/OnlineXmlCVs)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;

namespace UploadXmlCvsToSite
{
    public partial class UploadCV : System.Web.UI.Page
    {
        private CVsEntities objectContext = new CVsEntities();
        object UploadingXML = new object();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblSucc.Visible = false;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuCV.HasFile)
            {
                string xmlErrorDescription = string.Empty;
                byte[] fileBytes = fuCV.FileBytes;
                string fileName = fuCV.FileName;
                string fileType = System.IO.Path.GetExtension(fileName);

                bool fileOK = Tools.IsValidXML(fileName, fileBytes, ref xmlErrorDescription);

                if (fileOK == true)
                {
                    lock (UploadingXML)
                    {
                        ParseXMLtoDB parseXML = new ParseXMLtoDB();
                        bool parseSucc = parseXML.Parse(objectContext, ref xmlErrorDescription, fuCV.FileContent);

                        if (parseSucc == true)
                        {
                            lblSucc.Text = "CV uploaded successfully!";
                            lblSucc.Visible = true;
                        }
                        else
                        {
                            lblError.Text = xmlErrorDescription;
                            lblError.Visible = true;
                        }
                    }
                }
                else
                {
                    lblError.Text = "The file is not an XML!";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Choose file!";
                lblError.Visible = true;
            }
        }

    }
}