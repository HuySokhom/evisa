using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;

namespace eVisa.Controllers
{
    public class UserManager
    {
        public bool IsValid(string user_id, string password)
        {
            using ( var db = new eVisaContext() )
            {
                var hash = Crypto.Hash(password);
                return db.Users.Any(u => u.user_id == user_id && u.password == hash);
            }
        }

    }
}