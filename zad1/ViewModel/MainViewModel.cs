using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Presentation.Model;

// About:
// Properties inside class that derive from INotifyPropertyChanged interface and
//  are marked as public can be reached inside View.
//  eg. BallsCount, BallPosition.X, BallPosition.Y.
//
// ObservableCollection<T>
//  - Represents a dynamic data collection that provides notifications when items get added or removed, or when the whole list is refreshed.
// AsyncObservableCollection<T>
//  - Can be reached in View using Biding.Path

namespace Presentation.ViewModel {
    public class MainViewModel : INotifyPropertyChanged {

        private readonly MainModel model;

        public AsyncObservableCollection<BallProperty> Circles { get; set; } 

        public int BallsCount {
            get { return model.GetBallsCount(); }
            set {
                if (value >= 0) {
                    model.SetBallNumber(value);
                    OnPropertyChanged();
                }
            }
        }

        public ICommand Increase { get; }
        public ICommand Decrease { get; }
        public ICommand StartSimulation { get; }
        public ICommand StopSimulation { get; }

		public MainViewModel() {
            Circles = new AsyncObservableCollection<BallProperty>();
            model = new MainModel();

            BallsCount = 5;

            Increase = new RelayCommand(() => {
                BallsCount += 1;
            });

            Decrease = new RelayCommand(() => {
                BallsCount -= 1;
            });

            StartSimulation = new RelayCommand(() => {
                
                StopSimulation.Execute(this);

                model.SetBallNumber(BallsCount);

                for (int i = 0; i < BallsCount; i++) {
                    Circles.Add(new BallProperty());
                }

                model.BallDiameterChange += (sender, arguments) => {
                    if (Circles.Count > 0)
                        Circles[arguments.id].ChangeDiamater(arguments.diameter);
                };

                model.BallPositionChange += (sender, arguments) => {
                    if (Circles.Count > 0)
                        Circles[arguments.id].ChangePosition(arguments.position);
                };

                model.StartSimulation();
            });

            StopSimulation = new RelayCommand(() => {
                model.StopSimulation();
                Circles.Clear();
                model.SetBallNumber(BallsCount);
            });
        }

        // Event for View update
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}