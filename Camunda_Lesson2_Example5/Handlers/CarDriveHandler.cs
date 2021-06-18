using Camunda.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Camunda_Lesson2_Example5.Handlers
{
    [HandlerTopics("DriveCar", LockDuration = 10000)]
    public class CarDriveHandler : IExternalTaskHandler
    {
        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            return new BpmnErrorResult("CAR_ERROR", "Engine is not working");
        }
    }
}
