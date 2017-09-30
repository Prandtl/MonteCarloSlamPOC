using System;

namespace MonteCarloSlamPOC.GameLooperStepHandlers
{
	public class RandomMovementGameLooperStepHandler : IGameLooperStepHandler
	{
		private Random _random;

		public RandomMovementGameLooperStepHandler()
		{
			_random = new Random();
		}

		public void MakeChanges(TestFieldModel testFieldModel)
		{
			foreach (var gameObject in testFieldModel.GameObjects)
			{
				var xDelta = _random.Next(-8, 9);
				var yDelta = _random.Next(-8, 9);
				var newPositionX = gameObject.X + xDelta;
				var newPositionY = gameObject.Y + yDelta;
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
	}
}