using System;
using System.Collections.Generic;
using System.Numerics;
using Dane;

namespace Logika;

// About
// - Logical part holds the information that doesn't need to be serialized.

public abstract class LogikaAPI {

    public event EventHandler<OnBallChangeEventArgs>? BallChange;
    public abstract void AddBalls(int newCount);
	public abstract void AddBall(int newIdentifier, Vector2 newPosition, float newDiameter, Vector2 newVelocity, float newMass);
	public abstract void StartSimulation();
	public abstract void StopSimulation();
	public abstract int GetBallsCount();
	public abstract InterfaceKulka GetBall(int index);
    public abstract IList<InterfaceKulka> GetBalls();

	protected virtual void OnPositionChange(OnBallChangeEventArgs newArguments) {
        BallChange?.Invoke(this, newArguments);
	}

	protected virtual void OnDiameterChange(OnBallChangeEventArgs newArguments) {
        BallChange?.Invoke(this, newArguments);
	}

	public static LogikaAPI CreateBalls(Vector2 newBoardSize, DaneAPI? newDaneAPI = null) {
        newDaneAPI ??= DaneAPI.CreateBallsList(); // same as if (daneApi == null) daneApi = DaneAPI.CreateBallsList();
        return new Kulki(newDaneAPI, newBoardSize);
	}
}