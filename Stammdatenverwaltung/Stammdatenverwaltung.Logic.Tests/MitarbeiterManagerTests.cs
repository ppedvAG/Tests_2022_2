using System;
using Xunit;

namespace Stammdatenverwaltung.Logic.Tests
{
    public class MitarbeiterManagerTests
    {
        [Theory]
        [InlineData(1966, 9, 9, 56)]//Adam Sandler
        [InlineData(1966, 9, 8, 56)]
        [InlineData(1966, 9, 10, 55)]
        [InlineData(2022, 9, 9, 0)]
        [InlineData(2021, 9, 9, 1)]
        [InlineData(2023, 9, 1, -1)]
        public void CalcAge(int year, int month, int day, int expAge)
        {
            var today = new DateTime(2022, 09, 09);
            var mm = new MitarbeiterManager();

            var result = mm.CalcAge(new DateTime(year, month, day), today);

            Assert.Equal(expAge, result);
        }
    }
}
