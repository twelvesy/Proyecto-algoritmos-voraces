using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_algoritmos_voraces
{
    internal class CDesComPresor
    {
        // ==========================
        // ATRIBUTOS
        // ==========================

        private CArbolHuffman arbol;

        // ==========================
        // CONSTRUCTOR
        // ==========================

        public CDesComPresor()
        {
            arbol = new CArbolHuffman();
        }

        // ==========================
        // COMPRESIÓN
        // ==========================

        public CEstadisticasCompresion ComprimirArchivo(
            string rutaEntrada,
            string rutaSalida)
        {
            if (!File.Exists(rutaEntrada))
            {
                throw new FileNotFoundException(
                    "No se encontró el archivo: "
                    + rutaEntrada
                );
            }

            string textoOriginal =
                File.ReadAllText(
                    rutaEntrada,
                    Encoding.UTF8
                );

            if (string.IsNullOrEmpty(textoOriginal))
            {
                throw new Exception(
                    "El archivo está vacío."
                );
            }

            arbol.Construir(textoOriginal);

            string textoCodificado =
                CodificarTexto(
                    textoOriginal,
                    arbol.GetCodigos()
                );

            byte[] bytesComprimidos;
            int bitsRelleno;

            ConvertirBitsABytes(
                textoCodificado,
                out bytesComprimidos,
                out bitsRelleno
            );

            string arbolSerializado =
                arbol.SerializarArbol();

            EscribirArchivoComprimido(
                rutaSalida,
                arbolSerializado,
                bytesComprimidos,
                bitsRelleno
            );

            long tamanoOriginal =
                new FileInfo(rutaEntrada).Length;

            long tamanoComprimido =
                new FileInfo(rutaSalida).Length;

            CEstadisticasCompresion estadisticas =
                new CEstadisticasCompresion();

            estadisticas.SetTamanoOriginalBytes(
                tamanoOriginal
            );

            estadisticas.SetTamanoComprimidoBytes(
                tamanoComprimido
            );

            estadisticas.SetTotalCaracteres(
                textoOriginal.Length
            );

            estadisticas.SetTotalBitsComprimidos(
                textoCodificado.Length
            );

            estadisticas.SetTablaCodigos(
                new Dictionary<char, string>(
                    arbol.GetCodigos()
                )
            );

            return estadisticas;
        }

        // ==========================
        // DESCOMPRESIÓN
        // ==========================

        public void DescomprimirArchivo(
            string rutaEntrada,
            string rutaSalida)
        {
            if (!File.Exists(rutaEntrada))
            {
                throw new FileNotFoundException(
                    "No se encontró el archivo: "
                    + rutaEntrada
                );
            }

            string arbolSerializado;
            byte[] bytesComprimidos;
            int bitsRelleno;

            LeerArchivoComprimido(
                rutaEntrada,
                out arbolSerializado,
                out bytesComprimidos,
                out bitsRelleno
            );

            CNodo raiz =
                CArbolHuffman.DeserializarArbol(
                    arbolSerializado
                );

            if (raiz == null)
            {
                throw new Exception(
                    "No se pudo reconstruir el árbol."
                );
            }

            string bits =
                ConvertirBytesABits(
                    bytesComprimidos,
                    bitsRelleno
                );

            string textoRecuperado =
                DecodificarBits(
                    bits,
                    raiz
                );

            File.WriteAllText(
                rutaSalida,
                textoRecuperado,
                Encoding.UTF8
            );
        }

        // ==========================
        // CODIFICAR TEXTO
        // ==========================

        private string CodificarTexto(
            string texto,
            Dictionary<char, string> codigos)
        {
            StringBuilder sb =
                new StringBuilder();

            foreach (char c in texto)
            {
                sb.Append(codigos[c]);
            }

            return sb.ToString();
        }

        // ==========================
        // BITS -> BYTES
        // ==========================

        private void ConvertirBitsABytes(
            string bits,
            out byte[] bytes,
            out int bitsRelleno)
        {
            bitsRelleno =
                (8 - (bits.Length % 8)) % 8;

            string bitsRellenados =
                bits +
                new string(
                    '0',
                    bitsRelleno
                );

            int totalBytes =
                bitsRellenados.Length / 8;

            bytes = new byte[totalBytes];

            for (int i = 0; i < totalBytes; i++)
            {
                string grupoBits =
                    bitsRellenados.Substring(
                        i * 8,
                        8
                    );

                bytes[i] =
                    Convert.ToByte(
                        grupoBits,
                        2
                    );
            }
        }

        // ==========================
        // BYTES -> BITS
        // ==========================

        private string ConvertirBytesABits(
            byte[] bytes,
            int bitsRelleno)
        {
            StringBuilder sb =
                new StringBuilder();

            foreach (byte b in bytes)
            {
                sb.Append(
                    Convert
                    .ToString(b, 2)
                    .PadLeft(8, '0')
                );
            }

            string bits =
                sb.ToString();

            if (
                bitsRelleno > 0 &&
                bits.Length >= bitsRelleno
            )
            {
                bits =
                    bits.Substring(
                        0,
                        bits.Length -
                        bitsRelleno
                    );
            }

            return bits;
        }

        // ==========================
        // DECODIFICAR
        // ==========================

        private string DecodificarBits(
            string bits,
            CNodo raiz)
        {
            StringBuilder texto =
                new StringBuilder();

            CNodo nodoActual = raiz;

            foreach (char bit in bits)
            {
                if (bit == '0')
                {
                    nodoActual =
                        nodoActual.GetIzquierda();
                }
                else
                {
                    nodoActual =
                        nodoActual.GetDerecha();
                }

                if (nodoActual.EsHoja())
                {
                    texto.Append(
                        nodoActual
                        .GetCaracter()
                        .Value
                    );

                    nodoActual = raiz;
                }
            }

            return texto.ToString();
        }

        // ==========================
        // GUARDAR ARCHIVO BINARIO
        // ==========================

        private void EscribirArchivoComprimido(
            string ruta,
            string arbolSerializado,
            byte[] bytesComprimidos,
            int bitsRelleno)
        {
            using (
                BinaryWriter writer =
                new BinaryWriter(
                    File.Open(
                        ruta,
                        FileMode.Create
                    )
                )
            )
            {
                byte[] bytesArbol =
                    Encoding.UTF8.GetBytes(
                        arbolSerializado
                    );

                writer.Write(
                    bytesArbol.Length
                );

                writer.Write(
                    bytesArbol
                );

                writer.Write(
                    (byte)bitsRelleno
                );

                writer.Write(
                    bytesComprimidos
                );
            }
        }

        // ==========================
        // LEER ARCHIVO BINARIO
        // ==========================

        private void LeerArchivoComprimido(
            string ruta,
            out string arbolSerializado,
            out byte[] bytesComprimidos,
            out int bitsRelleno)
        {
            using (
                BinaryReader reader =
                new BinaryReader(
                    File.Open(
                        ruta,
                        FileMode.Open
                    )
                )
            )
            {
                int longitudArbol =
                    reader.ReadInt32();

                byte[] bytesArbol =
                    reader.ReadBytes(
                        longitudArbol
                    );

                arbolSerializado =
                    Encoding.UTF8.GetString(
                        bytesArbol
                    );

                bitsRelleno =
                    reader.ReadByte();

                bytesComprimidos =
                    reader.ReadBytes(
                        (int)(
                            reader.BaseStream.Length -
                            reader.BaseStream.Position
                        )
                    );
            }
        }
    }
}
