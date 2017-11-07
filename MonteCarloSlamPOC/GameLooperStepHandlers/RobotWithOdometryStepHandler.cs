using System;
using System.Linq;
using System.Windows.Forms;
using MonteCarloSlamPOC.TestObjects;

namespace MonteCarloSlamPOC.GameLooperStepHandlers
{
    class RobotWithOdometryStepHandler : IGameLooperStepHandler
    {
        private const int _angleStep = 10;
        private const int _positionStep = 10;

        private int _deltaAngle;
        private int _deltaPosition;

        private Random rand = new Random();

        public void MakeChanges(TestFieldModel testFieldModel)
        {
            var possibleRobbot = testFieldModel.GameObjects.FirstOrDefault(go => go is Robot);
            if (possibleRobbot != null) {
                var robot = (Robot)possibleRobbot;

                robot.Angle += _deltaAngle;
                robot.Angle %= 360;


                var deltaPositionX = (int)(_deltaPosition * Math.Sin(robot.Angle * Math.PI / 180));
                var deltaPositionY = (int)(_deltaPosition * Math.Cos(robot.Angle * Math.PI / 180));

                robot.X += deltaPositionX;
                robot.Y += deltaPositionY;

                robot.X = robot.X < 0 ?
                    0 :
                    robot.X >= testFieldModel.Width ?
                        testFieldModel.Width :
                        robot.X;
                robot.Y = robot.Y < 0 ?
                    0 :
                    robot.Y >= testFieldModel.Height ?
                        testFieldModel.Height :
                        robot.Y;
            }

            var possibleOdometry = testFieldModel.GameObjects.FirstOrDefault(go => go is OdometerEstimation);
            if (possibleOdometry != null) {
                var estimation = (OdometerEstimation)possibleOdometry;

                var deltaAngle = _deltaAngle + (rand.NextDouble() - 1) * 0.03 * _angleStep;

                estimation.Angle += deltaAngle;
                estimation.Angle %= 360;
                
                var deltaPosition = _deltaPosition + (rand.NextDouble() - 1) * 0.05 * _positionStep;

                var deltaPositionX = (int)(deltaPosition * Math.Sin(estimation.Angle * Math.PI / 180));
                var deltaPositionY = (int)(deltaPosition * Math.Cos(estimation.Angle * Math.PI / 180));

                estimation.X += deltaPositionX;
                estimation.Y += deltaPositionY;

                estimation.X = estimation.X < 0 ?
                    0 :
                    estimation.X >= testFieldModel.Width ?
                        testFieldModel.Width :
                        estimation.X;
                estimation.Y = estimation.Y < 0 ?
                    0 :
                    estimation.Y >= testFieldModel.Height ?
                        testFieldModel.Height :
                        estimation.Y;
                var oldX = estimation.X;
                var oldY = estimation.Y;
                var oldAngle = estimation.Angle;
            }


        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Up:
                    _deltaPosition = _positionStep;
                    break;
                case Keys.Down:
                    _deltaPosition = -_positionStep;
                    break;
                case Keys.Left:
                    _deltaAngle = _angleStep;
                    break;
                case Keys.Right:
                    _deltaAngle = -_angleStep;
                    break;
            }
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Up:
                    _deltaPosition = 0;
                    break;
                case Keys.Down:
                    _deltaPosition = 0;
                    break;
                case Keys.Left:
                    _deltaAngle = 0;
                    break;
                case Keys.Right:
                    _deltaAngle = 0;
                    break;
            }
        }
    }
}
