using System;
using Dane;

namespace Logika;

public class OnBallChangeEventArgs : EventArgs {
	public readonly InterfaceKulka Ball;

	public OnBallChangeEventArgs(InterfaceKulka newBall) {
		Ball = newBall;
	}
}
