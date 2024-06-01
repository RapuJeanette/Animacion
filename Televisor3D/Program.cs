using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Televisor3D
{
    internal class Program
    {
        [STAThread] // Añadir este atributo a Main
        static void Main(string[] args)
        {
           // Console.WriteLine("Hello World");
           // Game game = new Game(800, 600, "LearnOpenTK");
           // game.Run(60.0);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            Application.Run(form);


        }
    }
}
