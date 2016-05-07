using System.Data;
using System.Data.Common;

namespace ReaValley.DataMapper
{
    public interface IDatabase
    {
        IDataReader ExecuteReader(IDbCommand command);
        DbCommand GetStoredProcCommand(string storedProcedureName);
    }
}