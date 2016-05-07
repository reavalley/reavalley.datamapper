using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ReaValley.DataMapper.Tests.Helpers;

namespace ReaValley.DataMapper.Tests.Integration
{
    [TestFixture]
    public class StoredProcedureExecutorIntegrationTests
    {
        [Test]
        public void GivenResultSetMapperImplementation_WhenExecute_ThenListOfDogsIsReturned()
        {
            var database = GetDatabase();
            const string sp = "select_dogs";
            var parameterMapper = new Mock<IParameterMapper>();
            var resultsMapper = new DogResultSetMapper();

            var spExecuter = new StoredProcedureExecutor<Dog>(database, sp, parameterMapper.Object, resultsMapper);
            var result = spExecuter.Execute();

            result.Should().HaveCount(3);
        }

        private static IDatabase GetDatabase()
        {
            var dbCommand = new Mock<DbCommand>();
            var database = new Mock<IDatabase>();
            database.Setup(x => x.GetStoredProcCommand(It.IsAny<string>())).Returns(dbCommand.Object);
            database.Setup(x => x.ExecuteReader(It.IsAny<IDbCommand>())).Returns(GetMockIDataReader(GetTestData()));

            return database.Object;
        }

        private static IReadOnlyList<Dog> GetTestData()
        {
            return new List<Dog>
            {
                new Dog { Breed = "Border/Jack", Colour = "Sandy", Name = "Titan"},
                new Dog { Breed = "Border/Jack", Colour = "Sandy Brown", Name = "Holly"},
                new Dog { Breed = "Border Collie", Colour = "Black/White", Name = "Willow"}
            };
        } 

        private static IDataReader GetMockIDataReader(IReadOnlyList<Dog> dataToMap)
        {
            var moqDataReader = new Mock<IDataReader>();

            // This var stores current position in 'ojectsToEmulate' list
            var count = -1;

            moqDataReader.Setup(x => x.Read()).Returns(() => count < dataToMap.Count - 1).Callback(() => count++);

            moqDataReader.Setup(x => x["dog_name"]).Returns(() => dataToMap[count].Name);
            moqDataReader.Setup(x => x["dog_breed"]).Returns(() => dataToMap[count].Breed);
            moqDataReader.Setup(x => x["dog_colour"]).Returns(() => dataToMap[count].Colour);

            return moqDataReader.Object;
        }
    }
}