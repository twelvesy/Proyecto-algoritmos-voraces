using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    internal class CColaPrioridad
    //---Define la clase CColaPrioridad, implementada como un min-heap de nodos.
    //   Siempre entrega el nodo con menor frecuencia, usado para construir el árbol de Huffman.
    {
        //---ATRIBUTOS
        private List<CNodo> heap; //---lista interna que representa el heap

        //---CONSTRUCTOR
        //---inicializa el heap vacío
        public CColaPrioridad()
        {
            heap = new List<CNodo>();
        }

        //---MÉTODOS DE ACCESO
        //---retorna la cantidad de nodos en el heap
        public int GetCantidad() { return heap.Count; }

        //---MÉTODOS PRINCIPALES

        //---inserta un nodo en el heap y reordena hacia arriba
        public void Insertar(CNodo nodo)
        {
            heap.Add(nodo);
            SubirNodo(heap.Count - 1);
        }

        //---extrae y retorna el nodo de menor frecuencia (raíz del heap)
        public CNodo Extraer()
        {
            if (heap.Count == 0) throw new Exception("El heap está vacío.");

            CNodo minimo = heap[0];
            int ultimoIndice = heap.Count - 1;
            heap[0] = heap[ultimoIndice];
            heap.RemoveAt(ultimoIndice);
            if (heap.Count > 0) BajarNodo(0);
            return minimo;
        }

        //---retorna el nodo de menor frecuencia sin extraerlo
        public CNodo VerMinimo()
        {
            if (heap.Count == 0) throw new Exception("El heap está vacío.");
            return heap[0];
        }

        //---MÉTODOS AUXILIARES

        //---sube el nodo en el índice dado hasta su posición correcta
        private void SubirNodo(int indice)
        {
            while (indice > 0)
            {
                int padre = (indice - 1) / 2;
                if (heap[indice].GetFrecuencia() < heap[padre].GetFrecuencia())
                {
                    Intercambiar(indice, padre);
                    indice = padre;
                }
                else break;
            }
        }

        //---baja el nodo en el índice dado hasta su posición correcta
        private void BajarNodo(int indice)
        {
            while (true)
            {
                int menor = indice;
                int hijoIzquierdo = 2 * indice + 1;
                int hijoDerecho = 2 * indice + 2;

                if (hijoIzquierdo < heap.Count &&
                    heap[hijoIzquierdo].GetFrecuencia() < heap[menor].GetFrecuencia())
                    menor = hijoIzquierdo;

                if (hijoDerecho < heap.Count &&
                    heap[hijoDerecho].GetFrecuencia() < heap[menor].GetFrecuencia())
                    menor = hijoDerecho;

                if (menor != indice) { Intercambiar(indice, menor); indice = menor; }
                else break;
            }
        }

        //---intercambia dos nodos dentro del heap
        private void Intercambiar(int i, int j)
        {
            CNodo auxiliar = heap[i];
            heap[i] = heap[j];
            heap[j] = auxiliar;
        }
    }
}
