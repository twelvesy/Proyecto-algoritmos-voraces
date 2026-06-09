using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    internal class CArbolHuffman
    {
        // ==========================
        // ATRIBUTOS
        // ==========================

        private CNodo raiz;
        private Dictionary<char, string> codigos;

        // ==========================
        // CONSTRUCTOR
        // ==========================

        public CArbolHuffman()
        {
            raiz = null;
            codigos = new Dictionary<char, string>();
        }

        // ==========================
        // GETTERS
        // ==========================

        public CNodo GetRaiz()
        {
            return raiz;
        }

        public Dictionary<char, string> GetCodigos()
        {
            return codigos;
        }

        // ==========================
        // CONSTRUCCIÓN DEL ÁRBOL
        // ==========================

        public void Construir(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                throw new Exception("El texto no puede estar vacío.");
            }

            Dictionary<char, int> frecuencias =
                ContarFrecuencias(texto);

            CColaPrioridad cola = new CColaPrioridad();

            foreach (KeyValuePair<char, int> par in frecuencias)
            {
                CNodo nodo =
                    new CNodo(par.Key, par.Value);

                cola.Insertar(nodo);
            }

            if (cola.GetCantidad() == 1)
            {
                CNodo unico = cola.Extraer();

                CNodo nuevaRaiz =
                    new CNodo(null,
                             unico.GetFrecuencia());

                nuevaRaiz.SetIzquierda(unico);

                raiz = nuevaRaiz;
            }
            else
            {
                while (cola.GetCantidad() > 1)
                {
                    CNodo nodo1 = cola.Extraer();
                    CNodo nodo2 = cola.Extraer();

                    CNodo nuevo =
                        new CNodo(
                            null,
                            nodo1.GetFrecuencia()
                            + nodo2.GetFrecuencia()
                        );

                    nuevo.SetIzquierda(nodo1);
                    nuevo.SetDerecha(nodo2);

                    cola.Insertar(nuevo);
                }

                raiz = cola.Extraer();
            }

            codigos.Clear();

            GenerarCodigos(raiz, "");
        }

        // ==========================
        // CONTAR FRECUENCIAS
        // ==========================

        private Dictionary<char, int>
            ContarFrecuencias(string texto)
        {
            Dictionary<char, int> frecuencias =
                new Dictionary<char, int>();

            foreach (char c in texto)
            {
                if (frecuencias.ContainsKey(c))
                {
                    frecuencias[c]++;
                }
                else
                {
                    frecuencias[c] = 1;
                }
            }

            return frecuencias;
        }

        // ==========================
        // GENERAR CÓDIGOS
        // ==========================

        private void GenerarCodigos(
            CNodo nodo,
            string codigoActual)
        {
            if (nodo == null)
            {
                return;
            }

            if (nodo.EsHoja())
            {
                string codigo;

                if (codigoActual == "")
                {
                    codigo = "0";
                }
                else
                {
                    codigo = codigoActual;
                }

                codigos[
                    nodo.GetCaracter().Value
                ] = codigo;

                return;
            }

            GenerarCodigos(
                nodo.GetIzquierda(),
                codigoActual + "0"
            );

            GenerarCodigos(
                nodo.GetDerecha(),
                codigoActual + "1"
            );
        }

        // ==========================
        // SERIALIZAR ÁRBOL
        // ==========================

        public string SerializarArbol()
        {
            StringBuilder sb =
                new StringBuilder();

            SerializarNodo(raiz, sb);

            return sb.ToString();
        }

        private void SerializarNodo(
            CNodo nodo,
            StringBuilder sb)
        {
            if (nodo == null)
            {
                return;
            }

            if (nodo.EsHoja())
            {
                sb.Append(
                    "L"
                    + (int)nodo.GetCaracter().Value
                    + "|"
                );
            }
            else
            {
                sb.Append("I");

                SerializarNodo(
                    nodo.GetIzquierda(),
                    sb
                );

                SerializarNodo(
                    nodo.GetDerecha(),
                    sb
                );
            }
        }

        // ==========================
        // DESERIALIZAR ÁRBOL
        // ==========================

        public static CNodo DeserializarArbol(
            string datos)
        {
            int indice = 0;

            return DeserializarNodo(
                datos,
                ref indice
            );
        }

        private static CNodo DeserializarNodo(
            string datos,
            ref int indice)
        {
            if (indice >= datos.Length)
            {
                return null;
            }

            char tipo = datos[indice];

            indice++;

            if (tipo == 'L')
            {
                int inicio = indice;

                while (
                    indice < datos.Length &&
                    datos[indice] != '|'
                )
                {
                    indice++;
                }

                int codigoUnicode =
                    int.Parse(
                        datos.Substring(
                            inicio,
                            indice - inicio
                        )
                    );

                indice++;

                return new CNodo(
                    (char)codigoUnicode,
                    0
                );
            }
            else
            {
                CNodo nodo =
                    new CNodo(null, 0);

                nodo.SetIzquierda(
                    DeserializarNodo(
                        datos,
                        ref indice
                    )
                );

                nodo.SetDerecha(
                    DeserializarNodo(
                        datos,
                        ref indice
                    )
                );

                return nodo;
            }
        }
    }
}
