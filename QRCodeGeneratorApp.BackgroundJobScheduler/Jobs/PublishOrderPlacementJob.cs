using System;
using System.Collections.Generic;
using System.Text;
using TickerQ.Utilities.Base;

namespace QRCodeGeneratorApp.BackgroundJobScheduler.Jobs
{
    internal class PublishOrderPlacementJob
    {
        [TickerFunction(nameof(PublishOrderPlacementJob))]
        public async Task PublishOrderPlacement(TickerFunctionContext context, CancellationToken cancellationToken)
        {

        }
    }
}
