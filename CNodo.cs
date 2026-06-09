using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    public class CNodo
    {
        // ==========================
        // ATRIBUTOS
        // ==========================

        private char? caracter;
        private int frecuencia;
        private CNodo izquierda;
        private CNodo derecha;

        // ==========================
        // CONSTRUCTOR
        // ==========================

        public CNodo(char? caracter, int frecuencia)
        {
            this.caracter = caracter;
            this.frecuencia = frecuencia;
            this.izquierda = null;
            this.derecha = null;
        }

        // ==========================
        // GETTERS Y SETTERS
        // ==========================

        public char? GetCaracter()
        {
            return caracter;
        }

        public void SetCaracter(char? caracter)
        {
            this.caracter = caracter;
        }

        public int GetFrecuencia()
        {
            return frecuencia;
        }

        public void SetFrecuencia(int frecuencia)
        {
            this.frecuencia = frecuencia;
        }

        public CNodo GetIzquierda()
        {
            return izquierda;
        }

        public void SetIzquierda(CNodo izquierda)
        {
            this.izquierda = izquierda;
        }

        public CNodo GetDerecha()
        {
            return derecha;
        }

        public void SetDerecha(CNodo derecha)
        {
            this.derecha = derecha;
        }

        // ==========================
        // MÉTODOS
        // ==========================

        public bool EsHoja()
        {
            return izquierda == null && derecha == null;
        }
    }
}
