using Camunda.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Camunda_Lesson2_Example1.Handlers
{
    [HandlerTopics("CookingReceipts", LockDuration = 10000)]
    [HandlerVariables("FoodSelector")]
    public class GetCookingReceiptsHandler : IExternalTaskHandler
    {
        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            if (!externalTask.Variables.TryGetValue("FoodSelector", out var food))
            {
                return new BpmnErrorResult("NO_FOOD_SELECTOR", "No food to cook selected");
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://tasty.p.rapidapi.com/recipes/auto-complete?prefix={food.Value}"),
                Headers =
                {
                    { "x-rapidapi-key", "4930fadba5msh79c14b9b055fe45p1495e1jsn64d827b6ed2d" },
                    { "x-rapidapi-host", "tasty.p.rapidapi.com" },
                },
            };

            using var response = await client.SendAsync(request);

            var body = await response.Content.ReadAsStringAsync();

            return new CompleteResult
            {
                Variables = new Dictionary<string, Variable>
                {
                    ["Receipts"] = Variable.String(body)
                }
            };
        }
    }
}
