using Stammdatenverwaltung.Model;
using System;

namespace Stammdatenverwaltung.Logic
{
    public class MitarbeiterManager
    {
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
