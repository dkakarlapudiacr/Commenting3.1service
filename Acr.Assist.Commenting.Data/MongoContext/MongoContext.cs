using MongoDB.Driver;

namespace Acr.Assist.CommentMicroService.Data.MongoContext
{
    public class MongoContext
    {
        /// <summary>
        /// The client
        /// </summary>
        private MongoClient Client;

        /// <summary>
        /// The mongo database
        /// </summary>
        private IMongoDatabase mongoDB;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dataBase">The data base.</param>
        public MongoContext(string connectionString, string dataBase)
        {           
            Client = new MongoClient(connectionString);
            mongoDB = Client.GetDatabase(dataBase);   
        }

        /// <summary>
        /// Gets or sets the data base.
        /// </summary>
        /// <value>
        /// The data base.
        /// </value>
        public IMongoDatabase DataBase { get => mongoDB; set => mongoDB = value; }
    }
}
