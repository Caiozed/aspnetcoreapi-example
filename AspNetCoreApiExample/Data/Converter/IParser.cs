using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Data.Converter
{
    public interface IParser<In, Out>
    {
        Out Parse(In input);
        List<Out> ParseList(List<In> inputList);
    }
}
