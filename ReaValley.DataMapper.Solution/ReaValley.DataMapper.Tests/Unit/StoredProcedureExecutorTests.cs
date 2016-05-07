using System.Data;
using System.Data.Common;
using Moq;
using NUnit.Framework;
using ReaValley.DataMapper.Tests.Helpers;

namespace ReaValley.DataMapper.Tests.Unit
{

    [TestFixture]
    public class StoredProcedureExecutorTests
    {
        [Test]
        public void WhenExecute_ThenMocksAreExercised()
        {
            var database = new Mock<IDatabase>();
            const string sp = "select_dogs";
            var parameterMapper = new Mock<IParameterMapper>();
            var resultsMapper = new Mock<IResultSetMapper<Dog>>();

            var spExecuter = new StoredProcedureExecutor<Dog>(database.Object, sp, parameterMapper.Object, resultsMapper.Object);
            spExecuter.Execute();

            database.Verify(x => x.GetStoredProcCommand(It.IsAny<string>()), Times.Once);
            parameterMapper.Verify(x => x.AssignParameters(It.IsAny<DbCommand>(), It.IsAny<object[]>()), Times.Once);
            resultsMapper.Verify(x => x.MapResultSet(It.IsAny<IDataReader>()), Times.Once);
        }
    }
}
