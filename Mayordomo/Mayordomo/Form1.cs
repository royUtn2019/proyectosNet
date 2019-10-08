using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenPop.Pop3;
using WMPLib;
using Message = OpenPop.Mime.Message;

namespace Mayordomo
{
    public partial class Form1 : Form
    {
        private static readonly Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);   
        public WindowsMediaPlayer sonido;
        public WebClient CLIENTEWEB = new WebClient();
        public XmlDocument documentoGmail =  new XmlDocument();
        public XmlDocument documentoPami = new XmlDocument();

        public ArrayList miArray = new ArrayList();
        public ArrayList miArrayPami = new ArrayList();

        private string username = configuration.AppSettings.Settings["User"].Value;
        private string password = configuration.AppSettings.Settings["Pas"].Value;

        private string userPami = configuration.AppSettings.Settings["UserPami"].Value;
        private string gmail = configuration.AppSettings.Settings["urlMailGmail"].Value;
        


        public string pendientesNew;
        public string pendientesNewPami;

        public string pendientes;
        public string pendientesPami;

        public Form1()
        {
            InitializeComponent();
            ejecutarSonido();

            try
            {

                obtenerTotalCorreos();

                timer1.Interval = 3000;
                timer1.Start();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
 
        
           

        
        }

        private void obtenerTotalCorreos()
        {
            

            CLIENTEWEB.Credentials = new NetworkCredential(username,password);

            string RESPUESTA = CLIENTEWEB.DownloadString(gmail);
            documentoGmail.LoadXml(xml: RESPUESTA);

            string RESPUESTAPAMI = CLIENTEWEB.DownloadString(gmail +"/"+ userPami);
            documentoPami.LoadXml(xml: RESPUESTAPAMI);

            pendientes = documentoGmail.SelectSingleNode("/*[local-name()='feed']/*[local-name()='fullcount']").InnerText;

            pendientesPami = documentoPami.SelectSingleNode("/*[local-name()='feed']/*[local-name()='fullcount']").InnerText;

            mostrarNotificacionDePendientes(pendientes, pendientesPami);

            /*XmlNodeList LISTA = documentoGmail.SelectNodes("/*[local-name()='feed']/*[local-name()='entry']/*[local-name()='id']");

            foreach (XmlNode nodo in LISTA)
            {
                miArray.Add(nodo.ChildNodes.Item(index: 0).InnerText);
            }*/
        }

        


        public void ejecutarSonido()
        {
            sonido = new WindowsMediaPlayer();
            sonido.URL = Application.StartupPath + @"\Mp3\correo.mp3";
            sonido.URL = Application.StartupPath + @"\Mp3\correo.mp3";
            sonido.URL = Application.StartupPath + @"\Mp3\correo.mp3";


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            try
            {
                nuevoCorreoGmail();
                timer1.Interval = 10000;
                timer1.Start();
                nuevoCorreoPami();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nuevoCorreoPami()
        {
            if (ActiveForm != null) ActiveForm.Visible = false;
            CLIENTEWEB.Credentials = new NetworkCredential(username, password);
            string RESPUESTAPAMI = CLIENTEWEB.DownloadString(gmail + "/" + userPami);
            documentoPami.LoadXml(RESPUESTAPAMI);

            pendientesNewPami = documentoPami.SelectSingleNode("/*[local-name()='feed']/*[local-name()='fullcount']").InnerText;

            XmlNodeList LISTAPAMI = documentoPami.SelectNodes("/*[local-name()='feed']/*[local-name()='entry']/*[local-name()='id']");


            foreach (XmlNode nodoPami in LISTAPAMI)
            {
                bool containsPami = miArrayPami.Contains(nodoPami.ChildNodes.Item(0).InnerText);
                if (!containsPami)
                {
                    string tituloPami = (documentoPami.SelectSingleNode("/*[local-name()='feed']/*[local-name()='entry']/*[local-name()='title']").InnerText);
                    string AutorPami = (documentoPami.SelectSingleNode("/*[local-name()='feed']/*[local-name()='entry']/*[local-name()='author']/*[local-name()='email']").InnerText);
                    miArrayPami.Add(nodoPami.ChildNodes.Item(0).InnerText);
                    mostrarNotificacionDeActualizacionPami(AutorPami, tituloPami);

                }

            }
        }

       

        private void nuevoCorreoGmail()
        {
            if (ActiveForm != null) ActiveForm.Visible = false;
            CLIENTEWEB.Credentials = new NetworkCredential(username, password);
            string RESPUESTA = CLIENTEWEB.DownloadString(gmail);
            documentoGmail.LoadXml(RESPUESTA);

            pendientesNew = documentoGmail.SelectSingleNode("/*[local-name()='feed']/*[local-name()='fullcount']").InnerText;

            XmlNodeList LISTA = documentoGmail.SelectNodes("/*[local-name()='feed']/*[local-name()='entry']/*[local-name()='id']");


            foreach (XmlNode nodo in LISTA)
            {
                bool contains = miArray.Contains(nodo.ChildNodes.Item(0).InnerText);
                if (!contains)
                {
                    string titulo = (documentoGmail.SelectSingleNode("/*[local-name()='feed']/*[local-name()='entry']/*[local-name()='title']").InnerText);
                    string Autor = (documentoGmail.SelectSingleNode("/*[local-name()='feed']/*[local-name()='entry']/*[local-name()='author']/*[local-name()='email']").InnerText);
                    miArray.Add(nodo.ChildNodes.Item(0).InnerText);
                    mostrarNotificacionDeActualizacion(Autor, titulo);

                }

            }
        }


        private void mostrarNotificacionDePendientes(string pendientes, string pendientesPami)
        {
            Icon val = Properties.Resources.mail;
            notifyIcon1.Icon = val;
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.Text = "MAILS";
            notifyIcon1.BalloonTipTitle = "Usted tiene Mails sin leer";
            notifyIcon1.BalloonTipText = "Tenés " + pendientes + " mails sin leer de GMAIL. \nTenés " + pendientesPami + " mails sin leer de CORREO PAMI  \nVerifica tu bandeja de Gmail";
            notifyIcon1.ShowBalloonTip(30);
        }

        private void mostrarNotificacionDeActualizacion(string autor, string titulo)
        { 
            string texto = "NUEVO MAIL GMAIL";
            string tituloNotifay = "Usted tiene nuevos mails de GMAIL sin leer";
            string cuerpoNotifay = "Recibio un Mail de:\n" + autor.ToUpper() + "\nAsunto:\n " + titulo.ToUpper() + "\nPor favor verifica tu bandeja de correo GMAIL\nPuede ser un correo muy importante";
            Icon val = Properties.Resources.mail;
            /*notifyIcon1.Icon = val;
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.Text = "NUEVO MAIL GMAIL";
            notifyIcon1.BalloonTipTitle = "Usted tiene nuevos mails de GMAIL sin leer";
            notifyIcon1.BalloonTipText = "Recibio un Mail de:\n" + autor.ToUpper() + "\nAsunto:\n " + titulo.ToUpper() + "\nPor favor verifica tu bandeja de correo GMAIL\nPuede ser un correo muy importante";
            notifyIcon1.ShowBalloonTip(30);*/


            notifayGeneric(val, texto, tituloNotifay, cuerpoNotifay, 30);

        }

        private void notifayGeneric(Icon val, string texto, string tituloNotifay, string cuerpoNotifay, int i)
        {
            notifyIcon1.Icon = val;
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.Text = texto;
            notifyIcon1.BalloonTipTitle = tituloNotifay;
            notifyIcon1.BalloonTipText = cuerpoNotifay;
            notifyIcon1.ShowBalloonTip(i);
        }

        private void mostrarNotificacionDeActualizacionPami(string autorPami, string pamiAsunto)
        {
            string textoPami = "NUEVO MAIL PAMI";
            string tituloNotifayPami = "Usted tiene nuevos mails de PAMI sin leer\n";
            string cuerpoNotifay = "Recibio un Mail de:\n" + autorPami.ToUpper() + "\nAsunto:\n " + pamiAsunto.ToUpper() + "\nPor favor verifica tu bandeja de correo PAMI\nPuede ser un correo muy importante";
            
            
            Icon val = Properties.Resources.pami;
           // notifyIcon1.Icon = val;
            //notifyIcon1.Visible = true;
            //notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            //notifyIcon1.Text = "NUEVO MAIL PAMI";
           // notifyIcon1.BalloonTipTitle = "Usted tiene nuevos mails de PAMI sin leer\n";
           // notifyIcon1.BalloonTipText = "Recibio un Mail de:\n" + autorPami.ToUpper() + "\nAsunto:\n " + pamiAsunto.ToUpper() + "\nPor favor verifica tu bandeja de correo PAMI\nPuede ser un correo muy importante";
           // notifyIcon1.ShowBalloonTip(30);

            notifayGeneric(val, textoPami, tituloNotifayPami, cuerpoNotifay, 30);
        }

       /* private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (notifyIcon1.Text == "NUEVO MAIL GMAIL")
            {
                
            }
        }*/
       
    }
}
