

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace TableStorage
{
    class QueueExample
    {
        public static void AddQueue(CloudStorageAccount storageAccount)
        {
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
             

            CloudQueue queue = queueClient.GetQueueReference("queue");
            queue.CreateIfNotExists();
            queue.AddMessage(new CloudQueueMessage("Queued message 1"));
            queue.AddMessage(new CloudQueueMessage("Queued message 2"));
            queue.AddMessage(new CloudQueueMessage("Queued message 3"));
        }
    }
}
