using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Televisor3D
{
    class Game: GameWindow
    {
        public Escenario escenario;
        public Game(int width, int height, string tittle): base(width, height, GraphicsMode.Default, tittle) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
          //  GL.Rotate(1.0f, 0.0f, 0.1f, 0.0f);
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(Color4.AliceBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-3.5f, 3.5f, -5f, 2f, -3f, 3f);
            GL.MatrixMode(MatrixMode.Modelview);

            // escenario = new Escenario(new Vector(0, 0f, 0));
            //escenario.Adicionar("Televisor1", ArchivoJson<Objeto>.Cargar("../../Objetos/tele.json"));
            //escenario.Adicionar("Jarron1", ArchivoJson<Objeto>.Cargar("../../Objetos/Jarron.json"));
            //escenario.Adicionar("Reproductor1", ArchivoJson<Objeto>.Cargar("../../Objetos/Reproductor.json"));
            // escenario = ArchivoJson<Escenario>.Cargar("../../Objetos/escenario.json");
            if (escenario != null)
            {
                escenario.Dibujar();
            }

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
        protected override void OnResize(EventArgs e) {  
            GL.Viewport(0,0, Width, Height);
            base.OnResize(e); 
        }
    }
}
