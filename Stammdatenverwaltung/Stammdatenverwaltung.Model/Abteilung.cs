using System.Collections.Generic;

namespace Stammdatenverwaltung.Model
{
    public class Abteilung : Entity
    {
        public string Bezeichnung { get; set; }
        public virtual ICollection<Mitarbeiter> Mitarbeiter { get; set; } = new HashSet<Mitarbeiter>();
    }
}
