using Confluent.Kafka;
using DebeziumTest.Data.Common;
using DebeziumTest.Data.Entities;
using DebeziumTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace DebeziumTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context();

            ConsumerConfig config = new()
            {
                GroupId = "exampleserver.dbo.Hats",
                BootstrapServers = "localhost:29092",
                AutoOffsetReset = AutoOffsetReset.Earliest,

            };

            using IConsumer<Ignore, string> consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("exampleserver.dbo.Hats");

            CancellationTokenSource source = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                source.Cancel();
            };
            while (true)
            {
                ConsumeResult<Ignore, string> result = consumer.Consume(source.Token);

                if (result.Message?.Value == null)
                {
                    Console.WriteLine("Message is null.");
                    continue;
                }
                var jsonModel = JsonConvert.DeserializeObject<JsonModel<Hat>>(result.Message.Value);
                var payload = jsonModel?.Payload;

                if (payload == null) continue;

                if (payload?.Op == "c")
                {
                    if (payload.After == null) continue;

                    var hat = new Hat { Id = payload.After.Id, Name = payload.After.Name, HatType = payload.After.HatType };
                    context.Hats.Add(hat);
                    context.SaveChangesWithIdentityInsert();
                    Console.WriteLine($"{hat.Name} created.");
                }
                else if (payload?.Op == "u")
                {
                    if (payload.Before == null || payload.After == null) continue;

                    var hat = context.Hats.Find(payload.Before.Id);
                    if (hat == null) continue;

                    hat.Name = payload.After.Name;
                    hat.HatType = payload.After.HatType;
                    context.Entry(hat).State = EntityState.Modified;
                    context.SaveChanges();
                    Console.WriteLine($"{hat.Name} updated.");
                }
                else if (payload?.Op == "d")
                {
                    if (payload.Before == null) continue;

                    var hat = context.Hats.Find(payload.Before.Id);
                    if (hat == null) continue;

                    context.Remove(hat);
                    context.SaveChanges();
                    Console.WriteLine($"{hat.Name} deleted.");
                }
            }
        }
    }
}
