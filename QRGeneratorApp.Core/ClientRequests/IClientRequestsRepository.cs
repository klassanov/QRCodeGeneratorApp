using System;
using System.Collections.Generic;
using System.Text;
using QRGeneratorApp.Core.ClientRequests.Get;

namespace QRGeneratorApp.Core.ClientRequests
{
    public interface IClientRequestsRepository
    {
        Task<GetClientRequestResult> GetById(Guid id);
    }
}
