using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    internal class CEstadisticasCompresion
    //---Define la clase CEstadisticasCompresion, que almacena los resultados del proceso de compresión.
    //   Guarda tamaños, totales y la tabla de códigos generada por el árbol de Huffman.
    {
        //---ATRIBUTOS
        private long tamanoOriginalBytes;              //---tamaño del archivo original en bytes
        private long tamanoComprimidoBytes;            //---tamaño del archivo comprimido en bytes
        private int totalCaracteres;                   //---total de caracteres en el texto original
        private int totalBitsComprimidos;              //---total de bits en la cadena comprimida
        private Dictionary<char, string> tablaCodigos; //---tabla de códigos Huffman por carácter

        //---CONSTRUCTOR
        //---inicializa la tabla de códigos vacía
        public CEstadisticasCompresion()
        {
            tablaCodigos = new Dictionary<char, string>();
        }

        //---GETTERS Y SETTERS
        //---permiten acceder o modificar cada estadística registrada

        public long GetTamanoOriginalBytes() { return tamanoOriginalBytes; }
        public void SetTamanoOriginalBytes(long valor) { tamanoOriginalBytes = valor; }

        public long GetTamanoComprimidoBytes() { return tamanoComprimidoBytes; }
        public void SetTamanoComprimidoBytes(long valor) { tamanoComprimidoBytes = valor; }

        public int GetTotalCaracteres() { return totalCaracteres; }
        public void SetTotalCaracteres(int valor) { totalCaracteres = valor; }

        public int GetTotalBitsComprimidos() { return totalBitsComprimidos; }
        public void SetTotalBitsComprimidos(int valor) { totalBitsComprimidos = valor; }

        public Dictionary<char, string> GetTablaCodigos() { return tablaCodigos; }
        public void SetTablaCodigos(Dictionary<char, string> tabla) { tablaCodigos = tabla; }

        //---MÉTODOS
        //---calcula el porcentaje de reducción logrado por la compresión
        public double GetPorcentajeReduccion()
        {
            if (tamanoOriginalBytes == 0) return 0;
            return (1.0 - (double)tamanoComprimidoBytes / tamanoOriginalBytes) * 100.0;
        }
    }
}
