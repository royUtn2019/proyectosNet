using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneridValidador;
using WMPLib;

namespace VerificarSistemasTraza
{
     
    public partial class Form1 : Form
    {
        #region Inicializacion
        VerificadorSisten verificar = new VerificadorSisten();
       private static readonly Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
       public WindowsMediaPlayer sonido;
       string urlAnmat = configuration.AppSettings.Settings["UrlAnmatDefinitivo"].Value;  
       string urlSedr = configuration.AppSettings.Settings["UrlSedronarDefinitivo"].Value; 
       string urlFit = configuration.AppSettings.Settings["UrlFitoDefinitivo"].Value;
       string urlVet = configuration.AppSettings.Settings["UrlVeteDefinitivo"].Value; 
       string urlProdMed = configuration.AppSettings.Settings["UrlProdMedDefinitivo"].Value;
       private string urlSass = configuration.AppSettings.Settings["UrlSassDefinitivo"].Value;

       const int DEFAULT_TIME = 2000;
       Thread t0;
       Thread t1;
       Thread t2;
       Thread t3;
       Thread t4;
       Thread t5;

       Screen screen = Screen.PrimaryScreen;

        #endregion

       public Form1()
        {
            InitializeComponent();
            ejecutarSonido();
            realizarTarea();
            timer1.Interval = 250000;
            timer1.Start();
        }

       #region procesos y eventos
       private void realizarTarea()
        {
            
            limpiarFormulario();
            t0 = new Thread(inicializarProssAnmat);
            t0.Start();

            t1 = new Thread(inicializarProssSedronar);
            t1.Start();

            t2 = new Thread(inicializarProssFito);
            t2.Start();

            t3 = new Thread(inicializarProssVete);
            t3.Start();

            t4 = new Thread(inicializarProssProdMed);
            t4.Start();

            t5 = new Thread(inicializarProssSass);
            t5.Start();

            
        }


       private void inicializarProssAnmat()
       {
           Thread.Sleep(100);
           Invoke((MethodInvoker)(() => mostrarProcesandoAnmat()));
           Thread.Sleep(DEFAULT_TIME);
           if (InvokeRequired) Invoke(new Action(finishProcessAnmat));

       }

       private void inicializarProssSedronar()
       {
           Thread.Sleep(300);
           Invoke((MethodInvoker)(() => mostrarProcesandoSedro()));
           Thread.Sleep(DEFAULT_TIME+100);
           if (InvokeRequired) Invoke(new Action(finishProcessSedro));
       }

       private void inicializarProssFito()
       {
           Thread.Sleep(500);
           Invoke((MethodInvoker)(() => mostrarProcesandoFito()));
           Thread.Sleep(DEFAULT_TIME+200);
           if (InvokeRequired) Invoke(new Action(finishProcessFito));

       }

       private void inicializarProssVete()
       {
           Thread.Sleep(700);
           Invoke((MethodInvoker)(() => mostrarProcesandoVete()));
           Thread.Sleep(DEFAULT_TIME+300);
           if (InvokeRequired) Invoke(new Action(finishProcessVete));
       }

       private void inicializarProssProdMed()
       {
           Thread.Sleep(900);
           Invoke((MethodInvoker)(() => mostrarProcesandoProdMed()));
           Thread.Sleep(DEFAULT_TIME+400);
           if (InvokeRequired) Invoke(new Action(finishProcessProdMed));
       }

       private void inicializarProssSass()
       {
           Thread.Sleep(1100);
           Invoke((MethodInvoker)(() => mostrarProcesandoSass()));
           Thread.Sleep(DEFAULT_TIME+500);
           if (InvokeRequired) Invoke(new Action(finishProcessSass));
       }

       private void mostrarProcesandoAnmat()
       {
           p1.Visible = true;
       }

       private void mostrarProcesandoSedro()
       {
           p2.Visible = true;
       }

       private void mostrarProcesandoFito()
       {
           p3.Visible = true;
       }

       private void mostrarProcesandoVete()
       {
           p4.Visible = true;
       }

       private void mostrarProcesandoProdMed()
       {
           p5.Visible = true;
       }

       private void mostrarProcesandoSass()
       {
           p6.Visible = true;
       }

       private void anmatProceso()
       {
           try
           {
               p1.Visible = true;
               if (verificar.integridadSistemas(urlAnmat))
               {
                   p1.Visible = false;
                   string sistema = "Anmat ";
                   checkAnmat.Visible = true;
                   lbAnmat.Text = "El sistema de " + sistema + " funciona correctamente. ";

               }

           }
           catch (Exception ex)
           {
               p1.Visible = false;
               string sistema = "Anmat ";
               pErrorAnmat.Visible = true;
               lbAnmat.Text = "Error en " + sistema + ". " + ex.Message;
           }
       }

       private void sedronarProceso()
       {
           try
           {
               p2.Visible = true;
               if (verificar.integridadSistemas(urlSedr))
               {
                   p2.Visible = false;
                   const string sistema = "Sedronar ";
                   checkSedronar.Visible = true;
                   lbSedronar.Text = "El sistema de " + sistema + " funciona correctamente. ";

               }

           }
           catch (Exception ex)
           {
               p2.Visible = false;
               const string sistema = "Sedronar ";
               pErrorSe.Visible = true;
               lbSedronar.Text = "Error en " + sistema + ". " + ex.Message;
           }
       }


