using Stammdatenverwaltung.Model;
using Stammdatenverwaltung.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stammdatenverwaltung.Logic.Tests
{
    public partial class MitarbeiterManagerTests
    {
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
