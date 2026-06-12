using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    public class CNodo
    //---Define la clase CNodo, unidad básica del árbol de Huffman.
    //   Cada nodo puede ser hoja (con carácter) o nodo interno (sin carácter, solo frecuencia).
    {
        //---ATRIBUTOS
        private char? caracter;   //---carácter almacenado en el nodo (null si es nodo interno)
        private int frecuencia;   //---frecuencia de aparición del carácter en el texto
        private CNodo izquierda;  //---referencia al hijo izquierdo del árbol (bit 0)
        private CNodo derecha;    //---referencia al hijo derecho del árbol (bit 1)

        //---CONSTRUCTOR
        //---inicializa el nodo con un carácter y su frecuencia
        public CNodo(char? caracter, int frecuencia)
        {
            this.caracter = caracter;   //---asigna el carácter
            this.frecuencia = frecuencia; //---asigna la frecuencia
            this.izquierda = null;       //---hijo izquierdo vacío por defecto
            this.derecha = null;       //---hijo derecho vacío por defecto
        }

        //---GETTERS Y SETTERS
        //---permiten acceder o modificar los atributos del nodo

        public char? GetCaracter() { return caracter; }
        public void SetCaracter(char? caracter) { this.caracter = caracter; }

        public int GetFrecuencia() { return frecuencia; }
        public void SetFrecuencia(int frecuencia) { this.frecuencia = frecuencia; }

        public CNodo GetIzquierda() { return izquierda; }
        public void SetIzquierda(CNodo izquierda) { this.izquierda = izquierda; }

        public CNodo GetDerecha() { return derecha; }
        public void SetDerecha(CNodo derecha) { this.derecha = derecha; }

        //---MÉTODOS
        //---retorna true si el nodo es hoja (sin hijos)
        public bool EsHoja()
        {
            return izquierda == null && derecha == null;
        }
    }
}
