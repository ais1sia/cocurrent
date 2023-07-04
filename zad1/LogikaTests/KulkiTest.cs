using System.Collections.Generic;
using System.Numerics;
using Logika.Exceptions;
using NUnit.Framework;

namespace Logika.Tests;

public class KulkiTest {

	private LogikaAPI kulki;
	private readonly Vector2 boardSize = new Vector2(150, 100);

	private readonly Vector2 sharedVelocity = new Vector2(0, 0);
    private readonly float sharedDiameter = 1;
    private readonly float sharedMass = 1;

	[SetUp]
	public void SetUp() {
		kulki = LogikaAPI.CreateBalls(boardSize, new DataLayerFixture());
	}

	[Test]
	public void AddBallTest() {
		kulki.AddBall(0, boardSize / 2, sharedDiameter, sharedVelocity, sharedMass);
		Assert.AreEqual(1, kulki.GetBallsCount());
		Assert.AreEqual(boardSize / 2, kulki.GetBalls()[0].Transform.Position);
	}

	[Test]
	public void AddBallOutOfBoardTest() {
		Assert.Throws<PositionIsOutOfBoardException>((() => kulki.AddBall(
			0,
			boardSize + Vector2.One * 20, 
			sharedDiameter, 
			sharedVelocity, 
			sharedMass
        )));

		Assert.Throws<PositionIsOutOfBoardException>((() => kulki.AddBall(
			0,
			Vector2.One * -20,
            sharedDiameter,
            sharedVelocity,
            sharedMass
        )));

		Assert.AreEqual(0, kulki.GetBallsCount());
	}


	[Test]
	public void AddBallsTest() {
		kulki.AddBalls(15);
		Assert.AreEqual(15, kulki.GetBallsCount());
	}

	[Test]
	public void SimulationTest() {
		var interactionCount = 0;
		kulki.AddBalls(10);
		Assert.AreEqual(10, kulki.GetBallsCount());

		var startPositionList = new List<Vector2>();
		for (int i = 0; i < kulki.GetBallsCount(); i++)
			startPositionList.Add(kulki.GetBalls()[i].Transform.Position);

		kulki.BallChange += (_, _) => {
			interactionCount++;
			if (interactionCount >= 50)
				kulki.StopSimulation();
		};

		kulki.StartSimulation();
		while (interactionCount < 50) { }

		Assert.GreaterOrEqual(interactionCount, 49);

		for (int i = 0; i < kulki.GetBallsCount(); i++)
			if (startPositionList[i] != kulki.GetBalls()[i].Transform.Position)
				return;

		Assert.Fail();
	}

	[Test]
	public void CollideBoundryTop() {
        kulki.AddBall(0, new Vector2(40, 0), 10, new Vector2(-1, -1), 20);
        var ball = kulki.GetBall(0);

        Vector2 newPosition = ball.Transform.Position + ball.RigidBody.Velocity;

        Assert.IsTrue(newPosition.Y < 0);
    }

    [Test]
    public void CollideBoundryLeft() {
        kulki.AddBall(0, new Vector2(0, 40), 10, new Vector2(-1, -1), 20);
        var ball = kulki.GetBall(0);

        Vector2 newPosition = ball.Transform.Position + ball.RigidBody.Velocity;

        Assert.IsTrue(newPosition.X < 0);

        // If it would be rly. packed.
        // ball.RigidBody.Velocity = new Vector2(-ball.RigidBody.Velocity.X, ball.RigidBody.Velocity.Y);
        // newPosition.X = 0;
    }

    [Test]
    public void CollideBoundryBottom() {
        kulki.AddBall(0, new Vector2(40, boardSize.Y), 10, new Vector2(1, 1), 20);
        var ball = kulki.GetBall(0);

        Vector2 newPosition = ball.Transform.Position + ball.RigidBody.Velocity;

        Assert.IsTrue(newPosition.Y > boardSize.Y -  ball.Transform.Diameter);

        // If it would be rly. packed.
        // ball.RigidBody.Velocity = new Vector2(-ball.RigidBody.Velocity.X, ball.RigidBody.Velocity.Y);
        // newPosition.X = boardSize.X -  ball.Transform.Diameter;
    }

    [Test]
    public void CollideBoundryRight() {
        kulki.AddBall(0, new Vector2(boardSize.X, 40), 10, new Vector2(1, 1), 20);
        var ball = kulki.GetBall(0);

        Vector2 newPosition = ball.Transform.Position + ball.RigidBody.Velocity;

        Assert.IsTrue(newPosition.X > boardSize.X -  ball.Transform.Diameter);

        // If it would be rly. packed.
        // ball.RigidBody.Velocity = new Vector2(ball.RigidBody.Velocity.X, -ball.RigidBody.Velocity.Y);
        // newPosition.Y = boardSize.Y - ball.Transform.Diameter;
    }

    [Test]
	public void CollideBallOther() {
		kulki.AddBall(0, new Vector2(40, 40), 10, new Vector2(0, 0), 20);
        kulki.AddBall(1, new Vector2(35, 35), 10, new Vector2(0, 0), 20);

		var ball = kulki.GetBall(0);
        var other = kulki.GetBall(1);

		Assert.IsTrue(Kolizja.IsBallsCollides(ball, other));
    }
}