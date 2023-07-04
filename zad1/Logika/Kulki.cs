using Dane.Components;
using Logika;
using Logika.Exceptions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Dane;

namespace Logika;

// About
// 

internal class Kulki : LogikaAPI {

    public readonly Mutex simulationPause = new Mutex(false); // CriticalSection Lock
    private readonly DaneAPI daneKulki;

    public CancellationTokenSource CancelSimulationSource { get; private set; }         //
    public Vector2 BoardSize { get; }

	public Kulki(DaneAPI newDataBalls, Vector2 newBoardSize) {
        CancelSimulationSource = new CancellationTokenSource();
        daneKulki = newDataBalls;
		BoardSize = newBoardSize;
	}

	protected override void OnPositionChange(OnBallChangeEventArgs newArgs) {
		base.OnPositionChange(newArgs);
	}

	public override void AddBalls(int newCount) {
		for (var i = 0; i < newCount; i++) {

			// SET PRE SIMULATION VALUES
			var diameter = GetRandomDiameter();
			var mass = GetRandomMass();
            var spawnPoint = GetRandomPointInsideBoard(diameter);
			var spawnVelocity = GetRandomVelocity();

            var transform = DaneAPI.CreateTransform(spawnPoint, diameter);
			var rigidBody = DaneAPI.CreateRigidBody(spawnVelocity, mass);

			daneKulki.Add(DaneAPI.CreateBall(i, transform, rigidBody));
		}
	}

    private Vector2 GetRandomPointInsideBoard(float ballDiameter) {
        var rng = new Random();
        var isPositionIncorrect = true; 
        int x = 0, y = 0, iteration = 0;

        while (isPositionIncorrect) {
            x = rng.Next((int)ballDiameter, (int)(BoardSize.X - ballDiameter));
            y = rng.Next((int)ballDiameter, (int)(BoardSize.Y - ballDiameter));

            var transform = DaneAPI.CreateTransform(new Vector2(x, y), ballDiameter);

            isPositionIncorrect = IsCollideCircles(transform);
            iteration++;

            if (iteration == 100) {
                // NO AVAILABLE POSITION, BREAK
                isPositionIncorrect = false;
            }
        }

        return new Vector2(x, y);
    }

    private bool IsCollideCircles(ITransform transfrom) {
		for (int i = 0; i < daneKulki.GetCount(); i++)
            if (IsCollideCircle(daneKulki.Get(i).Transform, transfrom))
                return true;
        return false;
    }

    private bool IsCollideCircle(ITransform transfrom, ITransform other) {
        var distanceSquare = 
			(transfrom.Position.X - other.Position.X) * (transfrom.Position.X - other.Position.X) + 
			(transfrom.Position.Y - other.Position.Y) * (transfrom.Position.Y - other.Position.Y);
        var diameterSumSquare = (transfrom.Diameter + other.Diameter) * (transfrom.Diameter + other.Diameter);
        return distanceSquare <= diameterSumSquare;
    }

    public float GetRandomDiameter() {
        const float diameterScale = 50;
        const float diameterMin = 0.1f;

        var rng = new Random();
        return (float)((rng.NextDouble() + diameterMin) * diameterScale);
    }

    public float GetRandomMass() {
        const float diameterScale = 100;
        const float diameterMin = 0.5f;

        var rng = new Random();
        return (float)((rng.NextDouble() + diameterMin) * diameterScale);
    }

    private Vector2 GetRandomVelocity() {
        var rng = new Random();

        var x = (float)((rng.NextDouble() - 0.5) * 400); // 15 - [-7,5; 7.5], 10 - [-5; 5], 400 - [-200, 200]
        var y = (float)((rng.NextDouble() - 0.5) * 400); //

        return new Vector2(x, y);
    }

    public override void AddBall(
        int newIdentifier,
		Vector2 newPosition, 
		float newDiameter,
		Vector2 newVelocity,
		float newMass
	) {

		if (newPosition.X < 0 || newPosition.X > BoardSize.X || newPosition.Y < 0 || newPosition.Y > BoardSize.Y)
			throw new PositionIsOutOfBoardException();

        var transform = DaneAPI.CreateTransform(newPosition, newDiameter);
        var rigidBody = DaneAPI.CreateRigidBody(newVelocity, newMass);

        daneKulki.Add(DaneAPI.CreateBall(newIdentifier, transform, rigidBody));
	}

	public override void StartSimulation() {
		if (CancelSimulationSource.IsCancellationRequested) return;

		CancelSimulationSource = new CancellationTokenSource();

        for (var i = 0; i < daneKulki.GetCount(); i++) {
			var ball = new Kulka(i, daneKulki.Get(i), this);

			// ATTACH CALLBACKS
			ball.PositionChange += (ignored, arguments) => OnPositionChange(arguments);
            ball.DiameterChange += (ignored, arguments) => OnDiameterChange(arguments);

            // CREATING THRED'S
            Task.Factory.StartNew(ball.Simulate, CancelSimulationSource.Token);
		}
	}

	public override void StopSimulation() {
		CancelSimulationSource.Cancel();
	}

	public override int GetBallsCount() {
		return daneKulki.GetCount();
	}

    public override InterfaceKulka GetBall(int index) {
        return daneKulki.Get(index);
    }

	public override IList<InterfaceKulka> GetBalls() {
        var ballsList = new List<InterfaceKulka>();
		for (var i = 0; i < daneKulki.GetCount(); i++) 
			ballsList.Add(new Kulka(i, daneKulki.Get(i), this));
		return ballsList;
	}
}