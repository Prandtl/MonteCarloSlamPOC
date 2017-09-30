using System.Timers;
using MonteCarloSlamPOC.GameLooperStepHandlers;

namespace MonteCarloSlamPOC
{
	public class GameLooper
	{
		public int LoopInterval { get; set; }

		private TestFieldModel _model;
		private IGameLooperStepHandler _stepHandler;
		private Timer _gameTimer;

		public GameLooper(TestFieldModel model, IGameLooperStepHandler stephandler, int loopInterval)
		{
			_model = model;
			_stepHandler = stephandler;

			LoopInterval = loopInterval;

			_gameTimer = new Timer(LoopInterval);
			_gameTimer.Elapsed += OnLoopFinished;
		}

		public void Start()
		{
			_gameTimer.Start();
		}

		public void Stop()
		{
			_gameTimer.Stop();
		}

		private void OnLoopFinished(object sender, ElapsedEventArgs e)
		{
			_stepHandler.MakeChanges(_model);
			_model.InvokeModelChanged();
		}
	}
}
