using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace GeneridValidador
{
    public class VerificadorSisten
    {
        public bool integridadSistemas(string url)
        {
            bool estado;

            try
            {
                string uri = url; 
                var request = WebRequest.Create(uri);

                using (var response = request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        estado = true;

                    }
                }
            }
            catch (WebException ex)
            {
                estado = false;
                if (ex.Status == WebExceptionStatus.ProtocolError &&
                    ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Si no se encuentra la pagina, el famoso Error 404
                        //MessageBox.Show("No se encuentra la pagina");
                        //return estado;
                        
                        throw new Exception("No se encuentra la pagina \n" + ex.Message);
                        
                        

                    }
                    else
                    {
                        if(resp.StatusCode ==  HttpStatusCode.GatewayTimeout)//StatusCode == HttpStatusCode.RequestTimeout )
                        {
                            throw new Exception("Demora en cargar, detalle:\n" + ex.Message + " \n\n" + resp.StatusDescription); 
                        }
                        else
                        {
                            throw new Exception("Error inesperado, detalle:\n" + ex.Message + " \n\n" + resp.StatusDescription);
                        }
                        
                        //MessageBox.Show("Error inesperado, detalle:\n " + resp.StatusDescription);// Si fue otro error...
                    }
                }
                else
                {
                    // Si fue otro error...
                }

            }
            return estado;
        }
    }
}
