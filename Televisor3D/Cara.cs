using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;

namespace Televisor3D
{
    class Cara
    {
        public Dictionary<string, Vector> listaVertices;
        public Color Color;
        public Vector Centro;
        public Vector vectorT = new Vector(0f, 0f, 0f);
        public PrimitiveType Polygon;

        public Cara(Dictionary<string, Vector> lisVertices, Color color, Vector centro)
        {
            this.listaVertices = lisVertices;
            this.Color = color;
            this.Centro = centro;
        }

        public void Adicionar(string key, Vector verticeAdicionar)
        {
            listaVertices.Add(key, verticeAdicionar);
        }

        public void Eliminar(string key, Vector verticeEliminar)
        {
            listaVertices.Remove(key);
        }

        public void SetCentro(Vector centro)
        {
            this.Centro = centro;
        }

        public Vector GetCentro()
        {
            return Centro;
        }
        
        public void Rotar(double aX, double aY, double aZ)
        {
            foreach (var vertice in listaVertices)
            {
                double x = vertice.Value.X;
                double y = vertice.Value.Y;
                double z = vertice.Value.Z;

                // Aplicar rotación en el eje X
                double newY = y * Math.Cos(aX) - z * Math.Sin(aX);
                double newZ = y * Math.Sin(aX) + z * Math.Cos(aX);
                y = newY;
                z = newZ;

                // Aplicar rotación en el eje Y
                double newX = x * Math.Cos(aY) + z * Math.Sin(aY);
                newZ = -x * Math.Sin(aY) + z * Math.Cos(aY);
                x = newX;
                z = newZ;

                // Aplicar rotación en el eje Z
                newX = x * Math.Cos(aZ) - y * Math.Sin(aZ);
                newY = x * Math.Sin(aZ) + y * Math.Cos(aZ);
                x = newX;
                y = newY;

                vertice.Value.X = x;
                vertice.Value.Y = y;
                vertice.Value.Z = z;
            }
        }

        public void Trasladar(double x, double y, double z)
        {
            foreach (var vertice in listaVertices)
            {
                vertice.Value.X += x;
                vertice.Value.Y += y;
                vertice.Value.Z += z;
            }
        }

        public void Escalar(double x, double y, double z)
        {
            foreach (var vertice in listaVertices)
            {
                Console.WriteLine($"Antes: X={vertice.Value.X}, Y={vertice.Value.Y}, Z={vertice.Value.Z}");

                vertice.Value.X *= x;
                vertice.Value.Y *= y;
                vertice.Value.Z *= z;

                Console.WriteLine($"Después: X={vertice.Value.X}, Y={vertice.Value.Y}, Z={vertice.Value.Z}");
            }
        }

        public virtual void Dibujar(Vector centroMP) 
        {
            var centroMC = this.Centro + centroMP;

            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(Color);
            //va iterando sobre cada vertice en nuestra lista de vertices
            foreach (var vertice in listaVertices) 
            {
                // Añadimos el punto al vértice y lo dibujamos
                GL.Vertex3( vertice.Value.X + centroMC.X, vertice.Value.Y + centroMC.Y, vertice.Value.Z + centroMC.Z);
            }
            GL.End();
        }

        public void Dibujar()
        {
            var centroMC = this.Centro;

            GL.Begin(Polygon);
            GL.Color3(Color);
            //va iterando sobre cada vertice en nuestra lista de vertices
            foreach (var vertice in listaVertices)
            {
                // Añadimos el punto al vértice y lo dibujamos
                GL.Vertex3(vertice.Value.X + centroMC.X, vertice.Value.Y + centroMC.Y, vertice.Value.Z + centroMC.Z);
            }
            GL.End();
        }

    }
}
