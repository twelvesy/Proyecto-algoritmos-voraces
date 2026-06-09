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
            try
            {
                Console.WriteLine("=================================");
                Console.WriteLine(" COMPRESOR HUFFMAN");
                Console.WriteLine("=================================\n");

                string archivoOriginal =
                    "Bioinformatica.txt";

                string archivoComprimido =
                    "Bioinformatica.bin";

                string archivoRecuperado =
                    "Bioinformatica_recuperado.txt";

                CDesComPresor compresor =
                    new CDesComPresor();

                Console.WriteLine(
                    "Comprimiendo archivo..."
                );

                CEstadisticasCompresion estadisticas =
                    compresor.ComprimirArchivo(
                        archivoOriginal,
                        archivoComprimido
                    );

                Console.WriteLine(
                    "\nCOMPRESIÓN COMPLETADA"
                );

                Console.WriteLine(
                    "Tamaño original: "
                    + estadisticas.GetTamanoOriginalBytes()
                    + " bytes"
                );

                Console.WriteLine(
                    "Tamaño comprimido: "
                    + estadisticas.GetTamanoComprimidoBytes()
                    + " bytes"
                );

                Console.WriteLine(
                    "Caracteres procesados: "
                    + estadisticas.GetTotalCaracteres()
                );

                Console.WriteLine(
                    "Bits comprimidos: "
                    + estadisticas.GetTotalBitsComprimidos()
                );

                Console.WriteLine(
                    "Reducción: "
                    + estadisticas
                        .GetPorcentajeReduccion()
                        .ToString("F2")
                    + "%"
                );

                Console.WriteLine(
                    "\nTABLA DE CÓDIGOS HUFFMAN"
                );

                foreach (
                    KeyValuePair<char, string> par
                    in estadisticas.GetTablaCodigos()
                )
                {
                    Console.WriteLine(
                        "'" + par.Key + "' -> "
                        + par.Value
                    );
                }

                Console.WriteLine(
                    "\nDescomprimiendo archivo..."
                );

                compresor.DescomprimirArchivo(
                    archivoComprimido,
                    archivoRecuperado
                );

                Console.WriteLine(
                    "DESCOMPRESIÓN COMPLETADA"
                );

                Console.WriteLine(
                    "\nArchivo original : "
                    + archivoOriginal
                );

                Console.WriteLine(
                    "Archivo comprimido : "
                    + archivoComprimido
                );

                Console.WriteLine(
                    "Archivo recuperado : "
                    + archivoRecuperado
                );

                Console.WriteLine(
                    "\nProceso finalizado correctamente."
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "\nERROR:"
                );

                Console.WriteLine(
                    ex.Message
                );
            }

            Console.WriteLine(
                "\nPresione una tecla para salir..."
            );

            Console.ReadKey();
        }
    }
}
