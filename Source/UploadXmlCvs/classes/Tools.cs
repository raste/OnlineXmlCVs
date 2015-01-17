// Online XML Cvs (https://github.com/raste/OnlineXmlCVs)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace UploadXmlCvsToSite
{
    public class Tools
    {
        public static void CheckObjectContext(CVsEntities objectContext)
        {
            if (objectContext == null)
            {
                throw new ArgumentNullException("objectContext is null");
            }
        }

        public static void Save(CVsEntities objectContext)
        {
            CheckObjectContext(objectContext);

            objectContext.SaveChanges();
        }

        public static bool IsValidXML(string fileName, byte[] fileBytes, ref string xmlErrorDescription)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("fileName is nullor empty");
            }

            bool xmlOk = false;

            if (fileBytes != null)
            {
                int maxFileLength = 2097152;  // 2MB
                int minFileLength = 1024; // 1kb

                if (minFileLength < fileBytes.Length && fileBytes.Length <= maxFileLength)
                {
                    if (IsExtensionXml(fileName, out xmlErrorDescription) == true)
                    {
                        xmlOk = true;
                    }

                }
                else
                {
                    xmlErrorDescription = "The file size is either too big or too small!";
                }
            }
            else
            {
                xmlErrorDescription = "Error uploading!";
            }

            return xmlOk;
        }

        public static bool IsExtensionXml(string fileName, out string errorMessage)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            if (fileName == string.Empty)
            {
                throw new ArgumentNullException("fileName is empty.");
            }
          
            errorMessage = string.Empty;
            bool result = false;
            try
            {
                string fileType = System.IO.Path.GetExtension(fileName);

                fileType = (fileType ?? string.Empty).ToUpper();

                if (fileType == ".XML")
                {
                    result = true;
                }
                else
                {
                    errorMessage = "Not valid XML file.";
                }

                
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Cound not determine whether the file is an xml.", "fileName", ex);
            }

            return result;
        }

        public static string EncodeXml(string str)
        {
            string encoded = string.Empty;

            if (!string.IsNullOrEmpty(str))
            {
                encoded = HttpContext.Current.Server.HtmlEncode(str);
            }

            return encoded;
        }

        public static Label NewLine()
        {
            Label newLbl = new Label();
            newLbl.Text = "<br />";
            return newLbl;
        }

        public static String GetFormattedTextFromDB(String text)
        {
            if (text == null || text == string.Empty)
            {
                return string.Empty;
            }

            string FormattedStr;

            FormattedStr = text.Replace(Environment.NewLine, "<br/>");
            FormattedStr = FormattedStr.Replace("\n", "<br/>");
            FormattedStr = FormattedStr.Replace("\r", "<br/>");

            FormattedStr = FormattedStr.Replace("<br/> ", "<br/>&nbsp;");
            FormattedStr = FormattedStr.Replace("  ", " &nbsp;");

            return FormattedStr;
        }

        public static bool EmailValidatorPassed(string emailAddress)
        {
            if (!string.IsNullOrEmpty(emailAddress))
            {
                emailAddress = emailAddress.Replace(" ", string.Empty);
            }
            else
            {
                return false;
            }

            // copy&paste from http://www.regular-expressions.info/email.html
            string patternStrict = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$";
            Regex reStrict = new Regex(patternStrict, RegexOptions.IgnoreCase);

            bool isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;

        }

    }
}