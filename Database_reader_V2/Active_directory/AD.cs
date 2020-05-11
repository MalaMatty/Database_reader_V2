using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows;
using System.DirectoryServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace Database_reader_V2.Active_directory
{
    public class AD
    {
         private string GetCurrentDomainPath()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://localhost:389");

            return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
        }
           

            private void GetAllUsers()
            {
                SearchResultCollection results;
                DirectorySearcher ds = null;
                DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath());

                ds = new DirectorySearcher(de);
                ds.Filter = "(&(objectCategory=User)(objectClass=person))";

                results = ds.FindAll();

                foreach (SearchResult sr in results)
                {
                    Debug.WriteLine(sr.Properties["name"][0].ToString());
                    // The following is NOT available
                    // Debug.WriteLine(sr.Properties["mail"][0].ToString());
                }
            }
        private void btnLogin_Click(object sender)
        {
            // Authenticate user
            if (AuthenticateUser("localhost:389",
                          "adm1",
                          "sidel1"))
                
            {
                //DialogResult = true;
                MessageBox.Show("Credenziali giuste");
            }
            else
                MessageBox.Show("Unable to Authenticate Using the Supplied Credentials");
        }

        private bool AuthenticateUser(string domainName, string userName, string password)
        {
            bool ret = false;

            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://" + domainName,
                                    userName, password);
                DirectorySearcher dsearch = new DirectorySearcher(de);
                SearchResult results = null;

                results = dsearch.FindOne();

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return ret;
        }
      
    }

    }