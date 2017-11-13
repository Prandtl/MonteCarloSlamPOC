

using MonteCarloSlamPOC.TestObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonteCarloSlamPOC.GameLooperStepHandlers
{
    class MonteCarloStepHandler : RobotMovementStepHandler
    {
        public const int maxHypothesisCount = 1000;
        public const double hypothesisStepDispersion = 2;
        public const double hypothesisAngleStepDispersion = 2;
        public const double sensorsEps = 20;
        public const double sensorsEpsCalculatedDelta = 2;
        public const double beaconRadius = 350;

        private readonly Random random = new Random();

        private readonly object _locker = new object();

        private int counter = 0;
        private const int amountOfStepsBetweenKillingSprees = 1;

        public override void MakeChanges(TestFieldModel testFieldModel)
        {
            lock (_locker) {
                base.MakeChanges(testFieldModel);
                UpdateHypotheses(testFieldModel);
                KillBadHypotheses(testFieldModel);
                if (counter == 0) {
                    UpdateWeights(testFieldModel);
                    KillTheWeakHypotheses(testFieldModel);
                }
                GenerateSomeNewHypotheses(testFieldModel);
                counter = (counter + 1) % amountOfStepsBetweenKillingSprees;
            }
        }

        private void UpdateHypotheses(TestFieldModel testFieldModel)
        {
            foreach (var h in testFieldModel.GameObjects.Where(x => x is Hypothesis)) {
                var hypothesis = h as Hypothesis;
                hypothesis.Angle += _deltaAngle;
                hypothesis.Angle %= 360;


                var deltaPositionX = (int)(_deltaPosition * Math.Sin(hypothesis.Angle * Math.PI / 180));
                var deltaPositionY = (int)(_deltaPosition * Math.Cos(hypothesis.Angle * Math.PI / 180));

                hypothesis.X += deltaPositionX;
                hypothesis.Y += deltaPositionY;
            }
        }

        private void KillBadHypotheses(TestFieldModel testFieldModel)
        {
            var badHypotheses = testFieldModel
                .GameObjects
                .Where(x => x is Hypothesis)
                .Where(h => h.X < 0 ||
                    h.X >= testFieldModel.Width ||
                    h.Y < 0 ||
                    h.Y >= testFieldModel.Height)
                .ToList();
            testFieldModel.GameObjects = testFieldModel
                                                .GameObjects
                                                .Except(badHypotheses)
                                                .ToList();
        }

        private void UpdateWeights(TestFieldModel testFieldModel)
        {
            var possibleRobot = testFieldModel.GameObjects.FirstOrDefault(go => go is Robot) as Robot;
            var distances = CalculateDistanceToBeacons(possibleRobot, testFieldModel);
            distances = distances.Select(d => random.NextGaussian(d, sensorsEps + sensorsEpsCalculatedDelta)).ToArray();
            distances = distances.Select(d => d > beaconRadius ? -1 : d).ToArray();
            var hypotheses = testFieldModel.GameObjects
                .Where(x => x is Hypothesis)
                .Select(x => x as Hypothesis)
                .ToList();
            foreach (var hypothesis in hypotheses) {
                hypothesis.Weight = GetNewWeight(testFieldModel, hypothesis, distances);
            }
        }

        private double GetNewWeight(TestFieldModel testFieldModel, Hypothesis hypothesis, double[] distancesFromSensors)
        {
            double result = 1;
            var distancesForHypothesis = CalculateDistanceToBeacons(hypothesis, testFieldModel);
            for (int i = 0; i < distancesFromSensors.Length; i++) {
                var expected = distancesFromSensors[i];
                if (expected < 0)
                    continue;
                var recieved = distancesForHypothesis[i];
                result *= GetWeightFromNormalDistribution(recieved, expected, sensorsEps);
            }
            return result;
        }

        private double GetWeightFromNormalDistribution(double recieved, double expected, double eps)
        {
            var deltaSquare = Math.Pow(recieved - expected, 2);
            var deltaEps = Math.Pow(eps, 2);
            var weight = Math.Pow(Math.E, -deltaSquare / deltaEps) / Math.Sqrt(2 * Math.PI * deltaEps);
            return weight;
        }

        private double[] CalculateDistanceToBeacons(GameObject gameObject, TestFieldModel testFieldModel)
        {
            var beacons = testFieldModel.GameObjects.Where(x => x is Beacon);
            var distances = beacons.Select(beacon => CalculateDistanceToBeacon(gameObject, beacon)).ToArray();

            return distances;
        }

        private double CalculateDistanceToBeacon(GameObject gameObject, GameObject beacon)
        {
            var deltaX = gameObject.X - beacon.X;
            var deltaY = gameObject.Y - beacon.Y;
            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return distance;
        }

        private void KillTheWeakHypotheses(TestFieldModel testFieldModel)
        {
            var hypotheses = testFieldModel.GameObjects
                .Where(x => x is Hypothesis)
                .Select(x => x as Hypothesis)
                .ToList();
            var amountToKill = (int)(hypotheses.Count * 0.75);
            var killed = hypotheses.OrderBy(h => h.Weight).Take(amountToKill);
            testFieldModel.GameObjects = testFieldModel.GameObjects.Except(killed).ToList();
        }

        private void GenerateSomeNewHypotheses(TestFieldModel testFieldModel)
        {
            var hypotheses = testFieldModel.GameObjects
                .Where(x => x is Hypothesis)
                .Select(x => x as Hypothesis)
                .ToList();
            if (hypotheses.Count == 0) {
                GenerateHypothesesFromScratch(testFieldModel);
            }
            else {
                RegenerateHypotheses(testFieldModel, hypotheses);
            }
        }

        private void RegenerateHypotheses(TestFieldModel testFieldModel, List<Hypothesis> existingHypotheses)
        {
            var random = new Random();
            var amountToCreate = maxHypothesisCount - existingHypotheses.Count;
            var hypotheses = new List<Hypothesis>();
            for (int i = 0; i < amountToCreate; i++) {
                var parentIndex = random.Next(existingHypotheses.Count);
                var parent = existingHypotheses[parentIndex];

                var newX = (int)(random.NextGaussian(parent.X, 3));
                var newY = (int)(random.NextGaussian(parent.Y, 3));
                var newAngle = (int)(random.NextGaussian(parent.Angle, 3));

                var hypothesis = new Hypothesis(newX, newY, newAngle);
                hypotheses.Add(hypothesis);
            }
            testFieldModel.GameObjects.AddRange(hypotheses);
        }

        private void GenerateHypothesesFromScratch(TestFieldModel testFieldModel)
        {
            var random = new Random();
            var hypotheses = new List<Hypothesis>();
            for (int i = 0; i < maxHypothesisCount; i++) {
                var newX = (int)(random.NextDouble() * testFieldModel.Width);
                var newY = (int)(random.NextDouble() * testFieldModel.Height);
                var newAngle = (int)(random.NextDouble() * 360 - 180);
                var hypothesis = new Hypothesis(newX, newY, newAngle);
                hypothesis.Weight = 1 / maxHypothesisCount;
                hypotheses.Add(hypothesis);
            }
            testFieldModel.GameObjects.AddRange(hypotheses);
        }
    }
}
