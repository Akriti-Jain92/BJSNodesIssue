using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.WindowsAzure.ResourceStack.Common.BackgroundJobs;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nodes
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Nodes : StatelessService
    {
        public Nodes(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            System.Diagnostics.Debug.WriteLine("CreateServiceInstanceListeners");
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {

            System.Diagnostics.Debug.WriteLine("RunAsync");

            var jobBuilder1 = JobBuilder.Create(jobPartition: "periodictrigger", jobId: "periodictrigger")
              .WithCallback(typeof(JobCallback5min))
              .WithRepeatStrategy(TimeSpan.FromMinutes(5))
              .WithRetryStrategy(2, TimeSpan.FromMinutes(1));

            var managementClient1 = new JobManagementClient(
              //connectionString: String.Format("DefaultEndpointsProtocol=https;AccountName=octesturscale1nambjs1;AccountKey=zJ9/121MJs4gan4gf8L/7g/PeRUL0rXl3VvczoNSNKYeNaZMWdUjZykmcz1xGSWpwtbNnLqOiK1z+AStsmHKdw==;EndpointSuffix=core.windows.net"),
              connectionString: String.Format("DefaultEndpointsProtocol=https;AccountName=akservicetest;AccountKey=gDO3Zl0qPvyTUvxYl+j/X5+FsntRGl1+NmXtCZH6aarw31QzcoPoR6IEpcVwijMaD9DSbOPdo4Gw+ASt/ZHEFg==;EndpointSuffix=core.windows.net"),
              executionAffinity: String.Empty /*"5min1"*/,
              eventSource: new EventSourceTest(),
              queueNamePrefix: "periodictrigger",
              tableName: "periodictrigger",
              encryptionUtility: null);

            if(await managementClient1.GetJob(jobPartition: "periodictrigger", jobId: "periodictrigger").ConfigureAwait(false) == null)
            {
                await managementClient1
                  .CreateJob(jobBuilder1)
                  .ConfigureAwait(continueOnCapturedContext: false);

                var dispatcherClient1 = new JobDispatcherClient(
                  //connectionString: String.Format("DefaultEndpointsProtocol=https;AccountName=octesturscale1nambjs1;AccountKey=zJ9/121MJs4gan4gf8L/7g/PeRUL0rXl3VvczoNSNKYeNaZMWdUjZykmcz1xGSWpwtbNnLqOiK1z+AStsmHKdw==;EndpointSuffix=core.windows.net"),
                  connectionString: String.Format("DefaultEndpointsProtocol=https;AccountName=akservicetest;AccountKey=gDO3Zl0qPvyTUvxYl+j/X5+FsntRGl1+NmXtCZH6aarw31QzcoPoR6IEpcVwijMaD9DSbOPdo4Gw+ASt/ZHEFg==;EndpointSuffix=core.windows.net"),
                  executionAffinity: String.Empty /*"5min1"*/,
                  eventSource: new EventSourceTest(),
                  tableName: "periodictrigger",
                  queueNamePrefix: "periodictrigger",
                  encryptionUtility: null);

                dispatcherClient1.RegisterJobCallback(typeof(JobCallback5min));

                dispatcherClient1.ProvisionSystemConsistencyJob().Wait();

                dispatcherClient1.Start();
            }
            //else
            //{
            //    var dispatcherClient1 = new JobDispatcherClient(
            //      //connectionString: String.Format("DefaultEndpointsProtocol=https;AccountName=octesturscale1nambjs1;AccountKey=zJ9/121MJs4gan4gf8L/7g/PeRUL0rXl3VvczoNSNKYeNaZMWdUjZykmcz1xGSWpwtbNnLqOiK1z+AStsmHKdw==;EndpointSuffix=core.windows.net"),
            //      connectionString: String.Format("DefaultEndpointsProtocol=https;AccountName=akservicetest;AccountKey=gDO3Zl0qPvyTUvxYl+j/X5+FsntRGl1+NmXtCZH6aarw31QzcoPoR6IEpcVwijMaD9DSbOPdo4Gw+ASt/ZHEFg==;EndpointSuffix=core.windows.net"),
            //      executionAffinity: String.Empty /*"5min1"*/,
            //      eventSource: new EventSourceTest(),
            //      tableName: "periodictrigger",
            //      queueNamePrefix: "periodictrigger",
            //      encryptionUtility: null);

            //    dispatcherClient1.RegisterJobCallback(typeof(JobCallback5min));

            //    dispatcherClient1.ProvisionSystemConsistencyJob().Wait();

            //    dispatcherClient1.Start();
            //}
            

            //while (!cancellationToken.IsCancellationRequested)
            //{
            //}
            //System.Diagnostics.Debug.WriteLine("IsCancellationRequested");
        }

        protected override async Task OnCloseAsync(CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine("OnCloseAsync");
        }

        protected override void OnAbort()
        {
            System.Diagnostics.Debug.WriteLine("OnAbort");
        }

    }
}
