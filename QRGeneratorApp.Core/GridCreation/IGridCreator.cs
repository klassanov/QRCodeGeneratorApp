using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGeneratorApp.Core.GridCreation
{
    public interface IGridCreator
    {
        string CreateGrid(bool[,] qrMap);
    }
}
