using System;
using System.Threading;
using Xunit;

namespace Stammdatenverwaltung.Data.Tests
{
    public class EfContextTests
    {
        [Fact]
        public void New_context_should_create_new_db_if_not_exists()
        {
            var testConString = "Data Source=CreateTest.db;";
            var con = new EfContext(testConString);
            con.Database.EnsureDeleted();

            var wasCreate = con.Database.EnsureCreated();

            Assert.True(wasCreate);
        }
    }
}
