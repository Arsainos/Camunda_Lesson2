using Camunda.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Camunda_Lesson2_Example3.Handlers
{
    [HandlerTopics("Strafe", LockDuration = 10000)]
    public class StrafeHandler : IExternalTaskHandler
    {
        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            var strafed = Convert.ToBoolean(new Random().Next(0, 2));

            return new CompleteResult
            {
                Variables = new Dictionary<string, Variable>
                {
                    ["Strafed"] = Variable.Boolean(strafed)
                }
            };
        }
    }
}
