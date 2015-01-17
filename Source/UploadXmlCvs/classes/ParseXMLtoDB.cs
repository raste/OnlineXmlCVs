// Online XML Cvs (https://github.com/raste/OnlineXmlCVs)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;

namespace UploadXmlCvsToSite
{
    public class ParseXMLtoDB
    {
        object parsingXml = new object();

        public bool Parse(CVsEntities objectContext, ref string error, System.IO.Stream stream)
        {
           Tools.CheckObjectContext(objectContext);

            if (stream == null)
            {
                throw new ArgumentNullException("stream is null");
            }

            bool operationSuccess = false;

            try
            {
                lock (parsingXml)
                {
                    StringBuilder sbErrors = new StringBuilder();
                    string elementError = string.Empty;
                    bool incorrectElements = false; 

                    System.Xml.XmlDocument cvAsXML = new System.Xml.XmlDocument();
                    cvAsXML.Load(stream);

                    XmlNode root = cvAsXML.DocumentElement;
                    XmlAttribute cvDescr = root.Attributes["description"];
                    if (cvDescr != null && !string.IsNullOrEmpty(cvDescr.Value))
                    {
                        if (cvDescr.Value.Length > 200)
                        {
                            incorrectElements = true;
                            sbErrors.Append("<br />&nbsp;&nbsp;&nbsp;CV description is too long (must be less than 200)!");
                        }
                    }
                    else
                    {
                        incorrectElements = true;
                        sbErrors.Append("<br />&nbsp;&nbsp;&nbsp;No specified CV description!");
                    }

                    // Main elements

                    XmlNode ndFirstName = root.SelectSingleNode("FirstName");
                    XmlNode ndLastName = root.SelectSingleNode("LastName");
                    XmlNode ndEmail = root.SelectSingleNode("Email");
                    XmlNode ndYearBorn = root.SelectSingleNode("YearBorn");
                    XmlNode ndNationality = root.SelectSingleNode("Nationality");
                    XmlNode ndCity = root.SelectSingleNode("LiveInCity");
                    XmlNode ndDetails = root.SelectSingleNode("Details");
                    XmlNode ndGender = root.SelectSingleNode("Gender");


                    if (ErrorsInMainElements(sbErrors, ndFirstName, ndLastName, ndEmail, ndYearBorn
                        , ndNationality, ndCity, ndDetails, ndGender) == true)
                    {
                        incorrectElements = true;
                    }


                    // Education elements

                    XmlNode education = root.SelectSingleNode("Education");
                    if (ErrorsInEducation(sbErrors, education) == true)
                    {
                        incorrectElements = true;
                    }

                    // WorkExperiance elements

                    XmlNode workExperiance = root.SelectSingleNode("WorkExperiance");
                    if (ErrorsInWorkExperiance(sbErrors, workExperiance) == true)
                    {
                        incorrectElements = true;
                    }

                    // Skills elements

                    XmlNode skills = root.SelectSingleNode("Skills");
                    if (ErrorsInSkills(sbErrors, skills) == true)
                    {
                        incorrectElements = true;
                    }

                    if (incorrectElements == false)
                    {
                        // NO errors in XML format
                         
                        // Check if there is already such CV

                        bool isUserAlreadyAdded = false;

                        if (IsThereAlreadySuchUserCv(objectContext, ndFirstName.InnerXml, ndLastName.InnerXml, ndEmail.InnerXml
                            , cvDescr.InnerXml, out isUserAlreadyAdded) == false)
                        {

                            ////////// Everything is OK - begin parsing

                            User currentUser = null;

                            if (isUserAlreadyAdded == false)
                            {
                                // adding the user

                                User newUser = new User();

                                newUser.firstName = Tools.EncodeXml(ndFirstName.InnerXml);
                                newUser.lastName = Tools.EncodeXml(ndLastName.InnerXml);
                                newUser.email = Tools.EncodeXml(ndEmail.InnerXml);

                                objectContext.AddToUserSet(newUser);
                                Tools.Save(objectContext);

                                currentUser = newUser;
                            }
                            else
                            {
                                GetCVs getCvs = new GetCVs();

                                currentUser = getCvs.GetUser(objectContext, ndFirstName.InnerXml, ndLastName.InnerXml, ndEmail.InnerXml);
                            }

                            if (currentUser == null)
                            {
                                throw new InvalidOperationException("current user is null");
                            }


                            // adding CV - main details

                            int yearBorn = 0;
                            if (int.TryParse(ndYearBorn.InnerXml, out yearBorn) == false)
                            {
                                throw new Exception("Couldn`t parse YearBorn to int.");
                            }

                            CV newCV = new CV();

                            newCV.User = currentUser;
                            newCV.born = yearBorn;
                            newCV.nationality = Tools.EncodeXml(ndNationality.InnerXml);
                            newCV.liveIncity = Tools.EncodeXml(ndCity.InnerXml);
                            newCV.gender = Tools.EncodeXml(ndGender.InnerXml.ToLower());

                            if (ndDetails != null)
                            {
                                newCV.details = Tools.EncodeXml(ndDetails.InnerXml);
                            }

                            newCV.dateAdded = DateTime.UtcNow;
                            newCV.description = Tools.EncodeXml(cvDescr.Value);

                            objectContext.AddToCVSet(newCV);
                            Tools.Save(objectContext);

                            // adding education 
                            AddEducation(objectContext, newCV, education);

                            // adding work experiance
                            AddWorkExperience(objectContext, newCV, workExperiance);

                            // adding skills
                            AddSkills(objectContext, newCV, skills);

                            operationSuccess = true;

                        }
                        else
                        {
                            sbErrors.Append("There is already uploaded CV for this person with same description!");
                        }

                    }
                    else
                    {
                        sbErrors.Insert(0, "Incorrect document format. Some of the errors:");
                        operationSuccess = false;
                    }

                    error = sbErrors.ToString();
                }
            }
            catch
            {
                error = "Error loading XML file. Possible reason : incorrect xml format.";
            }


            return operationSuccess;
        }


