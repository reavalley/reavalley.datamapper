using System.Data.Common;

namespace ReaValley.DataMapper
{
    public interface IParameterMapper
    {
        void AssignParameters(DbCommand command, object[] parameterValues);
    }
}