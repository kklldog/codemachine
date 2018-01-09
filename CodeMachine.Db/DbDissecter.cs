using System;
using System.Configuration;
using CodeMachine.Db.DbDeissecters;
using CodeMachine.Db.Models;

namespace CodeMachine.Db
{
    public class DbDissecter
    {
        public static IDbDissecter Get(string connectionName)
        {
            var connectionSetting = ConfigurationManager.ConnectionStrings[connectionName];

            if (string.IsNullOrEmpty(connectionSetting.ProviderName))
            {
                return new SqlserverDissecter(connectionSetting.ConnectionString);
            }  

            if (connectionSetting.ProviderName.Equals(DbProviders.Sqlserver,StringComparison.CurrentCultureIgnoreCase))
            {
                return new SqlserverDissecter(connectionSetting.ConnectionString);
            }
            else if (connectionSetting.ProviderName.Equals(DbProviders.Oracle, StringComparison.CurrentCultureIgnoreCase))
            {
                return new OracleDissecter(connectionSetting.ConnectionString);
            }
            else
            {
                throw  new Exception(string.Format("Not implement dissecter for provider:{0}", connectionSetting.ProviderName));
            }
        }
    }
}
