using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace EasyBase.DataLayer
{
	public static partial class Settings
	{
		public static string ConnectionString
		{
            get {
                if (Environment.CommandLine.Contains(" /developer"))
                {
                    return (string)ConfigurationManager.ConnectionStrings["DeveloperConnectionString"].ToString();
                }
                else if (Environment.CommandLine.Contains(" /demo"))
                {
                    return (string)ConfigurationManager.ConnectionStrings["DemoConnectionString"].ToString();
                }
                else
                {
                    //return (string)ConfigurationManager.ConnectionStrings["ReleaseConnectionString"].ToString();
                    return (string)ConfigurationManager.ConnectionStrings["DeveloperConnectionString"].ToString();
                }
            }
		}

	    public static string DatabaseName
	    {
	        get
	        {
	            int startIndex = ConnectionString.IndexOf("Catalog=") + 8;
	            int length = ConnectionString.IndexOf(";", startIndex) - startIndex;
                string name = ConnectionString.Substring(startIndex, length);
	            return name;
	        }
	    }

        public static string TestConnectionString
        {
            get
            {
                return (string)ConfigurationManager.ConnectionStrings["TestConnectionString"].ToString();
            }
        }
	}
}
