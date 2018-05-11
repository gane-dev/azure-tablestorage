using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace TableStorage
{
    class OrderEntity:TableEntity
    {
        public OrderEntity(string customerName, string orderDate)
        {
            this.PartitionKey = customerName;
            this.RowKey = orderDate;
        }
        public OrderEntity() { }
        public string OrderNumber { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string Status { get; set; }
    }
}
