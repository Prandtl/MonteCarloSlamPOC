using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MonteCarloSlamPOC.TestObjects;

namespace MonteCarloSlamPOC
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(
				new TestField(
					new TestFieldModel()
					{
						Width = 750,
						Height = 500,
						GameObjects = new List<GameObject>()
						{
							new GameObject(0,0),
							new GameObject(10,10),
							new GameObject(50,50),
							new GameObject(150,150),
							new GameObject(300,300),
							new GameObject(450,270),
							new GameObject(700, 450),
							new GameObject(20,40),
						}
					}));
		}
	}
}
