using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Televisor3D
{
    class Escenario
    {
        public Dictionary<string, Objeto> objetos;
        public Vector Centro;

        public Escenario(Vector centro)
        {
            this.Centro = centro;
            objetos = new Dictionary<string, Objeto>();
            //InicializarObjetos();
        }

        public Escenario(Dictionary<string, Objeto> deObjetos, Vector centro)
        {
            this.objetos = deObjetos;
            this.Centro = centro;

            foreach (var objeto in deObjetos)
            {
                Vector nuevoCentrObjeto = objeto.Value.GetCentro() + Centro;
                objeto.Value.Centro = nuevoCentrObjeto;

                foreach (var parte in objeto.Value.partes)
                {
                    Vector nuevoCentroParte = parte.Value.GetCentro() + nuevoCentrObjeto;
                    parte.Value.Centro = nuevoCentroParte;

                    foreach(var cara in parte.Value.dCaras)
                    {
                        Vector nuevoCentroCara = cara.Value.GetCentro() + nuevoCentroParte;
                        cara.Value.Centro = nuevoCentroCara;
                    }

                }
            }
        }


        public void Adicionar(string key, Objeto escenarioAdicionar)
        {
            objetos.Add(key, escenarioAdicionar);
        }

        public void Eliminar(String key, Objeto escenarioEliminar)
        {
            objetos.Remove(key);
        }

        public Objeto GetObjeto(String key)
        {
            return objetos[key];
        }

        public Dictionary<String, Objeto> GetListaObjetos()
        {
            return objetos;
        }

        public void Rotar(double x, double y, double z)
        {
            foreach(var objeto in objetos)
            {
                objeto.Value.Rotar(x,y, z);
            }
        }

        public void Trasladar(double x, double y, double z)
        {
            foreach (var objeto in objetos)
            {
                objeto.Value.Trasladar(x, y, z);
            }
        }

        public void Escalar(double x, double y, double z)
        {
            foreach( var objeto in objetos)
            {
                objeto.Value.Escalar(x,y, z);
            }
        }
        public void Dibujar()
        {  
            foreach (var objeto in objetos)
            {
                objeto.Value.Dibujar(this.Centro);
            }
        }
    }
}
