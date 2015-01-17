// Online XML Cvs (https://github.com/raste/OnlineXmlCVs)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UploadXmlCvsToSite
{
    public partial class UserCV : System.Web.UI.Page
    {
        private CVsEntities objectContext = new CVsEntities();
        private User currentUser = null;
        private CV currentCV = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckParamsAndLoadData();

            Title = string.Format("{0} {1}'s CV", currentUser.firstName, currentUser.lastName);

            ShowCvData();
        }

        private void ShowCvData()
        {
            // personal data

            lblUserName.Text = string.Format("{0} {1}", currentUser.firstName, currentUser.lastName);
            lblEmail.Text = currentUser.email;

            lblFirstName.Text = currentUser.firstName;
            lblLastName.Text = currentUser.lastName;

            lblGender.Text = currentCV.gender;
            lblYear.Text = currentCV.born.ToString();
            lblNationality.Text = currentCV.nationality;

            lblCity.Text = currentCV.liveIncity;

            if (currentCV.gender.ToLower() == "male" || currentCV.gender.ToLower() == "мъж")
            {
                imgUser.ImageUrl = "~/images/user.png";
            }
            else
            {
                imgUser.ImageUrl = "~/images/userf.png";
            }
        
            // Education
            FillEductaion();

            // Work Experience
            FillWorkExperience();

            // Skills
            FillSkills();

            // Additional details
            if (!string.IsNullOrEmpty(currentCV.details))
            {
                pnlDetails.Visible = true;

                lblDetails.Text = Tools.GetFormattedTextFromDB(currentCV.details);
            }
            else
            {
                pnlDetails.Visible = false;
            }

        }

        private void FillEductaion()
        {
            List<Education> edu = new List<Education>();
            currentCV.Education.Load();
            edu = currentCV.Education.ToList();

            if (edu != null && edu.Count > 0)
            {
                pnlEducation.Visible = true;
                phEducation.Controls.Clear();

                List<Subject> subjects = new List<Subject>();
                int educount = 0;

                foreach (Education education in edu)
                {

                    if (educount > 0)
                    {
                        Panel hrPnl = new Panel();
                        phEducation.Controls.Add(hrPnl);
                        hrPnl.CssClass = "hrPnl";
                    }

                    Panel pnlEdu = new Panel();
                    phEducation.Controls.Add(pnlEdu);

                    string toDate = string.Empty;
                    if (education.toDate.HasValue)
                    {
                        toDate = education.toDate.Value.ToShortDateString();
                    }


                    AddDataRowToPnl(pnlEdu, "Period:", string.Format("{0} - {1}", education.fromDate.ToShortDateString(), toDate), false, true);

                    if (!string.IsNullOrEmpty(education.major))
                    {
                        AddDataRowToPnl(pnlEdu, "Major:", education.major, true, true);
                    }

                    if (!string.IsNullOrEmpty(education.level))
                    {
                        AddDataRowToPnl(pnlEdu, "Level:", education.level, true, false);
                    }

                    AddDataRowToPnl(pnlEdu, "Place:", education.placeName, true, true);
                    
                    AddDataRowToPnl(pnlEdu, "Location:", education.location, true, false);

                    if (!string.IsNullOrEmpty(education.details))
                    {
                        AddDataRowToPnl(pnlEdu, "Details:", education.details, true, false);
                    }

                    // subjects

                    education.Subjects.Load();
                    subjects = education.Subjects.ToList();

                    if (subjects != null && subjects.Count > 0)
                    {
                        Panel subjPnl = new Panel();
                        pnlEdu.Controls.Add(subjPnl);
                        subjPnl.CssClass = "clearfix2";

                        subjPnl.Attributes.CssStyle.Add(HtmlTextWriterStyle.MarginTop, "10px");
                        
                        Panel leftPnl = new Panel();
                        subjPnl.Controls.Add(leftPnl);
                        leftPnl.CssClass = "leftColumn";

                        Panel rightPnl = new Panel();
                        subjPnl.Controls.Add(rightPnl);
                        rightPnl.CssClass = "rightColumn";

                        Label subjLbl = new Label();
                        leftPnl.Controls.Add(subjLbl);
                        subjLbl.Text = "Main Subjects:";

                        int count = 0;

                        foreach (Subject subject in subjects)
                        {
                            if (count > 0)
                            {
                                rightPnl.Controls.Add(Tools.NewLine());
                            }

                            Label lbldescr = new Label();
                            rightPnl.Controls.Add(lbldescr);
                            lbldescr.Text = subject.subject;

                            count++;
                        }

                    }

                    educount++;

                }
            }
            else
            {
                pnlEducation.Visible = false;
            }
        }

        private void FillWorkExperience()
        {
            List<WorkExperiance> workExp = new List<WorkExperiance>();
            currentCV.WorkExperiance.Load();
            workExp = currentCV.WorkExperiance.ToList();

            if (workExp != null && workExp.Count > 0)
            {
                pnlWorkExperiance.Visible = true;
                phWorkExperiance.Controls.Clear();

                int workcount = 0;

                foreach (WorkExperiance experience in workExp)
                {

                    if (workcount > 0)
                    {
                        Panel hrPnl = new Panel();
                        phWorkExperiance.Controls.Add(hrPnl);
                        hrPnl.CssClass = "hrPnl";
                    }

                    Panel pnlWE = new Panel();
                    phWorkExperiance.Controls.Add(pnlWE);

                    string toDate = string.Empty;
                    if (experience.toDate.HasValue)
                    {
                        toDate = experience.toDate.Value.ToShortDateString();
                    }


                    AddDataRowToPnl(pnlWE, "Period:", string.Format("{0} - {1}", experience.fromDate.ToShortDateString(), toDate), false, true);

                    if (!string.IsNullOrEmpty(experience.companyName))
                    {
                        AddDataRowToPnl(pnlWE, "Company:", experience.companyName, true, true);
                    }

                    AddDataRowToPnl(pnlWE, "Position:", experience.position, true, true);


                    if (!string.IsNullOrEmpty(experience.details))
                    {
                        AddDataRowToPnl(pnlWE, "Details:", experience.details, true, false);
                    }

                    workcount++;

                }
            }
            else
            {
                pnlWorkExperiance.Visible = false;
            }
        }

        private void FillSkills()
        {
            List<Skill> skills = new List<Skill>();
            currentCV.Skills.Load();
            skills = currentCV.Skills.ToList();

            if (skills != null && skills.Count > 0)
            {
                pnlSkills.Visible = true;
                phSkills.Controls.Clear();

                Panel pnlSkill = new Panel();
                phSkills.Controls.Add(pnlSkill);
                pnlSkill.CssClass = "clearfix2";

                Panel leftPnl = new Panel();
                pnlSkill.Controls.Add(leftPnl);
                leftPnl.CssClass = "leftColumn";

                Panel rightPnl = new Panel();
                pnlSkill.Controls.Add(rightPnl);
                rightPnl.CssClass = "rightColumn";

                Label skillslbl = new Label();
                leftPnl.Controls.Add(skillslbl);
                skillslbl.Text = "Skills:";

                int count = 0;
                string data = string.Empty;

                foreach (Skill skill in skills)
                {

                    if (count > 0)
                    {
                        rightPnl.Controls.Add(Tools.NewLine());
                    }

                    data = skill.skill;

                    if (!string.IsNullOrEmpty(skill.gradation))
                    {
                        data += string.Format(" ({0})", skill.gradation);
                    }

                    if (!string.IsNullOrEmpty(skill.details))
                    {
                        data += string.Format(" - {0}", skill.details);
                    }

                    Label lblSkill = new Label();
                    rightPnl.Controls.Add(lblSkill);
                    lblSkill.Text = data;
                    lblSkill.Font.Bold = true;

                    count++;
                }
            }
            else
            {
                pnlSkills.Visible = false;
            }
        }

        private void AddDataRowToPnl(Panel pnl, string leftCol, string rightCol, bool marginTop, bool bold)
        {
            if (pnl == null)
            {
                throw new ArgumentNullException("pnl");
            }

            if (string.IsNullOrEmpty(leftCol))
            {
                throw new ArgumentException("leftCol is null or empty");
            }

            if (string.IsNullOrEmpty(rightCol))
            {
                throw new ArgumentException("rightCol is null empty");
            }


            Panel mainPnl = new Panel();
            pnl.Controls.Add(mainPnl);
            mainPnl.CssClass = "clearfix2";

            if (marginTop == true)
            {
                mainPnl.Attributes.CssStyle.Add(HtmlTextWriterStyle.MarginTop, "5px");
            }


            Panel typePnl = new Panel();
            mainPnl.Controls.Add(typePnl);
            typePnl.CssClass = "leftColumn";
            
            Panel dataPnl = new Panel();
            mainPnl.Controls.Add(dataPnl);
            dataPnl.CssClass = "rightColumn";

            Label lblleftData = new Label();
            typePnl.Controls.Add(lblleftData);
            lblleftData.Text = leftCol;

            Label lblrightData = new Label();
            dataPnl.Controls.Add(lblrightData);
            lblrightData.Text = rightCol;
            lblrightData.Font.Bold = bold;

        }

        private void CheckParamsAndLoadData()
        {
            String strCvID = Request.Params["CV"];
            if (!string.IsNullOrEmpty(strCvID))
            {
                GetCVs getCV = new GetCVs();

                long cvID = -1;
                if (long.TryParse(strCvID, out cvID))
                {
                    currentCV = getCV.GetCV(objectContext, cvID, false);
                }
                else
                {
                    Response.Redirect("AllCVs.aspx");
                }
            }
            else
            {
                Response.Redirect("AllCVs.aspx");
            }

            if (currentCV == null)
            {
                Response.Redirect("AllCVs.aspx");
            }

            currentCV.UserReference.Load();
            currentUser = currentCV.User;

        }

    }
}