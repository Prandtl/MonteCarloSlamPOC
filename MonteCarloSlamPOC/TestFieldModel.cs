

using System;
using System.Collections.Generic;
using MonteCarloSlamPOC.TestObjects;
using System.Linq;

namespace MonteCarloSlamPOC
{
    public class TestFieldModel
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public List<GameObject> GameObjects { get; set; }

        public event Action ModelChanged;

        public void InvokeModelChanged()
        {
            ModelChanged?.Invoke();
        }
    }
}
