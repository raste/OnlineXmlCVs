// Online XML Cvs (https://github.com/raste/OnlineXmlCVs)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

namespace UploadXmlCvsToSite
{
    public class GetCVs
    {

        public User GetUser(CVsEntities objectContext, string firstName, string lastName, string email)
        {
            Tools.CheckObjectContext(objectContext);

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) )
            {
                throw new ArgumentException("firstName or lastname or email is null or empty.");
            }

            User user = objectContext.UserSet.FirstOrDefault(usr => usr.firstName == firstName && usr.lastName == lastName && usr.email == email);

            return user;
        }

        public List<User> GetAllUsers(CVsEntities objectContext)
        {
            Tools.CheckObjectContext(objectContext);

            List<User> users = objectContext.UserSet.ToList();

            return users;
        }

        public CV GetCV(CVsEntities objectContext, long id, bool throwExcIfNull)
        {
            Tools.CheckObjectContext(objectContext);
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException("id < 1");
            }

            CV wantedCv = objectContext.CVSet.FirstOrDefault(ccv => ccv.ID == id);

            if (wantedCv == null && throwExcIfNull == true)
            {
                throw new ArgumentException(string.Format("There is no CV with ID = {0}", id));
            }

            return wantedCv;
        }

        public List<CV> GetAllUserCVs(CVsEntities objectContext, User user)
        {
            Tools.CheckObjectContext(objectContext);
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.CVs.Load();

            List<CV> cvs = user.CVs.ToList();

            return cvs;
        }
    }
}