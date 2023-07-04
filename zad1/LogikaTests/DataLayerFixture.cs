using System.Collections.Generic;
using Dane;

namespace Logika.Tests;

public class DataLayerFixture : DaneAPI {
	private readonly List<InterfaceKulka> ballsList;

	public DataLayerFixture() {
		ballsList = new List<InterfaceKulka>();
	}

	public override void Add(InterfaceKulka ball) {
		ballsList.Add(ball);
	}

	public override InterfaceKulka Get(int index) {
		return ballsList[index];
	}

	public override int GetCount() {
		return ballsList.Count;
	}
}