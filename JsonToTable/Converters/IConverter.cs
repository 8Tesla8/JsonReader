using System;
using System.Collections.Generic;
using System.Linq;
using JsonToTable.Desirializers;

namespace JsonToTable.Converters
{
    public interface IConverter<T,R>
    {
        T Convert(R data);
    }
}
