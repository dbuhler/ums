using System.Data.SqlClient;


namespace API
{
    /// <summary>
    /// This API provides basic access to the Database representing a Union. It has Classes for all tables,
    /// including Persons, Members, Organizations, Plans, Contributions and more. It is designed for 
    /// use specifically with the database defined as part of this project in Schemas/core.sql and 
    /// Schemas/views.sql. Schemas/triggers.sql adds a mechanism for keeping a history of changes made to 
    /// Member, Person and PlanMember tables.
    /// 
    /// Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
    /// December 01, 2014
    /// </summary>
    public static class Repository
    {
        /// <summary>
        /// Creates a new 'connection string' from a host name, database name, 
        /// username and password.
        /// </summary>
        /// <param name="host">Host name</param>
        /// <param name="database">Database name</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Connection string</returns>
        public static string GetConnectionString(string host, string database, string username, string password)
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder();

            builder.DataSource          = host;
            builder.InitialCatalog      = database;
            builder.PersistSecurityInfo = true;
            builder.UserID              = username;
            builder.Password            = password;

            return builder.ConnectionString;
        }


        /// <summary>
        /// Creates an API instance from the specified connection string. 
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Connection</returns>
        public static Connection OpenConnection(string connectionString)
        {
            return new Connection(connectionString);
        }


        /// <summary>
        /// Creates an API instance by building a connection string 
        /// with a host name, database name, username and password specified by user.
        /// </summary>
        /// <param name="host">Host name</param>
        /// <param name="database">Database name</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>API instance</returns>
        public static Connection OpenConnection(string host, string database, string username, string password)
        {
            return OpenConnection(GetConnectionString(host, database, username, password));
        }
    }
}