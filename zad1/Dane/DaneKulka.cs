using Dane.Components;

// About
//  Represents a drawable circular (2D) object.
//  When creating a InterfaceKulka we're returning DaneKulka.

namespace Dane {
    public interface InterfaceKulka {
        int Identifier { get; set; }
        ITransform Transform { get; set; }
        IRigidBody RigidBody { get; set; }
    }

    internal class DaneKulka : InterfaceKulka {
        public int Identifier { get; set; }
        public ITransform Transform { get; set; }
        public IRigidBody RigidBody { get; set; }
        public DaneKulka(
            int newIdentifier,
            ITransform newTransform,
            IRigidBody newRigidBody
        ) {
            Identifier = newIdentifier;
            Transform = newTransform;
            RigidBody = newRigidBody;
        }
	}
}
