using AutoFixture;
using AutoFixture.Kernel;
using Stammdatenverwaltung.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        [Fact]
        public void Can_add_new_Mitarbeitert_to_db()
        {
            var m = new Mitarbeiter { Name = $"Fred_{Guid.NewGuid()}" };

            using (var con = new EfContext())
            {
                con.Mitarbeiter.Add(m);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                Assert.Equal(m.Name, loaded.Name);
            }
        }

        [Fact]
        public void Can_update_Mitarbeitert()
        {
            var m = new Mitarbeiter { Name = $"Fred_{Guid.NewGuid()}" };
            var newName = $"Wilma_{Guid.NewGuid()}";
            using (var con = new EfContext())
            {
                con.Mitarbeiter.Add(m);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                loaded.Name = newName;
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                Assert.Equal(newName, loaded.Name);
            }
        }


        [Fact]
        public void Can_delete_Mitarbeitert()
        {
            var m = new Mitarbeiter { Name = $"Fred_{Guid.NewGuid()}" };
            using (var con = new EfContext())
            {
                con.Mitarbeiter.Add(m);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                Assert.NotNull(loaded);
                con.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                Assert.Null(loaded);
            }
        }



        [Fact]
        public void Can_add_new_Mitarbeitert_to_db_AutoFix()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            var m = fix.Create<Mitarbeiter>();

            using (var con = new EfContext())
            {
                con.Mitarbeiter.Add(m);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                Assert.Equal(m.Name, loaded.Name);
            }
        }

        internal class PropertyNameOmitter : ISpecimenBuilder
        {
            private readonly IEnumerable<string> names;

            internal PropertyNameOmitter(params string[] names)
            {
                this.names = names;
            }

            public object Create(object request, ISpecimenContext context)
            {
                var propInfo = request as PropertyInfo;
                if (propInfo != null && names.Contains(propInfo.Name))
                    return new OmitSpecimen();

                return new NoSpecimen();
            }
        }

    }
}
