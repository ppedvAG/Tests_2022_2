using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Stammdatenverwaltung.Model;
using System;
using Xunit;

namespace Stammdatenverwaltung.Data.Tests
{
    public class EfContextTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void New_context_should_create_new_db_if_not_exists()
        {
            var testConString = "Data Source=CreateTest.db;";
            var con = new EfContext(testConString);
            con.Database.EnsureDeleted();

            var wasCreate = con.Database.EnsureCreated();

            Assert.True(wasCreate);
        }

        [Fact]
        [Trait("Category", "Integration")]
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
        [Trait("Category", "Integration")]
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
        [Trait("Category", "Integration")]
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
                loaded.Should().NotBeNull();
                con.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                loaded.Should().BeNull();
            }
        }



        [Fact]
        [Trait("Category", "Integration")]
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
                loaded.Should().BeEquivalentTo(m, c => c.IgnoringCyclicReferences());
            }
        }


        [Fact]
        [Trait("Category", "Integration")]
        public void Delete_Kunde_is_always_possible_even_a_Mitarbeitert_is_Ansprechpartner()
        {
            var m = new Mitarbeiter() { Name = "Don't kill me" };
            var k = new Kunde() { Name = "Kill me" };
            m.Kunden.Add(k);

            using (var con = new EfContext())
            {
                con.Add(m);
                con.Add(k);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Kunden.Find(k.Id);
                con.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                con.Kunden.Find(k.Id).Should().BeNull();
                con.Mitarbeiter.Find(m.Id).Should().NotBeNull();
            }
        }

        [Fact]
        [Trait("Category","Integration")]
        public void Delete_Mitarbeiter_with_Ansprechpartner_throws_DbUpdateException()
        {
            var m = new Mitarbeiter() { Name = "kill me" };
            var k = new Kunde() { Name = "Don't Kill me" };
            m.Kunden.Add(k);

            using (var con = new EfContext())
            {
                con.Add(m);
                con.Add(k);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Mitarbeiter.Find(m.Id);
                con.Remove(loaded);
                var act = new Action(() => con.SaveChanges());
                act.Should().Throw<DbUpdateException>();
            }
        }

    }
}
