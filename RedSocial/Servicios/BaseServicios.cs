using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RedSocial.Utilidades;

namespace RedSocial.Servicios
{
    public class BaseServicios<T> where T:class 
    {
        public String Url { get; set; }


        public BaseServicios(String url)
        {
            Url = url;
        }


        public List<T> Get()
        {
            List<T> lista;
            var cl = WebRequest.Create(Url);
            //cl.Credentials = new NetworkCredential("luis.gil@tajamar365.com", "123456");


            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializador.Deserializar<List<T>>(resultado);


                }
            }

            return lista;





        }

        public T Get(Dictionary<String, Object> parametros)
        {
            T dato;
            var par = "?";

            foreach (var key in parametros.Keys)
            {
                if (par != "?")
                    par += "&";
                par += key + "=" + parametros[key];
            }


            var cl = WebRequest.Create(Url + par);


            cl.Method = "GET";
            try
            {
                var res = cl.GetResponse();
                using (var stream = res.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var resultado = reader.ReadToEnd();
                        dato = Serializador.Deserializar<T>(resultado);


                    }
                }

                return dato;
            }
            catch (Exception e)
            {
                return null;
            }

        }


        public List<T> GetList(Dictionary<String, Object> parametros)
        {
            List<T> dato;
            var par = "?";

            foreach (var key in parametros.Keys)
            {
                if (par != "?")
                    par += "&";
                par += key + "=" + parametros[key];
            }


            var cl = WebRequest.Create(Url + par);


            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    dato = Serializador.Deserializar<List<T>>(resultado);


                }
            }

            return dato;

        }

        public async Task Add(T modelo)
        {
            var serializado = Serializador.Serializar(modelo);

            using (var handler = new HttpClientHandler())
            {

                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(serializado);
                    contenido.Headers.ContentType =
                        new MediaTypeHeaderValue("application/json");

                    await client.PostAsync(new Uri(Url),
                        contenido);
                }
            }
        }

        public async Task Delete(int id)
        {


            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {


                    await client.DeleteAsync(new Uri(Url + "/" + id));
                }
            }
        }

        public async Task Update(int id, T modelo)
        {
            var serializado = Serializador.Serializar(modelo);

            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(serializado);
                    contenido.Headers.ContentType =
                        new MediaTypeHeaderValue("application/json");

                    await client.PutAsync(new Uri(Url),
                        contenido);
                }
            }
        }


    }
}