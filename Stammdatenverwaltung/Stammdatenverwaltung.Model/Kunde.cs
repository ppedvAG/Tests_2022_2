namespace Stammdatenverwaltung.Model
{
    public class Kunde : Person
    {
        public string KdNummer { get; set; }
        public virtual Mitarbeiter Ansprechpartner { get; set; }
    }
}
