using System;
using System.Numerics;
using Logika;

public class OnPositionChangeUiAdapterEventArgs : EventArgs {

    public readonly int id;
    public readonly Vector2 position;

    public OnPositionChangeUiAdapterEventArgs(int newId, Vector2 newPosition) {
        position = newPosition;
        id = newId;
    }
}

public class OnDiameterChangeUiAdapterEventArgs : EventArgs {
    public readonly int id;
    public readonly float diameter;

    public OnDiameterChangeUiAdapterEventArgs(int newId, float newDiameter) {
        diameter = newDiameter;
        id = newId;
    }
}

namespace Presentation.Model {

    public class MainModel {
        private readonly Vector2 boardSize;
        private int ballsAmount;
        private LogikaAPI ballsLogic;

        public event EventHandler<OnPositionChangeUiAdapterEventArgs>? BallPositionChange;
        public event EventHandler<OnDiameterChangeUiAdapterEventArgs>? BallDiameterChange;

        public MainModel() {
            boardSize = new Vector2(350, 200);
            ballsAmount = 0;
            ballsLogic = LogikaAPI.CreateBalls(boardSize);
            ballsLogic.BallChange += (sender, arguments) => {
                BallPositionChange?.Invoke(this, new OnPositionChangeUiAdapterEventArgs(arguments.Ball.Identifier, arguments.Ball.Transform.Position));
                BallDiameterChange?.Invoke(this, new OnDiameterChangeUiAdapterEventArgs(arguments.Ball.Identifier, arguments.Ball.Transform.Diameter));
            };
        }

        public void StartSimulation() {
            ballsLogic.AddBalls(ballsAmount);
            ballsLogic.StartSimulation();
        }

        public void StopSimulation() {
            ballsLogic.StopSimulation();

            // RESET SO WE CAN START AGAIN
            ballsLogic = LogikaAPI.CreateBalls(boardSize);
            ballsLogic.BallChange += (sender, arguments) => {
                BallPositionChange?.Invoke(this, new OnPositionChangeUiAdapterEventArgs(arguments.Ball.Identifier, arguments.Ball.Transform.Position));
                BallDiameterChange?.Invoke(this, new OnDiameterChangeUiAdapterEventArgs(arguments.Ball.Identifier, arguments.Ball.Transform.Diameter));
            };
        }

        public void SetBallNumber(int amount) { ballsAmount = amount; }

        public int GetBallsCount() { return ballsAmount; }

        public void OnBallPositionChange(OnPositionChangeUiAdapterEventArgs args) {
            BallPositionChange?.Invoke(this, args);
        }
    }

}
