using Moq;
using Stammdatenverwaltung.Model;
using Stammdatenverwaltung.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var mm = new MitarbeiterManager(null);

            var result = mm.CalcAge(new DateTime(year, month, day), today);

            Assert.Equal(expAge, result);
        }

        [Fact]
        public void GetAbteilungWithYoungestAverageMitarbeiter_Empty_DB_returns_null()
        { }

        [Fact]
        public void GetAbteilungWithYoungestAverageMitarbeiter_3_Abts_Number_2_wins_moq()
        {
            var testDay = new DateTime(2022, 9, 9);
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(x => x.GetAll<Abteilung>()).Returns(() =>
            {
                var abt1 = new Abteilung() { Bezeichnung = "Abt1" };
                abt1.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-100) });
                abt1.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-200) });

                var abt2 = new Abteilung() { Bezeichnung = "Abt2" };
                abt2.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-20) });
                abt2.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-30) });

                var abt3 = new Abteilung() { Bezeichnung = "Abt3" };
                abt3.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-400) });

                return new[] { abt1, abt2, abt3 };
            });
            var mm = new MitarbeiterManager(repoMock.Object);

            var result = mm.GetAbteilungWithYoungestAverageMitarbeiter(testDay);

            Assert.Equal("Abt2", result.Bezeichnung);
        }

        [Fact]
        public void GetAbteilungWithYoungestAverageMitarbeiter_3_Abts_Number_2_wins()
        {
            var testDay = new DateTime(2022, 9, 9);
            var mm = new MitarbeiterManager(new TestRepo());

            var result = mm.GetAbteilungWithYoungestAverageMitarbeiter(testDay);

            Assert.Equal("Abt2", result.Bezeichnung);
        }

        class TestRepo : IRepository
        {
            public void Add<T>(T entity) where T : Entity
            {
                throw new NotImplementedException();
            }

            public void Delete<T>(T entity) where T : Entity
            {
                throw new NotImplementedException();
            }

            public IEnumerable<T> GetAll<T>() where T : Entity
            {
                if (typeof(T) == typeof(Abteilung))
                {
                    var abt1 = new Abteilung() { Bezeichnung = "Abt1" };
                    abt1.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-100) });
                    abt1.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-200) });

                    var abt2 = new Abteilung() { Bezeichnung = "Abt2" };
                    abt2.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-20) });
                    abt2.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-30) });

                    var abt3 = new Abteilung() { Bezeichnung = "Abt3" };
                    abt3.Mitarbeiter.Add(new Mitarbeiter() { GebDatum = DateTime.Now.AddYears(-400) });

                    return new[] { abt1, abt2, abt3 }.Cast<T>();
                }
                throw new NotImplementedException();
            }

            public T GetById<T>(int id) where T : Entity
            {
                throw new NotImplementedException();
            }

            public int SaveAll()
            {
                throw new NotImplementedException();
            }

            public void Update<T>(T entity) where T : Entity
            {
                throw new NotImplementedException();
            }
        }
    }
}
