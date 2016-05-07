using System.Collections.Generic;
using System.Data;

namespace ReaValley.DataMapper.Tests.Helpers
{
    public class DogResultSetMapper : IResultSetMapper<Dog>
    {
        public IEnumerable<Dog> MapResultSet(IDataReader reader)
        {
            var dogs = new List<Dog>();

            while (reader.Read())
            {
                var dog = new Dog
                {
                    Breed = reader["dog_breed"].ToString(),
                    Colour = reader["dog_colour"].ToString(),
                    Name = reader["dog_name"].ToString()
                };
                dogs.Add(dog);
            }
            return dogs;
        }
    }
}