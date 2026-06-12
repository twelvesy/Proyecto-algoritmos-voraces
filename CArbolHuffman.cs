using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    internal class CArbolHuffman
    //---Define la clase CArbolHuffman, que construye y gestiona el árbol binario de Huffman.
    //   Genera los códigos de compresión para cada carácter, incluyendo letras con tilde.
    {
        //---ATRIBUTOS
        private CNodo raiz;                        //---raíz del árbol de Huffman
        private Dictionary<char, string> codigos;  //---tabla de códigos binarios por carácter

        //---CONSTRUCTOR
        //---inicializa el árbol vacío y la tabla de códigos
        public CArbolHuffman()
        {
            raiz = null;
            codigos = new Dictionary<char, string>();
        }

        //---GETTERS
        public CNodo GetRaiz() { return raiz; }
        public Dictionary<char, string> GetCodigos() { return codigos; }

        //---CONSTRUCCIÓN DEL ÁRBOL
        //---construye el árbol de Huffman a partir de las frecuencias del texto
        public void Construir(string texto)
        {
            if (string.IsNullOrEmpty(texto)) throw new Exception("El texto no puede estar vacío.");

            Dictionary<char, int> frecuencias = ContarFrecuencias(texto);
            CColaPrioridad cola = new CColaPrioridad();

            foreach (KeyValuePair<char, int> par in frecuencias)
                cola.Insertar(new CNodo(par.Key, par.Value));

            if (cola.GetCantidad() == 1)
            {
                //---caso especial: un solo carácter distinto en el texto
                CNodo unico = cola.Extraer();
                CNodo nuevaRaiz = new CNodo(null, unico.GetFrecuencia());
                nuevaRaiz.SetIzquierda(unico);
                raiz = nuevaRaiz;
            }
            else
            {
                //---combina los dos nodos de menor frecuencia hasta formar la raíz
                while (cola.GetCantidad() > 1)
                {
                    CNodo nodo1 = cola.Extraer();
                    CNodo nodo2 = cola.Extraer();
                    CNodo nuevo = new CNodo(null, nodo1.GetFrecuencia() + nodo2.GetFrecuencia());
                    nuevo.SetIzquierda(nodo1);
                    nuevo.SetDerecha(nodo2);
                    cola.Insertar(nuevo);
                }
                raiz = cola.Extraer();
            }

            codigos.Clear();
            GenerarCodigos(raiz, "");
        }

        //---CONTAR FRECUENCIAS
        //---cuenta cuántas veces aparece cada carácter en el texto
        private Dictionary<char, int> ContarFrecuencias(string texto)
        {
            Dictionary<char, int> frecuencias = new Dictionary<char, int>();
            foreach (char c in texto)
            {
                if (frecuencias.ContainsKey(c)) frecuencias[c]++;
                else frecuencias[c] = 1;
            }
            return frecuencias;
        }

        //---GENERAR CÓDIGOS
        //---recorre el árbol en profundidad y asigna el código binario a cada hoja
        private void GenerarCodigos(CNodo nodo, string codigoActual)
        {
            if (nodo == null) return;

            if (nodo.EsHoja())
            {
                //---si el árbol tiene un solo nodo, se asigna "0" por defecto
                codigos[nodo.GetCaracter().Value] = (codigoActual == "") ? "0" : codigoActual;
                return;
            }

            GenerarCodigos(nodo.GetIzquierda(), codigoActual + "0");
            GenerarCodigos(nodo.GetDerecha(), codigoActual + "1");
        }

        //---SERIALIZAR ÁRBOL
        //---convierte el árbol a una cadena de texto para guardarlo en el archivo comprimido.
        //   Las hojas se guardan como "L<codepoint>|" usando el valor Unicode del carácter,
        //   lo que garantiza soporte correcto para tildes y caracteres especiales.
        public string SerializarArbol()
        {
            StringBuilder sb = new StringBuilder();
            SerializarNodo(raiz, sb);
            return sb.ToString();
        }

        private void SerializarNodo(CNodo nodo, StringBuilder sb)
        {
            if (nodo == null) return;

            if (nodo.EsHoja())
                sb.Append("L" + (int)nodo.GetCaracter().Value + "|"); //---guarda codepoint Unicode
            else
            {
                sb.Append("I");
                SerializarNodo(nodo.GetIzquierda(), sb);
                SerializarNodo(nodo.GetDerecha(), sb);
            }
        }

        //---DESERIALIZAR ÁRBOL
        //---reconstruye el árbol desde la cadena serializada.
        //   Lee el codepoint Unicode y lo convierte a char.
        public static CNodo DeserializarArbol(string datos)
        {
            int indice = 0;
            return DeserializarNodo(datos, ref indice);
        }

        private static CNodo DeserializarNodo(string datos, ref int indice)
        {
            if (indice >= datos.Length) return null;

            char tipo = datos[indice++];

            if (tipo == 'L')
            {
                //---lee el número Unicode hasta encontrar el separador '|'
                int inicio = indice;
                while (indice < datos.Length && datos[indice] != '|') indice++;

                int codigoUnicode = int.Parse(datos.Substring(inicio, indice - inicio));
                indice++; //---salta el '|'

                return new CNodo((char)codigoUnicode, 0);
            }
            else
            {
                //---nodo interno: reconstruye recursivamente ambos hijos
                CNodo nodo = new CNodo(null, 0);
                nodo.SetIzquierda(DeserializarNodo(datos, ref indice));
                nodo.SetDerecha(DeserializarNodo(datos, ref indice));
                return nodo;
            }
        }
    }
}
