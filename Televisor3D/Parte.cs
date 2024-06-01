using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Televisor3D
{
    class Parte
    {
        public Dictionary<string, Cara> dCaras;
        public Vector Centro;
        public Vector velocidad = new Vector(0,0,0);
        public Vector aceleracion = new Vector(0, -9.8, 0);
        public double limiteInferior, limiteSuperior;
        public bool rebotando = false;
        public Parte(Dictionary<string, Cara> dCaras, Vector centro)
        {
            this.dCaras = dCaras;
            this.Centro = centro;

            foreach (var cara in dCaras)
            {
                cara.Value.Centro = Centro + cara.Value.GetCentro();
            }
        }

        public void Adicionar(string key, Cara verticeAdicionar)
        {
            dCaras.Add(key, verticeAdicionar);
        }

        public void Eliminar(string key, Cara verticeEliminar)
        {
            dCaras.Remove(key);
        }

        public void SetCentro(Vector centro)
        {
            this.Centro = centro;
        }

        public Vector GetCentro()
        {
            return Centro;
        }

        public Cara GetCara(string key)
        {
            return dCaras[key];
        }

        public Dictionary<String, Cara> GetListaDeCaras()
        {
            return dCaras;
        }

        public void Rotar(double x, double y, double z)
        {
            foreach (var cara in dCaras)
            {
                cara.Value.Rotar(x, y, z);
            }
        }

        public void Trasladar(double x, double y, double z)
        {
            foreach (var cara in dCaras)
            {
                cara.Value.Trasladar(x, y, z);
            }
        }

        public void Escalar(double x, double y, double z)
        {
            foreach (var cara in dCaras)
            {
                cara.Value.Escalar(x, y, z);
            }
        }

        public void Dibujar(Vector centroMO)
        {
            var centroMP = this.Centro + centroMO;

            foreach(var pcara in dCaras)
            {
                pcara.Value.Dibujar(centroMP);
            }
        }

        public void Rebotar( double deltaTime)
        {
            if (!rebotando) return;

            // Aplica la lógica de rebote a la parte individual
            if (Centro.Y <= limiteInferior || Centro.Y >= limiteSuperior)
            {
                velocidad.Y = -velocidad.Y; // Invierte la velocidad vertical para simular el rebote
            }

            // Actualiza la posición de la parte en función de la velocidad
            Centro += velocidad;
        }

        public void IniciarRebote()
        {
            rebotando = true;
        }

        public void DetenerRebote()
        {
            rebotando = false;
        }
    }
}
