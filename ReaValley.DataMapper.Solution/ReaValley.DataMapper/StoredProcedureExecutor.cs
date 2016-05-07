using System.Collections.Generic;
using System.Data;

namespace ReaValley.DataMapper
{
    public class StoredProcedureExecutor<TResult>
    {
        private readonly IDatabase _database;
        private readonly string _storedProcedureName;
        private readonly IParameterMapper _parameterMapper;
        private readonly IResultSetMapper<TResult> _resultSetMapper;

        public StoredProcedureExecutor(
            IDatabase database,
            string storedProcedureName,
            IParameterMapper parameterMapper, 
            IResultSetMapper<TResult> resultSetMapper)
        {
            _database = database;
            _parameterMapper = parameterMapper;
            _resultSetMapper = resultSetMapper;
            _storedProcedureName = storedProcedureName;
        }

        public IEnumerable<TResult> Execute(params object[] parameters)
        {
            using (var storedProcCommand = _database.GetStoredProcCommand(_storedProcedureName))
            {
                _parameterMapper.AssignParameters(storedProcCommand, parameters);
                return Execute(storedProcCommand);
            }
        }

        private IEnumerable<TResult> Execute(IDbCommand command)
        {
            var reader = _database.ExecuteReader(command);
            return _resultSetMapper.MapResultSet(reader);
        }
    }
}
