using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    public class CNodo
    //---Define la clase CNodo, unidad bàsica del arbol de Huffman.
    //   Cada nodo puede ser hoja (con carater) o nodo interno (sin caracter, solo frecuencia)
    {
        //---ATRIBUTOS

        private char? caracter; //---caracter almacenado en el nodo (null si es nodo interno)
        private int frecuencia; //---frecuencia de aparicion del caracter en el texto
        private CNodo izquierda; //--Referencia al hijo izquierdo del arbol (bit 0)
        private CNodo derecha; //----Referencia al hijo derecho del arbol (bit 1)

        //---CONSTRUCTOR
        //----inicializa el nodo con un caracter y su frecuencia
        public CNodo(char? caracter, int frecuencia)
        {
            this.caracter = caracter; //---asigna el caracter
            this.frecuencia = frecuencia; //---asigna la frecuencia
            this.izquierda = null; //---hijo izquierdo vacio por defecto
            this.derecha = null; //---hijo derecho vacio por defecto
        }

        //---GETTERS Y SETTERS
        //---nos permite acceder o recuperar los valores de los atributos del nodo
        public char? GetCaracter()
        {
            return caracter; //---retorna el caracter
        }

        public void SetCaracter(char? caracter)
        {
            this.caracter = caracter; //---acceder o modificar al caracter
        }

        public int GetFrecuencia()
        {
            return frecuencia; //---retorna la frecuencia
        }

        public void SetFrecuencia(int frecuencia)
        {
            this.frecuencia = frecuencia; //---accede o modifica la frecuencia
        }

        public CNodo GetIzquierda()
        {
            return izquierda; //---retorna el hijo izquierdo
        }

        public void SetIzquierda(CNodo izquierda)
        {
            this.izquierda = izquierda; //---accedo o modifica al hijo izquierdo
        }

        public CNodo GetDerecha()
        {
            return derecha; //--retorna el hijo derecho
        }

        public void SetDerecha(CNodo derecha)
        {
            this.derecha = derecha; //---accede o modifica al hijo derecho
        }

        //---MÉTODOS
        //---metodo para validar si el nodo es hoja
        public bool EsHoja()
        {
            return izquierda == null && derecha == null;
        }
    }
}
