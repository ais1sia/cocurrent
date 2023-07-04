using System.Collections.Generic;
using System.Threading.Tasks;

// About
//  It's a list of exsisting in current scene so called spheres.

namespace Dane {
    internal class DaneKulki : DaneAPI {
        private readonly List<InterfaceKulka> balls;

        public DaneKulki() {
            balls = new List<InterfaceKulka>();
        }

        public override void Add(InterfaceKulka newBall) {
            balls.Add(newBall);
        }

        public override InterfaceKulka Get(int index) {
            return balls[index];
        }

        public override int GetCount() {
            return balls.Count;
        }
    }
}
