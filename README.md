# Online Xml CVs

### About

June 2011

A simple site for uploading and browsing CVs in XML format.

It was given as recruitment task with the following objectives:
-  Page for uploading CV in XML format, which is stored in database.
-  Page with list of already uploaded CVs, with person's name and link to detailed CV page.
-  Page for detailed view of the CV.
-  Development period of up to 2 weeks.

It was written in 3 days. The project also has validation of the XML's (no XSD/DTD used) with detailed error messages and option to upload CV's in English and Bulgarian languages.

### Technologies

.NET 4.0, Web Forms, LINQ, Entity Framework, C#

### Poke/Edit

In order to see the code you will have to open the [Source/UploadXmlCvs.sln](https://github.com/raste/OnlineXmlCVs/blob/master/Source/UploadXmlCvs.sln) file with Visual Studio 2010 or greater.

To run the site from Visual Studio: 
- Set [UploadCV.aspx](https://github.com/raste/OnlineXmlCVs/blob/master/Source/UploadXmlCvs/UploadCV.aspx) as start page 
- Make sure tou have Microsoft SQL Server 2008 or greater installed. 
- Create the database from the script files ([DB/DbScriptNoData.sql](https://github.com/raste/OnlineXmlCVs/blob/master/DB/DbScriptNoData.sql) or [DB/DbScriptWithData.sql](https://github.com/raste/OnlineXmlCVs/blob/master/DB/DbScriptWithData.sql)) or restore it from the backup files ([DB/DbWithNoEnteredData.bak](https://github.com/raste/OnlineXmlCVs/blob/master/DB/DbWithNoEnteredData.bak) or [DB/DbWithEnteredData.bak](https://github.com/raste/OnlineXmlCVs/blob/master/DB/DbWithEnteredData.bak)).
- Update the database connection string in [Source/UploadXmlCvs/Web.config file](https://github.com/raste/OnlineXmlCVs/blob/master/Source/UploadXmlCvs/Web.config). Replace "NAME" in `Data Source=NAME;` with the name of your SQL Server. Replace "CVs" in `Initial Catalog=CVs;` with the application database name. If the database is password protected add `user id=dbUser;password=userPass;` right after `Initial Catalog=PermissionsManagement;` section and replace "dbUser" with the database user and "userPass" with his password.
- Test the upload function with the XML files in [XML files folder](https://github.com/raste/OnlineXmlCVs/tree/master/XML%20files) 

### Images

![alt text](https://github.com/raste/OnlineXmlCVs/blob/master/screenshots/Upload.png "Upload screen")

![alt text](https://github.com/raste/OnlineXmlCVs/blob/master/screenshots/Validation.png "Validation")

![alt text](https://github.com/raste/OnlineXmlCVs/blob/master/screenshots/AllCVs.png "Uploaded CVs")

![alt text](https://github.com/raste/OnlineXmlCVs/blob/master/screenshots/CV.png "CV")

![alt text](https://github.com/raste/OnlineXmlCVs/blob/master/screenshots/XMLFormat.png "XML format description")
