using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    internal class CEstadisticasCompresion
    {
        // ==========================
        // ATRIBUTOS
        // ==========================

        private long tamanoOriginalBytes;
        private long tamanoComprimidoBytes;
        private int totalCaracteres;
        private int totalBitsComprimidos;
        private Dictionary<char, string> tablaCodigos;

        // ==========================
        // CONSTRUCTOR
        // ==========================

        public CEstadisticasCompresion()
        {
            tablaCodigos = new Dictionary<char, string>();
        }

        // ==========================
        // GETTERS Y SETTERS
        // ==========================

        public long GetTamanoOriginalBytes()
        {
            return tamanoOriginalBytes;
        }

        public void SetTamanoOriginalBytes(long valor)
        {
            tamanoOriginalBytes = valor;
        }

        public long GetTamanoComprimidoBytes()
        {
            return tamanoComprimidoBytes;
        }

        public void SetTamanoComprimidoBytes(long valor)
        {
            tamanoComprimidoBytes = valor;
        }

        public int GetTotalCaracteres()
        {
            return totalCaracteres;
        }

        public void SetTotalCaracteres(int valor)
        {
            totalCaracteres = valor;
        }

        public int GetTotalBitsComprimidos()
        {
            return totalBitsComprimidos;
        }

        public void SetTotalBitsComprimidos(int valor)
        {
            totalBitsComprimidos = valor;
        }

        public Dictionary<char, string> GetTablaCodigos()
        {
            return tablaCodigos;
        }

        public void SetTablaCodigos(Dictionary<char, string> tabla)
        {
            tablaCodigos = tabla;
        }

        // ==========================
        // MÉTODOS
        // ==========================

        public double GetPorcentajeReduccion()
        {
            if (tamanoOriginalBytes == 0)
            {
                return 0;
            }

            return
                (1.0 -
                (double)tamanoComprimidoBytes /
                tamanoOriginalBytes)
                * 100.0;
        }
    }
}