       private void fitoProceso()
       {
           try
           {
               p3.Visible = true;
               if (verificar.integridadSistemas(urlFit))
               {
                   p3.Visible = false;
                   const string sistema = "Fitosanitarios ";
                   checkFito.Visible = true;
                   lbFito.Text = "El sistema de " + sistema + " funciona correctamente. ";

               }

           }
           catch (Exception ex)
           {
               p3.Visible = false;
               const string sistema = "Fitosanitarios ";
               pErrorFito.Visible = true;
               lbFito.Text = "Error en " + sistema + ". " + ex.Message;
           }
       }


       private void veteProceso()
       {
           try
           {
               p4.Visible = true;
               if (verificar.integridadSistemas(urlVet))
               {
                   p4.Visible = false;
                   const string sistema = "Veterinarios ";
                   checkVete.Visible = true;
                   lbVet.Text = "El sistema de " + sistema + " funciona correctamente. ";

               }

           }
           catch (Exception ex)
           {

               p4.Visible = false;
               const string sistema = "Veterinarios ";
               pErrorVet.Visible = true;
               lbVet.Text = "Error en " + sistema + ". " + ex.Message;
           }

       }
        
       private void prodMedProceso()
        {
            try
            {
                p5.Visible = true;
                if (verificar.integridadSistemas(urlProdMed))
                {
                    p5.Visible = false;
                    const string sistema = "Productos Medicos ";
                    checkProMed.Visible = true;
                    lbProdMed.Text = "El sistema de " + sistema + " funciona correctamente. ";

                }
               
            }
            catch (Exception ex)
            {
                p5.Visible = false;
                const string sistema = "Productos Medicos ";
                pErrorProMe.Visible = true;
                lbProdMed.Text = "Error en " + sistema + ". " + ex.Message;
            }


            
        }

       private void sassProceso()
        {
            try
            {
                p6.Visible = true;
                if (verificar.integridadSistemas(urlSass))
                {
                    p6.Visible = false;
                    const string sistema = "Sass ";
                    checkSass.Visible = true;
                    lbSass.Text = "El sistema de " + sistema + " funciona correctamente. ";

                }

            }
            catch (Exception ex)
            {
                p6.Visible = false;
                const string sistema = "Sass ";
                pErrorSass.Visible = true;
                lbSass.Text = "Erro en " + sistema + ". " + ex.Message;
            }


        }


       private void finishProcessAnmat()
        {
            anmatProceso();
            p1.Visible = false;
            t0.Abort();
        }

       private void finishProcessSedro()
        {
            sedronarProceso();
            p2.Visible = false;
            t1.Abort(); 
        }

       private void finishProcessFito()
        {
            fitoProceso();
            p3.Visible = false;
            t2.Abort();
        }

       private void finishProcessVete()
        {
            veteProceso();
            p4.Visible = false;
            t3.Abort();
        }

       private void finishProcessProdMed()
        {
            prodMedProceso();
            p5.Visible = false;
            t4.Abort();
        }

       private void finishProcessSass()
        {
            sassProceso();
            p6.Visible = false;
            t5.Abort();
        }

       private void limpiarFormulario()
        {
            checkAnmat.Visible = false;
            checkSedronar.Visible = false;
            checkFito.Visible = false;
            checkVete.Visible = false;
            checkProMed.Visible = false;
            checkSass.Visible = false;
            pErrorAnmat.Visible = false;
            pErrorSe.Visible = false;
            pErrorFito.Visible = false;
            pErrorVet.Visible = false;
            pErrorProMe.Visible = false;
            pErrorSass.Visible = false;

            lbAnmat.Text = "";
            lbSedronar.Text = "";
            lbFito.Text = "";
            lbVet.Text = "";
            lbProdMed.Text = "";
            lbSass.Text = "";

        }

       private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

       private void pBoxCancelar_MouseEnter(object sender, EventArgs e)
        {
            pBoxCancelar1.Visible = true;
            pBoxCancelar.Visible = false;
            Cursor = Cursors.Hand;
        }

       private void pBoxCancelar_MouseLeave(object sender, EventArgs e)
        {
            pBoxCancelar1.Visible = true;
            pBoxCancelar.Visible = false;
            Cursor = Cursors.Arrow;
        }

       private void pBoxCancelar1_MouseEnter(object sender, EventArgs e)
        {
            pBoxCancelar1.Visible = true;
            pBoxCancelar.Visible = false;
            Cursor = Cursors.Hand;
        }

       private void pBoxCancelar1_MouseLeave(object sender, EventArgs e)
        {
            pBoxCancelar.Visible = true;
            pBoxCancelar1.Visible = false;
            Cursor = Cursors.Arrow;
        }

       private void pBoxCancelar1_Click(object sender, EventArgs e)
        {
            Close();
        }

       private void Form1_Load(object sender, EventArgs e)
        {
            int Height = screen.Bounds.Width;
            int Width = screen.Bounds.Height;
            Location = new Point(Height - (Size.Width+5), Width - (Size.Height+43));
        }

       public void ejecutarSonido()
        {
            sonido = new WindowsMediaPlayer();
            sonido.URL = Application.StartupPath + @"\Mp3\noti.mp3";
        }
       #endregion
    }
}
