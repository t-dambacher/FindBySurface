using System;

namespace FindBySurface.Dtos
{
    public sealed class Commune : IEquatable<Commune>
    {
        public string CodeInsee { get; }
        public string CodePostal { get; }
        public string Libelle { get; }
        public string CodeDepartement { get; }

        public Commune(string codeInsee, string codePostal, string libelle)
        {
            this.CodeInsee = codeInsee ?? throw new ArgumentNullException(nameof(codeInsee));
            this.CodePostal = codePostal ?? throw new ArgumentNullException(nameof(codePostal));
            this.Libelle = libelle ?? throw new ArgumentNullException(nameof(libelle));
            this.CodeDepartement = codePostal.Substring(0, 2);    // Good enough
        }

        public override bool Equals(object obj) => Equals(obj as Commune);
        public bool Equals(Commune other) => string.Equals(this.CodeInsee, other?.CodeInsee);
        public override int GetHashCode() => this.CodeInsee.GetHashCode();
    }
}
