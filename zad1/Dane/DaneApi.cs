using Dane.Components;
using System;
using System.Numerics;
using System.Threading;

namespace Dane;

// About
//  It is the interface we're sharing outside.

public abstract class DaneAPI {
    public abstract void Add(InterfaceKulka newBall);
	public abstract int GetCount();
	public abstract InterfaceKulka Get(int index);
	public static DaneAPI CreateBallsList() { return new DaneKulki(); }
    public static ITransform CreateTransform(Vector2 newPosition, float newDiameter) { return new Transform(newPosition, newDiameter); }
    public static IRigidBody CreateRigidBody(Vector2 newVelocity, float newMass) { return new RigidBody(newVelocity, newMass); }
	public static InterfaceKulka CreateBall(int newIdentifier, ITransform newTransfrom, IRigidBody newRigidBody) { return new DaneKulka(newIdentifier, newTransfrom, newRigidBody); }
}