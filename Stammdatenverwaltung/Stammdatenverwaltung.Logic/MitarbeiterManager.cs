using Stammdatenverwaltung.Model;
using Stammdatenverwaltung.Model.Contracts;
using System;
using System.Linq;

namespace Stammdatenverwaltung.Logic
{
    public class MitarbeiterManager
    {
        public IRepository Repository { get; }

        public MitarbeiterManager(IRepository repository)
        {
            Repository = repository;
        }

        public Abteilung GetAbteilungWithYoungestAverageMitarbeiter(DateTime today)
        {
            return Repository.GetAll<Abteilung>()
                             .OrderBy(x => x.Mitarbeiter.Average(m => CalcAge(m.GebDatum, today)))
                             .FirstOrDefault();
        }

        public int CalcAge(DateTime birthdate, DateTime today)
        {
            // Calculate the age.
            var age = today.Year - birthdate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (birthdate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
