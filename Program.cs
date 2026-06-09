using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //---creando variables para almacenar los nombres de los archivos
            string archivoOriginal = "Bioinformatica.txt";
            string archivoComprimido = "Bioinformatica.bin";
            string archivoRecuperado = "Bioinformatica_recuperado.txt";

            //---crear objeto compresor
            CDesComPresor compresor = new CDesComPresor();

            //---variable para almacenar las estadísticas de compresión
            CEstadisticasCompresion estadisticas = null;

            //---variable para controlar el menú
            int opcion;

            do
            {
                //---imprimiendo título principal
                Console.WriteLine("=================================");
                Console.WriteLine("      COMPRESOR HUFFMAN");
                Console.WriteLine("=================================");

                //---mostrando opciones del menú
                Console.WriteLine();
                Console.WriteLine("1. Comprimir archivo");
                Console.WriteLine("2. Descomprimir archivo");
                Console.WriteLine("3. Mostrar estadísticas");
                Console.WriteLine("4. Mostrar tabla Huffman");
                Console.WriteLine("0. Salir");
                Console.WriteLine();

                //---solicitar opción al usuario
                Console.Write("Seleccione una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                try
                {
                    switch (opcion)
                    {
                        case 1:

                            //---comprimir archivo y guardar estadísticas
                            estadisticas = compresor.ComprimirArchivo(archivoOriginal, archivoComprimido);

                            //---mensaje de éxito
                            Console.WriteLine("Archivo comprimido correctamente.");

                            break;

                        case 2:

                            //---descomprimir archivo
                            compresor.DescomprimirArchivo(archivoComprimido, archivoRecuperado);

                            //---mensaje de éxito
                            Console.WriteLine("Archivo descomprimido correctamente.");

                            break;

                        case 3:

                            //---verificar si existe una compresión previa
                            if (estadisticas == null)
                            {
                                Console.WriteLine("Primero debe comprimir un archivo.");
                                break;
                            }

                            //---mostrar estadísticas
                            Console.WriteLine("Tamaño original: " + estadisticas.GetTamanoOriginalBytes() + " bytes");
                            Console.WriteLine("Tamaño comprimido: " + estadisticas.GetTamanoComprimidoBytes() + " bytes");
                            Console.WriteLine("Caracteres procesados: " + estadisticas.GetTotalCaracteres());
                            Console.WriteLine("Bits comprimidos: " + estadisticas.GetTotalBitsComprimidos());
                            Console.WriteLine("Reducción: " + estadisticas.GetPorcentajeReduccion().ToString("F2") + "%");

                            break;

                        case 4:

                            //---verificar si existe una compresión previa
                            if (estadisticas == null)
                            {
                                Console.WriteLine("Primero debe comprimir un archivo.");
                                break;
                            }

                            //---imprimir tabla Huffman
                            Console.WriteLine("TABLA DE CÓDIGOS HUFFMAN");
                            Console.WriteLine();

                            foreach (KeyValuePair<char, string> par in estadisticas.GetTablaCodigos())
                            {
                                Console.WriteLine("'" + par.Key + "' -> " + par.Value);
                            }

                            break;

                        case 0:

                            //---mensaje de salida
                            Console.WriteLine("Fin del programa.");

                            break;

                        default:

                            //---opción incorrecta
                            Console.WriteLine("Opción inválida.");

                            break;
                    }
                }
                catch (Exception ex)
                {
                    //---mostrar mensaje de error
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            } while (opcion != 0);
        }
    }
}   
