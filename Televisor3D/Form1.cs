using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using OpenTK;

namespace Televisor3D
{
    public partial class Form1 : Form
    {
        private Escenario escenario;
        Game game;

        private System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 16;
            timer.Tick += OnTimerTick;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            escenario = new Escenario(new Vector(0,0,0));
            escenario.Adicionar("Televisor1", ArchivoJson<Objeto>.Cargar("../../Objetos/tele.json"));
            escenario.Adicionar("Jarron1", ArchivoJson<Objeto>.Cargar("../../Objetos/Jarron.json"));
            escenario.Adicionar("Reproductor1", ArchivoJson<Objeto>.Cargar("../../Objetos/Reproductor.json"));
            escenario.Adicionar("Pelota", ArchivoJson<Objeto>.Cargar("../../Objetos/Pelota.json"));
            // escenario = ArchivoJson<Escenario>.Cargar("../../Objetos/escenario.json");

            Thread correr = new Thread(Ejecutar);
            correr.Start();

            listBox1.Items.Clear();
            listBox1.Items.Add("Escenario");
            foreach (var objeto in escenario.objetos.Keys)
                listBox1.Items.Add(objeto);

            listBox1.SelectedIndex = 0;
            ActualizarInformacionObjetos();
        }
        private void Ejecutar()
        {
            game = new Game(600, 700, "LearnOpenTK");
            game.escenario = this.escenario;
            game.Run();
        }

