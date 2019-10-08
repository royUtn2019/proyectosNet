using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
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
       string urlSass = configuration.AppSettings.Settings["UrlSassDefinitivo"].Value;
       string urlVade = configuration.AppSettings.Settings["UrlVademecum"].Value;

       Sistema siste = new Sistema();
       List<Sistema> listSis = new List<Sistema>();
       const int DEFAULT_TIME = 2000;
       Thread t0,t1,t2,t3,t4,t5, t6;

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

            t6 = new Thread(inicializarProssVademecum);
            t6.Start();
           
        }


       private void anmatProceso()
       {
           p1.Visible = true;
           siste = new Sistema();
           try
           {
               if (verificar.integridadSistemas(urlAnmat))
               {

                   string sistema = "Anmat ";
                   checkAnmat.Visible = true;
                   lbAnmat.Text = "El sistema de " + sistema.ToUpper() + " funciona correctamente. ";
                   

               }

           }
           catch (Exception ex)
           {
               string sistema = "Anmat ";
               pErrorAnmat.Visible = true;
               lbAnmat.Text = "Error en " + sistema.ToUpper() + ". " + ex.Message;
               siste.NombreSistema = sistema;
               siste.Estado = false;
               siste.Error = ex.Message;
               listSis.Add(siste);
           }
       }


       private void sedronarProceso()
       {
           p2.Visible = true;
           siste = new Sistema();
           try
           {
               if (verificar.integridadSistemas(urlSedr))
               {
                   const string sistema = "Sedronar ";
                   checkSedronar.Visible = true;
                   lbSedronar.Text = "El sistema de " + sistema.ToUpper() + " funciona correctamente. ";
                   
               }

           }
           catch (Exception ex)
           {
               const string sistema = "Sedronar ";
               pErrorSe.Visible = true;
               lbSedronar.Text = "Error en " + sistema.ToUpper() + ". " + ex.Message;
               siste.NombreSistema = sistema;
               siste.Estado = false;
               siste.Error = ex.Message;
               listSis.Add(siste);
           }
       }

       private void fitoProceso()
       {
           p3.Visible = true;
           siste = new Sistema();
           try
           {
               if (verificar.integridadSistemas(urlFit))
               {
                   const string sistema = "Fitosanitarios ";
                   checkFito.Visible = true;
                   lbFito.Text = "El sistema de " + sistema.ToUpper() + " funciona correctamente. ";
                   
               }

           }
           catch (Exception ex)
           {
               const string sistema = "Fitosanitarios ";
               pErrorFito.Visible = true;
               lbFito.Text = "Error en " + sistema.ToUpper() + ". " + ex.Message;
               siste.NombreSistema = sistema;
               siste.Estado = false;
               siste.Error = ex.Message;
               listSis.Add(siste);
           }
       }
       private void veteProceso()
       {
           siste = new Sistema();
           p4.Visible = true;
           try
           {
               if (verificar.integridadSistemas(urlVet))
               {
                   const string sistema = "Veterinarios ";
                   checkVete.Visible = true;
                   lbVet.Text = "El sistema de " + sistema.ToUpper() + " funciona correctamente. ";
                   
               }

           }
           catch (Exception ex)
           {

               const string sistema = "Veterinarios ";
               pErrorVet.Visible = true;
               lbVet.Text = "Error en " + sistema.ToUpper() + ". " + ex.Message;
               siste.NombreSistema = sistema;
               siste.Estado = false;
               siste.Error = ex.Message;
               listSis.Add(siste);
           }

       }


       private void prodMedProceso()
       {
           siste = new Sistema();
           p5.Visible = true;
           try
           {
               if (verificar.integridadSistemas(urlProdMed))
               {
                   const string sistema = "Productos Medicos ";
                   checkProMed.Visible = true;
                   lbProdMed.Text = "El sistema de " + sistema.ToUpper() + " funciona correctamente. ";
                   
               }

           }
           catch (Exception ex)
           {
               const string sistema = "Productos Medicos ";
               pErrorProMe.Visible = true;
               lbProdMed.Text = "Error en " + sistema.ToUpper() + ". " + ex.Message;
               siste.NombreSistema = sistema;
               siste.Estado = false;
               siste.Error = ex.Message;
               listSis.Add(siste);
           }

       }
       private void sassProceso()
       {
           siste = new Sistema();
           p6.Visible = true;
           try
            {
                
                if (verificar.integridadSistemas(urlSass))
                {
                    const string sistema = "Sass ";
                    checkSass.Visible = true;
                    lbSass.Text = "El sistema de " + sistema.ToUpper() + " funciona correctamente. ";
                    
                }
               
            }
            catch (Exception ex)
            {
                const string sistema = "Sass ";
                pErrorSass.Visible = true;
                lbSass.Text = "Error en " + sistema.ToUpper() + ". " + ex.Message;
                siste.NombreSistema = sistema;
                siste.Estado = false;
                siste.Error = ex.Message;
                listSis.Add(siste);
            }

            
        }

        
        

        private void vademecumProceso()
        {
            siste = new Sistema();
            p7.Visible = true;
            try
            {

                if (verificar.integridadSistemas(urlVade))
                {
                    const string sistema = "Vademecum ";
                    checkVade.Visible = true;
                    lbVade.Text = "El sistema de " + sistema.ToUpper() + " funciona correctamente. ";
                }

            }
            catch (Exception ex)
            {
                const string sistema = "Vademecum ";
                pErrorVade.Visible = true;
                lbVade.Text = "Error en " + sistema.ToUpper() + ". " + ex.Message;
                siste.NombreSistema = sistema;
                siste.Estado = false;
                siste.Error = ex.Message;
                listSis.Add(siste);
            }
        }

        private void enviarMsn(List<Sistema> sistemas)
        {
            if (sistemas.Count!= 0)
            {
                
                string num = "+5491130378572";
                string yourId = "oepYSyfvNESguvIRDvcUonJvZ2VyX2RvdF91dG5fYXRfZ21haWxfZG90X2NvbQ==";
                string yourMobile = num;  //"+5491130378572";;

                string t = null;
                for (int i = 0; i < sistemas.Count; i++)
                {
                    if (i == sistemas.Count)
                    {
                        t = sistemas[i].NombreSistema;
                    }
                    else
                    {
                        t = t + sistemas[i].NombreSistema + ", ";
                    }
                    
                }

                
                string yourMessage = "Por favor verificar integridad de los sistemas de Trazabilidad, hay sistemas caidos: \n" +t  + "\n";

                try
                {
                    string url = "https://NiceApi.net/API";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Headers.Add("X-APIId", yourId);
                    request.Headers.Add("X-APIMobile", yourMobile);
                    using (StreamWriter streamOut = new StreamWriter(request.GetRequestStream()))
                    {
                        streamOut.Write(yourMessage);
                    }
                    using (StreamReader streamIn = new StreamReader(request.GetResponse().GetResponseStream()))
                    {
                        Console.WriteLine(streamIn.ReadToEnd());
                    }
                }
                catch (SystemException se)
                {
                    Console.WriteLine(se.Message);
                }
                Console.ReadLine();
            }
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
            Thread.Sleep(DEFAULT_TIME);
            if (InvokeRequired) Invoke(new Action(finishProcessSedro)); 
        }

        private void inicializarProssFito()
        {
            Thread.Sleep(500);
            Invoke((MethodInvoker)(() => mostrarProcesandoFito()));
            Thread.Sleep(DEFAULT_TIME);
            if (InvokeRequired) Invoke(new Action(finishProcessFito));

        }

        private void inicializarProssVete()
        {
            Thread.Sleep(700);
            Invoke((MethodInvoker)(() => mostrarProcesandoVete()));
            Thread.Sleep(DEFAULT_TIME);
            if (InvokeRequired) Invoke(new Action(finishProcessVete));
        }

        private void inicializarProssProdMed()
        {
            Thread.Sleep(900);
            Invoke((MethodInvoker)(() => mostrarProcesandoProdMed()));
            Thread.Sleep(DEFAULT_TIME);
            if (InvokeRequired) Invoke(new Action(finishProcessProdMed));
        }

        private void inicializarProssSass()
        {
            Thread.Sleep(1100);
            Invoke((MethodInvoker)(() => mostrarProcesandoSass()));
            Thread.Sleep(DEFAULT_TIME);
            if (InvokeRequired) Invoke(new Action(finishProcessSass));
        }

        private void inicializarProssVademecum()
        {
            Thread.Sleep(1300);
            Invoke((MethodInvoker)(() => mostrarProcesandoVademecum()));
            Thread.Sleep(DEFAULT_TIME);
            if (InvokeRequired) Invoke(new Action(finishProcessVademecum));
        }

        private void mostrarProcesandoVademecum()
        {
            vademecumProceso();
            p7.Visible = true;
        }

        private void mostrarProcesandoAnmat()
        {
            anmatProceso();
            p1.Visible = true;
        }

        private void mostrarProcesandoSedro()
        {
            sedronarProceso();
            p2.Visible = true;
        }

        private void mostrarProcesandoFito()
        {
            fitoProceso();
            p3.Visible = true;
        }

        private void mostrarProcesandoVete()
        {
            veteProceso();
            p4.Visible = true;
        }

        private void mostrarProcesandoProdMed()
        {
            prodMedProceso();
            p5.Visible = true;
        }

        private void mostrarProcesandoSass()
        {
            sassProceso();
            p6.Visible = true;
        }

        private void finishProcessAnmat()
        {
            p1.Visible = false;
            t0.Abort();
        }

        private void finishProcessSedro()
        {
            p2.Visible = false;
            t1.Abort(); 
        }

        private void finishProcessFito()
        {
            p3.Visible = false;
            t2.Abort();
        }

        private void finishProcessVete()
        {
            p4.Visible = false;
            t3.Abort();
        }

        private void finishProcessProdMed()
        {
            p5.Visible = false;
            t4.Abort();
        }

        private void finishProcessSass()
        {
            p6.Visible = false;
            t5.Abort();
        }

        private void finishProcessVademecum()
        {
            p7.Visible = false;
            t6.Abort();
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
            enviarMsn(listSis);
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
            Location = new Point(Height - Size.Width, Width - (Size.Height+40));
        }

        public void ejecutarSonido()
        {
            sonido = new WindowsMediaPlayer();
            sonido.URL = Application.StartupPath + @"\Mp3\noti.mp3";
        }
       #endregion
    }
}
