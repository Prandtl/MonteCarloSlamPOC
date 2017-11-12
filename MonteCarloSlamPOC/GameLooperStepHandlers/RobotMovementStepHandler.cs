using System;
using System.Linq;
using System.Windows.Forms;
using MonteCarloSlamPOC.TestObjects;

namespace MonteCarloSlamPOC.GameLooperStepHandlers
{
    class RobotMovementStepHandler : IGameLooperStepHandler
    {
        public const int _angleStep = 10;
        public const int _positionStep = 10;

        public int _deltaAngle;
        public int _deltaPosition;

        public virtual void MakeChanges(TestFieldModel testFieldModel)
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
