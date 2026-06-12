using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    internal class CDesComPresor
    //---Define la clase CDesComPresor, responsable de comprimir y descomprimir archivos de texto.
    //   El archivo comprimido es un .txt con el árbol serializado en la primera línea y la cadena
    //   de ceros y unos en texto corrido en la segunda línea.
    //   Las estadísticas simulan el tamaño real que tendría si se empaquetara en binario (.bin).
    {
        //---ATRIBUTOS
        private CArbolHuffman arbol;    //---árbol de Huffman usado en la compresión
        private string textoCodificado; //---cadena de bits generada en la última compresión

        //---CONSTRUCTOR
        //---inicializa el compresor con un árbol vacío
        public CDesComPresor()
        {
            arbol = new CArbolHuffman();
            textoCodificado = "";
        }

        //---COMPRESIÓN
        //---construye el árbol, codifica el texto y guarda árbol + bits en un .txt
        public CEstadisticasCompresion ComprimirArchivo(string rutaEntrada, string rutaSalida)
        {
            if (!File.Exists(rutaEntrada))
                throw new FileNotFoundException("No se encontró el archivo: " + rutaEntrada);

            string textoOriginal = File.ReadAllText(rutaEntrada, Encoding.UTF8);

            if (string.IsNullOrEmpty(textoOriginal))
                throw new Exception("El archivo está vacío.");

            arbol.Construir(textoOriginal);

            textoCodificado = CodificarTexto(textoOriginal, arbol.GetCodigos());

            //---línea 1: árbol serializado | línea 2: bits en texto corrido
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(arbol.SerializarArbol());
            sb.Append(textoCodificado);
            File.WriteAllText(rutaSalida, sb.ToString(), Encoding.UTF8);

            //---tamaño simulado: cuántos bytes ocuparía si los bits se empaquetaran en binario
            long tamanoSimuladoBytes = (textoCodificado.Length + 7) / 8;

            CEstadisticasCompresion estadisticas = new CEstadisticasCompresion();
            estadisticas.SetTamanoOriginalBytes(new FileInfo(rutaEntrada).Length);
            estadisticas.SetTamanoComprimidoBytes(tamanoSimuladoBytes);
            estadisticas.SetTotalCaracteres(textoOriginal.Length);
            estadisticas.SetTotalBitsComprimidos(textoCodificado.Length);
            estadisticas.SetTablaCodigos(new Dictionary<char, string>(arbol.GetCodigos()));
            return estadisticas;
        }

        //---DESCOMPRESIÓN
        //---lee la primera línea como árbol y la segunda como cadena de bits, luego decodifica
        public void DescomprimirArchivo(string rutaEntrada, string rutaSalida)
        {
            if (!File.Exists(rutaEntrada))
                throw new FileNotFoundException("No se encontró el archivo: " + rutaEntrada);

            string[] lineas = File.ReadAllLines(rutaEntrada, Encoding.UTF8);

            if (lineas.Length < 2)
                throw new Exception("El archivo comprimido no tiene el formato esperado.");

            string arbolSerializado = lineas[0];
            string bits = lineas[1];

            CNodo raiz = CArbolHuffman.DeserializarArbol(arbolSerializado);
            if (raiz == null) throw new Exception("No se pudo reconstruir el árbol.");

            string textoRecuperado = DecodificarBits(bits, raiz);

            File.WriteAllText(rutaSalida, textoRecuperado, Encoding.UTF8);
        }

        //---CODIFICAR TEXTO
        //---reemplaza cada carácter del texto por su código binario de Huffman
        private string CodificarTexto(string texto, Dictionary<char, string> codigos)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in texto) sb.Append(codigos[c]);
            return sb.ToString();
        }

        //---DECODIFICAR
        //---recorre los bits usando el árbol para recuperar el texto original carácter a carácter
        private string DecodificarBits(string bits, CNodo raiz)
        {
            StringBuilder texto = new StringBuilder();
            CNodo nodoActual = raiz;

            foreach (char bit in bits)
            {
                nodoActual = (bit == '0') ? nodoActual.GetIzquierda() : nodoActual.GetDerecha();

                if (nodoActual.EsHoja())
                {
                    texto.Append(nodoActual.GetCaracter().Value);
                    nodoActual = raiz;
                }
            }

            return texto.ToString();
        }
    }
}