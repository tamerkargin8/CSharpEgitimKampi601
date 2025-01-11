using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        private readonly MongoDbConnection _connection;

        public CustomerOperations(MongoDbConnection connection)
        {
            _connection = connection;
        }

        public void AddCustomer(Customer customer)
        {
            var customerCollection = _connection.GetCustomersCollection();
            var document = new BsonDocument
                {
                    {"CustomerName", customer.CustomerName},
                    {"CustomerSurname", customer.CustomerSurname},
                    {"CustomerCity", customer.CustomerCity},
                    {"CustomerBalance", customer.CustomerBalance},
                    {"CustomerShoppingCount", customer.CustomerShoppingCount}
                };
            customerCollection.InsertOne(document);
        }

        public List<Customer> GetAllCustomer()
        {
            var customerCollection = _connection.GetCustomersCollection();
            var customers = customerCollection.Find(new BsonDocument()).ToList();
            List<Customer> customerList = new List<Customer>();
            foreach (var c in customers)
            {
                customerList.Add(new Customer()
                {
                    CustomerId = c["_id"].ToString(),
                    CustomerName = c["CustomerName"].ToString(),
                    CustomerSurname = c["CustomerSurname"].ToString(),
                    CustomerCity = c["CustomerCity"].ToString(),
                    CustomerBalance = c["CustomerBalance"].AsDecimal,
                    CustomerShoppingCount = c["CustomerShoppingCount"].AsInt32
                });
            }
            return customerList;
        }
        public void DeleteCustomer(string customerId)
        {
            var customerCollection = _connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customerId));
            customerCollection.DeleteOne(filter);
            //customerCollection.DeleteOne(new BsonDocument("_id", new ObjectId(customerId))); tek kod hali
        }
        public void UpdateCustomer(Customer customer)
        {
            var customerCollection = _connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId));
            var update = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerShoppingCount", customer.CustomerShoppingCount);
            customerCollection.UpdateOne(filter, update);
        }
        public Customer GetCustomerByID(string customerId)
        {
            var customerCollection = _connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customerId));
            var result = customerCollection.Find(filter).FirstOrDefault();
            return new Customer()
            {
                CustomerId = result["_id"].ToString(), //customer
                CustomerName = result["CustomerName"].ToString(),
                CustomerSurname = result["CustomerSurname"].ToString(),
                CustomerCity = result["CustomerCity"].ToString(),
                CustomerBalance = result["CustomerBalance"].AsDecimal, //decimal.Parse(result["CustomerBalance"].ToString())
                CustomerShoppingCount = result["CustomerShoppingCount"].AsInt32 // int.Parse(result["CustomerShoppingCount"].ToString())
            };
        }
    }
}
