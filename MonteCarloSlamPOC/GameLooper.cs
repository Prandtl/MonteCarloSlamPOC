using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			_gameTimer.Start();
		}

		private void OnLoopFinished(object sender, ElapsedEventArgs e)
		{
			_stepHandler.MakeChanges(_model);
		}
	}
}
