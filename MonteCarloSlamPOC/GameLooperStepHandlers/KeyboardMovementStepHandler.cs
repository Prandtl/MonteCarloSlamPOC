using System.Windows.Forms;

namespace MonteCarloSlamPOC.GameLooperStepHandlers
{
	class KeyboardMovementStepHandler:IGameLooperStepHandler
	{
		private readonly int step = 8;

		private int _deltaX;
		private int _deltaY;

		public void MakeChanges(TestFieldModel testFieldModel)
		{
			foreach (var gameObject in testFieldModel.GameObjects)
			{
				var newPositionX = gameObject.X + _deltaX;
				var newPositionY = gameObject.Y + _deltaY;
				newPositionX = newPositionX < 0 ?
					0 :
					newPositionX >= testFieldModel.Width ?
						testFieldModel.Width :
						newPositionX;
				newPositionY = newPositionY < 0 ?
					0 :
					newPositionY >= testFieldModel.Height ?
						testFieldModel.Height :
						newPositionY;
				gameObject.X = newPositionX;
				gameObject.Y = newPositionY;
			}
		}

		public void OnKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Up:
					_deltaY = -step;
					break;
				case Keys.Down:
					_deltaY = step;
					break;
				case Keys.Left:
					_deltaX = -step;
					break;
				case Keys.Right:
					_deltaX = step;
					break;
			}
		}

		public void OnKeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Up:
					_deltaY = 0;
					break;
				case Keys.Down:
					_deltaY = 0;
					break;
				case Keys.Left:
					_deltaX = 0;
					break;
				case Keys.Right:
					_deltaX = 0;
					break;
			}
		}
	}
}
