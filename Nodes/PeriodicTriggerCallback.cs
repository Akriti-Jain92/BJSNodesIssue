using Microsoft.WindowsAzure.ResourceStack.Common.BackgroundJobs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nodes
{
    [JobCallback(Name = "JobCallback5min")]
    public class JobCallback5min : JobDelegate
    {
        public async override Task<JobExecutionResult> ExecuteAsync(JobExecutionContext context, CancellationToken token)
        {
            Console.WriteLine("I am CallBack for 5 mins: " + DateTime.Now);
            System.Diagnostics.Debug.WriteLine("I am CallBack for 5 mins: " + DateTime.Now);
            var can = token.IsCancellationRequested;
            //string fullPath = String.Format("C:\\Users\\akritijain\\Desktop\\BJS\\output.txt");
            //using (StreamWriter writer = File.AppendText(fullPath))
            //{
            //    writer.WriteLine("I am CallBack for 5 mins: " + DateTime.Now);
            //}
            //HttpClient client = new HttpClient();
            //HttpContent? content = null;
            //String path = String.Format("http://localhost:8376/WeatherForecast");
            //await client.PostAsync(
            //    path, content);

            return await Task.FromResult(new JobExecutionResult { Status = JobExecutionStatus.Succeeded });
        }
    }
}
