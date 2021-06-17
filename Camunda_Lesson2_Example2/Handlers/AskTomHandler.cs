using Camunda.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Camunda_Lesson2_Example2.Handlers
{
    [HandlerTopics("AskTom", LockDuration = 10000)]
    public class AskTomHandler : IExternalTaskHandler
    {
        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            await Task.Delay(5000);

            return new CompleteResult
            {
                Variables = new Dictionary<string, Variable>
                {
                    ["Answer"] = Variable.String("Try to ask Tim")
                }
            };
        }
    }
}
