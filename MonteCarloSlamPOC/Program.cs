using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MonteCarloSlamPOC.TestObjects;

namespace MonteCarloSlamPOC
{
    static class Program
    {
        private const int TestFieldWidth = 750;
        private const int TestFieldHeight = 400;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AllocConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Application.Run(
                new TestField(
                    new TestFieldModel() {
                        Width = TestFieldWidth,
                        Height = TestFieldHeight,
                        GameObjects = new List<GameObject>()
                        {
                            new Beacon(40, 40, Brushes.Coral),
                            new Beacon(40, TestFieldHeight - 40, Brushes.DarkMagenta),
                            new Beacon(TestFieldWidth - 40, 40, Brushes.MediumVioletRed),
                            new Beacon(TestFieldWidth - 40, TestFieldHeight- 40, Brushes.Green),
                            new Robot(TestFieldWidth / 2, TestFieldHeight / 2),
                            new OdometerEstimation(TestFieldWidth / 2, TestFieldHeight / 2)
                        }
                    }));
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
