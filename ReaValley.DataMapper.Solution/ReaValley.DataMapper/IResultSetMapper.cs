using System.Collections.Generic;
using System.Data;

namespace ReaValley.DataMapper
{
    public interface IResultSetMapper<out TResult>
    {
        IEnumerable<TResult> MapResultSet(IDataReader reader);
    }
}