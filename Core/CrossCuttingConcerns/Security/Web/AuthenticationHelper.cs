using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Security;

namespace Core.CrossCuttingConcerns.Security.Web
{
    public class AuthenticationHelper
    {
        public static void CreateAuthCookie(Guid id, string userName, DateTime expiration, string[] roles,
            bool rememberMe, string firstName, string lastName)
        {

            var authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, expiration, rememberMe,
                CreatAuthTags(firstName, roles, lastName, id));
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }
        /// <summary>
        /// Cookie eklenecek verilerin
        /// belirli bir düzen içinde
        /// eklenmesini saglar.
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static string CreatAuthTags(string firstName, string[] roles, string lastName, Guid id)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(firstName);
            stringBuilder.Append("|");
            for (int i = 0; i < roles.Length; i++)
            {
                //Admin,Editor
                stringBuilder.Append(roles[i]);
                if (i < roles.Length - 1)
                {
                    stringBuilder.Append(",");
                }

            }
            stringBuilder.Append("|");
            stringBuilder.Append(lastName);
            stringBuilder.Append("|");
            stringBuilder.Append(id);

            return stringBuilder.ToString();
        }


    }
}
