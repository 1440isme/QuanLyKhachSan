using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    [Serializable]
    public partial class Entities
    {
        private Entities(DbConnection connectionString, bool contextOwnsConnection = true)
            : base(connectionString, contextOwnsConnection) { }
        public static Entities CreateEntities(bool contextOwnsConnection = true)
        {
            //Doc file connect
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open("connectdb.dba", FileMode.Open, FileAccess.Read);
            connect cp = (connect)bf.Deserialize(fs);

            //Decrypt noi dung
            string servername = Encryptor.Decrypt(cp.servername, "qwertyuiop", true);
            string username = Encryptor.Decrypt(cp.username, "qwertyuiop", true);
            string pass = Encryptor.Decrypt(cp.passwd, "qwertyuiop", true);
            string database = Encryptor.Decrypt(cp.database, "qwertyuiop", true);


            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            SqlConnectionStringBuilder sqlConnectBuiler = new SqlConnectionStringBuilder();
            sqlConnectBuiler.DataSource = servername;
            sqlConnectBuiler.InitialCatalog = database;
            sqlConnectBuiler.UserID = username;
            sqlConnectBuiler.Password = pass;

            string sqlConnectionString = sqlConnectBuiler.ConnectionString;

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = sqlConnectionString;

            entityBuilder.Metadata = @"res://*/KhachSan.csdl|res://*/KhachSan.ssdl|res://*/KhachSan.msl";

            EntityConnection connection = new EntityConnection(entityBuilder.ConnectionString);

            fs.Close();
            return new Entities(connection);
        }
    }
}
