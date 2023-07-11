using Dane.Components;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;
using Dane;
using System.Threading;

namespace Logika;

// About
// 

internal class Kulka : InterfaceKulka {

	private readonly InterfaceKulka ball;
    private readonly Random random;
	private readonly Kulki owner;

	public event EventHandler<OnBallChangeEventArgs>? PositionChange;
    public event EventHandler<OnBallChangeEventArgs>? DiameterChange;


    // CONSTRUCTORS

    public Kulka(int newId, InterfaceKulka newBall, Kulki newOwner) {
        random = new Random();
        owner = newOwner;
        ball = newBall;
        Id = newId;
	}

	public Kulka(int newId, ITransform newTransform, IRigidBody newRigidBody, Kulki newOwner) {
		ball = DaneAPI.CreateBall(newId, newTransform, newRigidBody);
        random = new Random();
        owner = newOwner;
        Id = newId;
	}

	// PROPERTIES

    public int Id { get; private set; }

    public Vector2 Position {
		get => ball.Transform.Position;
		set => ball.Transform.Position = value;
	}

	public float Diameter {
        get => ball.Transform.Diameter;
        set => ball.Transform.Diameter = value;
    }

	public Vector2 Velocity {
        get => ball.RigidBody.Velocity;
        set => ball.RigidBody.Velocity = value;
    }

    public float Mass {
        get => ball.RigidBody.Mass;
        set => ball.RigidBody.Mass = value;
    }
    public int Identifier { get => ball.Identifier; set => ball.Identifier = value; }
    public ITransform Transform { get => ball.Transform; set => ball.Transform = value; }
    public IRigidBody RigidBody { get => ball.RigidBody; set => ball.RigidBody = value; }

    // FUNCTIONS

    public async void Simulate() {
        var timer = new Stopwatch();
        float deltaTime = 0.0f;

        while (!owner.CancelSimulationSource.Token.IsCancellationRequested) {

            timer.Start();

            // Change Values
            Position = MoveInsideBoard(Diameter, deltaTime); 

			// Apply Values
            PositionChange?.Invoke(this, new OnBallChangeEventArgs(this));

            await Task.Delay(4, owner.CancelSimulationSource.Token).ContinueWith(ignored => { });

            timer.Stop();
            deltaTime = timer.ElapsedMilliseconds / 1000.0f;
            timer.Reset();

        }
	}

    // UTIL

    public Vector2 MoveInsideBoard(float ballDiameter, float deltaTime) {
        
        for (int i = 0; i < owner.GetBallsCount(); i++) {
            var other = owner.GetBall(i);

            if (other.Identifier == Id) continue;

            owner.simulationPause.WaitOne();
            try {
                if (Kolizja.IsBallsCollides(ball, other))
                    Kolizja.HandleCollision(ball, other);
            } finally {
                owner.simulationPause.ReleaseMutex();
            }
        }

        Vector2 newPosition = Position + Vector2.Multiply(Velocity, deltaTime);

        // BOUNDRY CLAMP
        if (newPosition.X < 0) {
            Velocity = new Vector2(-Velocity.X, Velocity.Y);
            newPosition.X = 0;
        } else if (newPosition.X > owner.BoardSize.X - ballDiameter) {
            Velocity = new Vector2(-Velocity.X, Velocity.Y);
            newPosition.X = owner.BoardSize.X - ballDiameter;
        }

        if (newPosition.Y < 0) {
            Velocity = new Vector2(Velocity.X, -Velocity.Y);
            newPosition.Y = 0;
        } else if (newPosition.Y > owner.BoardSize.Y - ballDiameter) {
            Velocity = new Vector2(Velocity.X, -Velocity.Y);
            newPosition.Y = owner.BoardSize.Y -ballDiameter;
        }

        return newPosition;
	}
}