        private void ActualizarInformacionObjetos()
        {
            listBox2.Items.Clear();
            if (listBox1.SelectedItem.ToString() == "Escenario")
            {
                listBox2.Enabled= false;
                return;
            }
            listBox2.Enabled = true;
            listBox2.Items.Add("Objeto");
            foreach ( var parte in escenario.GetObjeto(listBox1.SelectedItem.ToString()).GetListaDeParte().Keys) 
            {
                listBox2.Items.Add(parte);
            }
            listBox2.SelectedIndex = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarInformacionObjetos();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vector3 factorEscala = ObtenerFactorEscala();

            if (listBox1.SelectedItem.ToString() == "Escenario")
            {
                escenario.Escalar(factorEscala.X, factorEscala.Y, factorEscala.Z);
            }
            else
            {
                string nombreObjeto = listBox1.SelectedItem.ToString();

                if (escenario.objetos.ContainsKey(nombreObjeto))
                {
                    Objeto objeto = escenario.objetos[nombreObjeto];

                    if (listBox2.SelectedItem.ToString() == "Objeto")
                    {
                        // Escalar el objeto completo
                        objeto.Escalar(factorEscala.X, factorEscala.Y, factorEscala.Z);
                    }
                    else
                    {
                        string nombreParte = listBox2.SelectedItem.ToString();

                        if (objeto.GetListaDeParte().ContainsKey(nombreParte))
                        {
                            // Escalar la parte del objeto
                            objeto.GetListaDeParte()[nombreParte].Escalar(factorEscala.X, factorEscala.Y, factorEscala.Z);
                        }
                        else
                        {
                            MessageBox.Show("Seleccione una parte válida del objeto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un objeto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private Vector3 ObtenerFactorEscala()
        {
            float factorEscalaX, factorEscalaY, factorEscalaZ;

            // Intenta convertir el texto del cuadro de texto en un número flotante
            if (float.TryParse(textBox1.Text, out factorEscalaX)&& 
                float.TryParse(textBox1.Text, out factorEscalaY) &&
                float.TryParse(textBox1.Text, out factorEscalaZ))
            {
                // Devuelve el factor de escala si la conversión fue exitosa
                return new Vector3(factorEscalaX, factorEscalaY, factorEscalaZ);
            }
            else
            {
                MessageBox.Show("Factor de escala inválido. Por favor ingrese un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Vector3.One; // Valor predeterminado de factor de escala
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Vector3 angulosRotacion = ObtenerAngulosRotacion();

            if (listBox1.SelectedItem.ToString() == "Escenario")
            {
                escenario.Rotar(angulosRotacion.X, angulosRotacion.Y, angulosRotacion.Z);
            }
            else
            {
                string nombreObjeto = listBox1.SelectedItem.ToString();
                if (listBox2.SelectedItem.ToString() == "Objeto")
                {
                    escenario.GetObjeto(nombreObjeto).Rotar(angulosRotacion.X, angulosRotacion.Y, angulosRotacion.Z);
                }
                else
                {
                    string nombreParte = listBox2.SelectedItem.ToString();
                    escenario.GetObjeto(nombreObjeto).GetParte(nombreParte).Rotar(angulosRotacion.X, angulosRotacion.Y, angulosRotacion.Z);
                }
            }
        }

        private Vector3 ObtenerAngulosRotacion()
        {
            float anguloX, anguloY, anguloZ;

            if (float.TryParse(textBox7.Text, out anguloX) &&
                float.TryParse(textBox8.Text, out anguloY) &&
                float.TryParse(textBox9.Text, out anguloZ))
            {
              
                return new Vector3(anguloX, anguloY, anguloZ);
            }
            else
            {
                MessageBox.Show("Valores de ángulo de rotación inválidos. Por favor ingrese números válidos en los cuadros de texto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Vector3.Zero;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vector3 traslacion = ObtenerTraslacion();

            if (listBox1.SelectedItem.ToString() == "Escenario")
            {
                escenario.Trasladar(traslacion.X, traslacion.Y, traslacion.Z);
            }
            else
            {
                string nombreObjeto = listBox1.SelectedItem.ToString();

                if (escenario.objetos.ContainsKey(nombreObjeto))
                {
                    Objeto objeto = escenario.objetos[nombreObjeto];

                    if (listBox2.SelectedItem.ToString() == "Objeto")
                    {
                        // Trasladar el objeto completo
                        objeto.Trasladar(traslacion.X, traslacion.Y, traslacion.Z);
                    }
                    else
                    {
                        string nombreParte = listBox2.SelectedItem.ToString();

                        if (objeto.GetListaDeParte().ContainsKey(nombreParte))
                        {
                            // Trasladar la parte del objeto
                            objeto.GetListaDeParte()[nombreParte].Trasladar(traslacion.X, traslacion.Y, traslacion.Z);
                        }
                        else
                        {
                            MessageBox.Show("Seleccione una parte válida del objeto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un objeto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Vector3 ObtenerTraslacion()
        {
            float traslacionX, traslacionY, traslacionZ;

            if (float.TryParse(textBox4.Text, out traslacionX) &&
                float.TryParse(textBox5.Text, out traslacionY) &&
                float.TryParse(textBox6.Text, out traslacionZ))
            {
                return new Vector3(traslacionX, traslacionY, traslacionZ);
            }
            else
            {
                MessageBox.Show("Valores de traslación inválidos. Por favor ingrese números válidos en los cuadros de texto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Vector3.Zero;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedObject = listBox1.SelectedItem.ToString();
            if (selectedObject != "Escenario" && escenario.objetos.ContainsKey(selectedObject))
            {
                Objeto objeto = escenario.objetos[selectedObject];
                objeto.IniciarRebote();
                timer.Start();
            }
            else
            {
                MessageBox.Show("Seleccione un objeto valido para rebotar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            float deltaTime = timer.Interval / 1000.0f;
            string selectedObject = listBox1.SelectedItem.ToString();
            if (selectedObject != "Escenario" && escenario.objetos.ContainsKey(selectedObject))
            {
                Objeto objeto = escenario.objetos[selectedObject];
                objeto.Rebotar(deltaTime);
                Console.WriteLine("Hello World");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string selectedObject = listBox1.SelectedItem.ToString();
            if (selectedObject != "Escenario" && escenario.objetos.ContainsKey(selectedObject))
            {
                Objeto objeto = escenario.objetos[selectedObject];
                objeto.DetenerRebote(); // Detiene el rebote en todas las partes del objeto
                timer.Stop(); // Detiene el temporizador
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            foreach (var objeto in escenario.objetos.Values)
            {
            }
        }
    }
}
