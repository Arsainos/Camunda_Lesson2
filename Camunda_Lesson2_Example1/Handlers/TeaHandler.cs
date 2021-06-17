using Camunda.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Camunda_Lesson2_Example1.Handlers
{
    [HandlerTopics("DrinkTea", LockDuration = 10000)]
    [HandlerVariables("TeaType", LocalVariables = true)]
    public class TeaHandler : IExternalTaskHandler
    {
        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            await Task.Delay(5000);

            return new CompleteResult
            {
                LocalVariables = new Dictionary<string, Variable>
                {
                    ["Drunk"] = Variable.String("Yes")
                }
            };
        }
    }
}
