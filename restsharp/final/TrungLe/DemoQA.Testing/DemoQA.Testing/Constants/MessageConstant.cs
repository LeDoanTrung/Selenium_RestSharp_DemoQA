﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Testing.Constants
{
    public class MessageConstant
    {
        //Message for Book Request
        public const string DuplicatedBookMessage = "ISBN already present in the User's Collection!";
        public const string NonExistingInUserCollectionMessage = "ISBN supplied is not available in User's Collection!";
        public const string NonExistingInBookCollectionMessage = "ISBN supplied is not available in Books Collection!";

        //Message for User Request
        public const string UserUnauthorizedMessage = "User not authorized!";
    }
}
