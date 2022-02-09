using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Core.CrossCuttingConcerns.Security.Web
{
   public class SecurityUtilities
    {
        public Identity FormsAuthTicketToIdentity(FormsAuthenticationTicket ticket)
        {
            var identity = new Identity
            {
                Id=SetId(ticket),
                Name = SetName(ticket),
                Roles = SetRoles(ticket),
                FirsName = SetFirstName(ticket),
                LastName = SetLastName(ticket),
                AuthenticationType = SetAuthType(),
                IsAuthenticated = SetIsAuthenticated()
            };
            return identity;
        }

        private bool SetIsAuthenticated()
        {
            return true;
        }

        private string SetAuthType()
        {
            return "Forms";
        }
        
        private string SetLastName(FormsAuthenticationTicket ticket)
        {
            //CreatAuthTags(string firstName, string[] roles, string lastName, Guid id)
            string[] data = ticket.UserData.Split('|');
            return data[2];
        }

        private string SetFirstName(FormsAuthenticationTicket ticket)
        {
            //CreatAuthTags(string firstName, string[] roles, string lastName, Guid id)
            string[] data = ticket.UserData.Split('|');
            return data[0];
        }

        private string[] SetRoles(FormsAuthenticationTicket ticket)
        {
            //CreatAuthTags(string firstName, string[] roles, string lastName, Guid id)
            string[] data = ticket.UserData.Split('|');
            string[] roles = data[1].Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            return roles;
        }

       

        private string SetName(FormsAuthenticationTicket ticket)
        {
            return ticket.Name;
        }

        private Guid SetId(FormsAuthenticationTicket ticket)
        {
            //CreatAuthTags(string firstName, string[] roles, string lastName, Guid id)
            string[] data = ticket.UserData.Split('|');
            return new Guid(data[3]);
        }
    }
}