        private void AddSkills(CVsEntities objectContext, CV currCv, XmlNode skills)
        {
            Tools.CheckObjectContext(objectContext);
            if (currCv == null)
            {
                throw new ArgumentNullException("currCv");
            }


            if (skills == null)
            {
                throw new ArgumentNullException("skills");
            }

            XmlNodeList spSkills = skills.SelectNodes("Skill");

            if (spSkills.Count > 0)
            {

                XmlNode ndName;
                XmlNode ndGradation;
                XmlNode ndDetails;

                foreach (XmlNode node in spSkills)
                {
                    ndName = node.SelectSingleNode("Name");
                    ndGradation = node.SelectSingleNode("Gradation");
                    ndDetails = node.SelectSingleNode("Details");


                    Skill newSkill = new Skill();

                    newSkill.CV = currCv;
                    newSkill.skill = Tools.EncodeXml(ndName.InnerXml);
                    if (ndGradation != null)
                    {
                        newSkill.gradation = Tools.EncodeXml(ndGradation.InnerXml);
                    }
                    if (ndDetails != null)
                    {
                        newSkill.details = Tools.EncodeXml(ndDetails.InnerXml);
                    }

                    objectContext.AddToSkillSet(newSkill);
                    Tools.Save(objectContext);

                }


            }


        }

