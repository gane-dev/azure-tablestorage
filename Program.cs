using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure;
using System.Configuration;
namespace TableStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // UpdateRecords(tableClient);
            DeleteRecords(tableClient);
            RecordsByPartition(tableClient);
            Console.Read();
        }
        private static void RecordsByPartition(CloudTableClient tableClient)
        {
            CloudTable table = tableClient.GetTableReference("orders");
            TableQuery<OrderEntity> query = new TableQuery<OrderEntity>().Where(
TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Lana"));

            foreach (OrderEntity entity in table.ExecuteQuery(query))
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey,
                entity.Status, entity.RequiredDate);
            }
            Console.ReadKey();
        }
        private static void UpdateRecords(CloudTableClient tableClient)
        {
            CloudTable table = tableClient.GetTableReference("orders");
            TableOperation retrieveOperation = TableOperation.Retrieve<OrderEntity>("Lana",
"20141217");
            TableResult retrievedResult = table.Execute(retrieveOperation);
            OrderEntity updateEntity = (OrderEntity)retrievedResult.Result;
            if (updateEntity != null)
            {
                updateEntity.Status = "shipped";
                updateEntity.ShippedDate = Convert.ToDateTime("12/20/2014");
                TableOperation insertOrReplaceOperation = TableOperation.
              InsertOrReplace(updateEntity);
                table.Execute(insertOrReplaceOperation);
            }
       
}
        private static void DeleteRecords(CloudTableClient tableClient)
        {
            CloudTable table = tableClient.GetTableReference("orders");
            TableOperation retrieveOperation = TableOperation.Retrieve<OrderEntity>("Lana",
"20141217");
            TableResult retrievedResult = table.Execute(retrieveOperation);
            OrderEntity updateEntity = (OrderEntity)retrievedResult.Result;
            TableOperation deleteOperation = TableOperation.Delete(updateEntity);
            table.Execute(deleteOperation);
            Console.WriteLine("Entity deleted.");
        }
    private static void InsertRecords(CloudTableClient tableClient)
        {
            CloudTable table = tableClient.GetTableReference("orders");
            table.CreateIfNotExists();




            OrderEntity newOrder = new OrderEntity("Archer", "20141216");

            newOrder.OrderNumber = "101";

            newOrder.ShippedDate = Convert.ToDateTime("12/18/2017");

            newOrder.RequiredDate = Convert.ToDateTime("12/14/2017");

            newOrder.Status = "shipped";

            TableOperation insertOperation = TableOperation.Insert(newOrder);

            table.Execute(insertOperation);


            TableBatchOperation batchOperation = new TableBatchOperation();

            OrderEntity newOrder1 = new OrderEntity("Lana", "20141217");
            newOrder1.OrderNumber = "102";
            newOrder1.ShippedDate = Convert.ToDateTime("1/1/1900");
            newOrder1.RequiredDate = Convert.ToDateTime("1/1/1900");
            newOrder1.Status = "pending";
            OrderEntity newOrder2 = new OrderEntity("Lana", "20141218");
            newOrder2.OrderNumber = "103";
            newOrder2.ShippedDate = Convert.ToDateTime("1/1/1900");
            newOrder2.RequiredDate = Convert.ToDateTime("12/25/2014");
            newOrder2.Status = "open";
            OrderEntity newOrder3 = new OrderEntity("Lana", "20141219");
            newOrder3.OrderNumber = "103";
            newOrder3.ShippedDate = Convert.ToDateTime("12/17/2014");
            newOrder3.RequiredDate = Convert.ToDateTime("12/17/2014");
            newOrder3.Status = "shipped";
            batchOperation.Insert(newOrder1);
            batchOperation.Insert(newOrder2);
            batchOperation.Insert(newOrder3);
            table.ExecuteBatch(batchOperation);
        }
    }
}
