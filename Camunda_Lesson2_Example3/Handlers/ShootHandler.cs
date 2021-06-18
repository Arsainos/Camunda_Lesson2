using Camunda.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Camunda_Lesson2_Example3.Handlers
{
    [HandlerTopics("Shoot", LockDuration = 10000)]
    public class ShootHandler : IExternalTaskHandler
    {
        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            var shooted = Convert.ToBoolean(new Random().Next(0, 2));

            return new CompleteResult
            {
                Variables = new Dictionary<string, Variable>
                {
                    ["Shooted"] = Variable.Boolean(shooted)
                }
            };
        }
    }
}