        private void AddWorkExperience(CVsEntities objectContext, CV currCv, XmlNode workExperiance)
        {
            Tools.CheckObjectContext(objectContext);
            if (currCv == null)
            {
                throw new ArgumentNullException("currCv");
            }


            if (workExperiance == null)
            {
                return;
            }

            XmlNodeList spExperiance = workExperiance.SelectNodes("SpExperiance");

            if (spExperiance.Count > 0)
            {

                XmlNode ndFrom;
                XmlNode ndTo;
                XmlNode ndCompany;
                XmlNode ndPosition;
                XmlNode ndDetails;

                foreach (XmlNode node in spExperiance)
                {
                    ndFrom = node.SelectSingleNode("From");
                    ndTo = node.SelectSingleNode("To");
                    ndCompany = node.SelectSingleNode("Company");
                    ndPosition = node.SelectSingleNode("Position");
                    ndDetails = node.SelectSingleNode("Details");

                    DateTime from;
                    DateTime to = DateTime.MinValue;

                    if (DateTime.TryParse(ndFrom.InnerXml, out from) == false)
                    {
                        throw new ArgumentException("Couldn`t parse WorkExperiance > From  - to DateTime!");
                    }

                    if (ndTo != null && !string.IsNullOrEmpty(ndTo.InnerXml) && DateTime.TryParse(ndTo.InnerXml, out to) == false)
                    {
                        throw new ArgumentException("Couldn`t parse WorkExperiance > To  - to DateTime!");
                    }

                    WorkExperiance newExperiance = new WorkExperiance();

                    newExperiance.CV = currCv;

                    newExperiance.fromDate = from;
                    if (to > DateTime.MinValue)
                    {
                        newExperiance.toDate = to;
                    }

                    if (ndCompany != null)
                    {
                        newExperiance.companyName = Tools.EncodeXml(ndCompany.InnerXml);
                    }
                    newExperiance.position = Tools.EncodeXml(ndPosition.InnerXml);
                    if (ndDetails != null)
                    {
                        newExperiance.details = Tools.EncodeXml(ndDetails.InnerXml);
                    }

                    objectContext.AddToWorkExperianceSet(newExperiance);
                    Tools.Save(objectContext);

                }


            }


        }

        private void AddEducation(CVsEntities objectContext, CV currCv, XmlNode education)
        {
            Tools.CheckObjectContext(objectContext);
            if (currCv == null)
            {
                throw new ArgumentNullException("currCv");
            }


            if (education == null)
            {
                return;
            }

            XmlNodeList spEducation = education.SelectNodes("SpEducation");

            if (spEducation.Count > 0)
            {

                XmlNode ndFrom;
                XmlNode ndTo;
                XmlNode ndPlaceName;
                XmlNode ndLocation;
                XmlNode ndLevel;
                XmlNode ndMajor;
                XmlNode ndEduDetails;

                XmlNodeList eduSubject;
                XmlNode eduSubjects;


                foreach (XmlNode node in spEducation)
                {
                    ndFrom = node.SelectSingleNode("From");
                    ndTo = node.SelectSingleNode("To");
                    ndPlaceName = node.SelectSingleNode("PlaceName");
                    ndLocation = node.SelectSingleNode("Location");
                    ndLevel = node.SelectSingleNode("Level");
                    ndMajor = node.SelectSingleNode("Major");
                    ndEduDetails = node.SelectSingleNode("Details");

                    DateTime from;
                    DateTime to = DateTime.MinValue;

                    if (DateTime.TryParse(ndFrom.InnerXml, out from) == false)
                    {
                        throw new ArgumentException("Couldn`t parse Education > From  - to DateTime!");
                    }

                    if (ndTo != null && !string.IsNullOrEmpty(ndTo.InnerXml) && DateTime.TryParse(ndTo.InnerXml, out to) == false)
                    {
                        throw new ArgumentException("Couldn`t parse Education > To  - to DateTime!");
                    }

                    /////////

                    Education newEducation = new Education();

                    newEducation.CV = currCv;
                    newEducation.fromDate = from;
                    if (to > DateTime.MinValue)
                    {
                        newEducation.toDate = to;
                    }
                    newEducation.placeName = Tools.EncodeXml(ndPlaceName.InnerXml);
                    newEducation.location = Tools.EncodeXml(ndLocation.InnerXml);
                    

                    if (ndEduDetails != null)
                    {
                        newEducation.details = Tools.EncodeXml(ndEduDetails.InnerXml);
                    }
                    if (ndLevel != null)
                    {
                        newEducation.level = Tools.EncodeXml(ndLevel.InnerXml);
                    }
                    if (ndMajor != null)
                    {
                        newEducation.major = Tools.EncodeXml(ndMajor.InnerXml);
                    }


                    objectContext.AddToEducationSet(newEducation);
                    Tools.Save(objectContext);

                    ////////

                    eduSubjects = node.SelectSingleNode("Subjects");
                    if (eduSubjects != null)
                    {
                        eduSubject = eduSubjects.SelectNodes("Subject");
                        foreach (XmlNode subject in eduSubject)
                        {

                            if (!string.IsNullOrEmpty(subject.InnerXml))
                            {
                                Subject newSubject = new Subject();

                                newSubject.Education = newEducation;
                                newSubject.subject = Tools.EncodeXml(subject.InnerXml);

                                objectContext.AddToSubjectSet(newSubject);
                                Tools.Save(objectContext);
                            }
                        }
                    }


                }
            }


        }


        private bool IsThereAlreadySuchUserCv(CVsEntities objectContext, string firstName, string lastName, string email
            , string description, out bool isUserAdded)
        {
            Tools.CheckObjectContext(objectContext);
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("firstName or lastname or email or description is null or empty.");
            }

            isUserAdded = false;
            bool result = false;

            User user = objectContext.UserSet.FirstOrDefault(ucv => ucv.firstName == firstName && ucv.lastName == lastName && ucv.email == email);
            if (user != null)
            {
                isUserAdded = true;

                user.CVs.Load();
                foreach (CV cv in user.CVs)
                {
                    if (cv.description == description)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private bool ErrorsInSkills(StringBuilder sbErrors, XmlNode skills)
        {
            if (sbErrors == null)
            {
                throw new ArgumentNullException("sbErrors");
            }

            bool errorsInNodes = false;
            string elementError = string.Empty;

            if (skills == null)
            {
                sbErrors.Append("<br />&nbsp;&nbsp;&nbsp;No specified skills in the CV.");
                return true;
            }

            XmlNodeList spSkill = skills.SelectNodes("Skill");
            if (spSkill.Count > 0)
            {

                XmlNode ndName;
                XmlNode ndGradation;
                XmlNode ndDetails;

                List<string> skillNames = new List<string>();
                bool repeatingSkills = false;

                foreach (XmlNode node in spSkill)
                {
                    ndName = node.SelectSingleNode("Name");
                    ndGradation = node.SelectSingleNode("Gradation");
                    ndDetails = node.SelectSingleNode("Details");


                    if (IsElementValueCorrect(ndName, "Name", false, 200, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    else if(repeatingSkills == false)
                    {
                        if (skillNames.Contains(ndName.InnerXml) == true)
                        {
                            repeatingSkills = true;
                        }
                        else
                        {
                            skillNames.Add(ndName.InnerXml);
                        } 
                    }

                    if (IsElementValueCorrect(ndGradation, "Gradation", true, 100, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndDetails, "Details", true, long.MaxValue, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }

                }

                if (repeatingSkills == true)
                {
                    errorsInNodes = true;
                    sbErrors.Append("<br />&nbsp;&nbsp;&nbsp;There shouldn`t be repeating skills!");
                }
               
            }
            else
            {
                sbErrors.Append("<br />&nbsp;&nbsp;&nbsp;No specified skills in the CV.");
                errorsInNodes = true;
            }

            return errorsInNodes;
        }

        private bool ErrorsInWorkExperiance(StringBuilder sbErrors, XmlNode workExperiance)
        {
            if (sbErrors == null)
            {
                throw new ArgumentNullException("sbErrors");
            }

            bool errorsInNodes = false;
            string elementError = string.Empty;

            if (workExperiance == null)
            {
                return false;
            }

            XmlNodeList spWorkExperiance = workExperiance.SelectNodes("SpExperiance");
            if (spWorkExperiance.Count > 0)
            {

                XmlNode ndFrom;
                XmlNode ndTo;
                XmlNode ndCompany;
                XmlNode ndPosition;
                XmlNode ndDetails;

                foreach (XmlNode node in spWorkExperiance)
                {
                    ndFrom = node.SelectSingleNode("From");
                    ndTo = node.SelectSingleNode("To");
                    ndCompany = node.SelectSingleNode("Company");
                    ndPosition = node.SelectSingleNode("Position");
                    ndDetails = node.SelectSingleNode("Details");



                    if (IsElementValueCorrect(ndFrom, "From", false, 100, out elementError, true) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndTo, "To", true, 100, out elementError, true) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndCompany, "Company", true, 300, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndPosition, "Position", false, 300, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndDetails, "Details", true, long.MaxValue, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                   


                }
            }

            return errorsInNodes;
        }

        private bool ErrorsInEducation(StringBuilder sbErrors, XmlNode education)
        {
            if (sbErrors == null)
            {
                throw new ArgumentNullException("sbErrors");
            }

            bool errorsInNodes = false;
            string elementError = string.Empty;

            if (education == null)
            {
                return false;
            }

            XmlNodeList spEducation = education.SelectNodes("SpEducation");

            if (spEducation.Count > 0)
            {

                XmlNode ndFrom;
                XmlNode ndTo;
                XmlNode ndPlaceName;
                XmlNode ndLocation;
                XmlNode ndLevel;
                XmlNode ndMajor;
                XmlNode ndEduDetails;

                XmlNodeList eduSubject;
                XmlNode eduSubjects;


                foreach (XmlNode node in spEducation)
                {
                    ndFrom = node.SelectSingleNode("From");
                    ndTo = node.SelectSingleNode("To");
                    ndPlaceName = node.SelectSingleNode("PlaceName");
                    ndLocation = node.SelectSingleNode("Location");
                    ndLevel = node.SelectSingleNode("Level");
                    ndMajor = node.SelectSingleNode("Major");
                    ndEduDetails = node.SelectSingleNode("Details");


                    if (IsElementValueCorrect(ndFrom, "From", false, 100, out elementError, true) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndTo, "To", true, 100, out elementError, true) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndPlaceName, "PlaceName", false, 100, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndLocation, "Location", false, 100, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndLevel, "Level", true, 100, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndMajor, "Major", true, 100, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }
                    if (IsElementValueCorrect(ndEduDetails, "Details", true, long.MaxValue, out elementError, false) == false)
                    {
                        errorsInNodes = true;
                        sbErrors.Append(elementError);
                    }

                    eduSubjects = node.SelectSingleNode("Subjects");
                    if (eduSubjects != null)
                    {
                        eduSubject = eduSubjects.SelectNodes("Subject");
                        foreach (XmlNode subject in eduSubject)
                        {
                            if (IsElementValueCorrect(subject, "Subject", true, 100, out elementError, false) == false)
                            {
                                errorsInNodes = true;
                                sbErrors.Append(elementError);
                            }
                        }
                    }


                }
            }

            return errorsInNodes;
        }


        private bool ErrorsInMainElements(StringBuilder sbErrors , XmlNode ndFirstName, XmlNode ndLastName, XmlNode ndEmail, XmlNode ndYearBorn, 
                    XmlNode ndNationality, XmlNode ndCity, XmlNode ndDetails, XmlNode ndGender)
        {
            if (sbErrors == null)
            {
                throw new ArgumentNullException("sbErrors");
            }

            bool errorsInNodes = false;
            string elementError = string.Empty;


            if (IsElementValueCorrect(ndFirstName, "FirstName", false, 100, out elementError, false) == false)
            {
                errorsInNodes = true;
                sbErrors.Append(elementError);
            }

            if (IsElementValueCorrect(ndLastName, "LastName", false, 100, out elementError, false) == false)
            {
                errorsInNodes = true;
                sbErrors.Append(elementError);
            }

            if (IsElementValueCorrect(ndEmail, "Email", false, 300, out elementError, false) == false)
            {
                errorsInNodes = true;
                sbErrors.Append(elementError);
            }
            else
            {
                if (Tools.EmailValidatorPassed(ndEmail.InnerXml) == false)
                {
                    errorsInNodes = true;
                    sbErrors.Append("<br />&nbsp;&nbsp;&nbsp;Incorrect email format!");
                }
            }

            if (IsElementValueCorrect(ndYearBorn, "YearBorn", false, long.MaxValue, out elementError, false) == false)
            {
                errorsInNodes = true;
                sbErrors.Append(elementError);
            }
            else
            {
                int year = 1;
                if (int.TryParse(ndYearBorn.InnerXml, out year) == false)
                {
                    errorsInNodes = true;
                    sbErrors.Append("<br />&nbsp;&nbsp;&nbsp;Incorrect year born format!");
                }
                else
                {
                    if (year >= DateTime.UtcNow.Year)
                    {
                        errorsInNodes = true;
                        sbErrors.Append("<br />&nbsp;&nbsp;&nbsp;Are you sure that you are born in the future?");
                    }
                }
            }

            if (IsElementValueCorrect(ndNationality, "Nationality", false, 100, out elementError, false) == false)
            {
                errorsInNodes = true;
                sbErrors.Append(elementError);
            }

            if (IsElementValueCorrect(ndCity, "LiveInCity", false, 100, out elementError, false) == false)
            {
                errorsInNodes = true;
                sbErrors.Append(elementError);
            }

            if (IsElementValueCorrect(ndDetails, "Details", true, long.MaxValue, out elementError, false) == false)
            {
                errorsInNodes = true;
                sbErrors.Append(elementError);
            }

            if (IsElementValueCorrect(ndGender, "Gender", false, 100, out elementError, false) == false)
            {
                errorsInNodes = true;
                sbErrors.Append(elementError);
            }
            else
            {
                string gender = ndGender.InnerXml.ToLower();
                if (gender != "male" && gender != "female" && gender != "мъж" && gender != "жена")
                {
                    errorsInNodes = true;
                    sbErrors.Append("<br />&nbsp;&nbsp;&nbsp;Gender must be male/female or мъж/жена!");
                }
            }


            return errorsInNodes;
        }



        private bool IsElementValueCorrect(XmlNode element, string elName, bool canBeEmpty, long maxLength, out string error, bool tryParseToDateTime)
        {

            if (string.IsNullOrEmpty(elName))
            {
                throw new ArgumentNullException("elName is empty.");
            }

            if (maxLength < 1)
            {
                throw new ArgumentOutOfRangeException("maxLength < 1");
            }

            bool valueCorrect = false;
            error = string.Empty;

            if (element != null)
            {
                if (string.IsNullOrEmpty(element.InnerXml))
                {
                    if (canBeEmpty == true)
                    {
                        valueCorrect = true;
                    }
                    else
                    {
                        error = string.Format("<br />&nbsp;&nbsp;&nbsp;Missing element ' {0} ' value!", elName);
                    }
                }
                else
                {
                    if (element.InnerXml.Length > maxLength)
                    {
                        error = string.Format("<br />&nbsp;&nbsp;&nbsp;Element ' {0} ' description cannot be bigger than {1} symbols!", elName, maxLength);
                    }
                    else
                    {
                        if (tryParseToDateTime == true)
                        {
                            DateTime time;
                            if (DateTime.TryParse(element.InnerXml, out time) == true)
                            {
                                valueCorrect = true;
                            }
                            else
                            {
                                error = string.Format("<br />&nbsp;&nbsp;&nbsp;Incorrect date format of element ' {0} '! Format must be : yyyy/dd/mm", elName);
                            }
                        }
                        else
                        {
                            valueCorrect = true;
                        }

                    }
                }
            }
            else
            {
                if (canBeEmpty == true)
                {
                    valueCorrect = true;
                }
                else
                {
                    error = string.Format("<br />&nbsp;&nbsp;&nbsp;Missing element ' {0} ' in the CV!", elName);
                }
            }

            return valueCorrect;
        }


    }
}