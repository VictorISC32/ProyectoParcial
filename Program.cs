using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_3er_Parcial
{
    internal class Program
    {
        static void Main(string[] args)
        {

            double[,] MatrizFinal = null; // Esta variable almacena la última matriz calculada
            string OperacionFinal = "";   // Esta variable almacena la última operación realizada

            while (true)
            {
                // Aqui muestra el menú principal
                Console.Clear();
                Console.WriteLine("Bienvenido a este programa que realiza Operaciones de Matrices\n\n");
                Console.WriteLine("Este es tu Menu de Opciones:\n");
                Console.WriteLine("1. Suma de Matrices");
                Console.WriteLine("2. Resta de Matrices");
                Console.WriteLine("3. Multiplicacion de Matrices");
                Console.WriteLine("4. Ultima Operacion Realizada");
                Console.WriteLine("5. Salir\n");
                Console.Write("Elige una opción del Menù (1-5) : ");
                string opcion = Console.ReadLine();

                    if (opcion == "5") break; // con esta opcion sales del programa

                    if (opcion == "1" || opcion == "2" || opcion == "3")
                    {
                        // Se obtienen las dimensiones de las matrices
                        int filas, columnas;
                        (filas, columnas) = DimensionesAobtener();

                        // Se obtienen los valores de las matrices 1 Y 2
                        double[,] matriz1 = ObtenerMatriz(filas, columnas, "A");
                        double[,] matriz2 = ObtenerMatriz(filas, columnas, "B");

                        // Realiza la operación seleccionada del menu 
                        if (opcion == "1")
                        {
                            MatrizFinal = SumarMatrices(matriz1, matriz2);
                            OperacionFinal = "Suma";
                        }
                        else if (opcion == "2")
                        {
                            MatrizFinal = RestarMatrices(matriz1, matriz2);
                            OperacionFinal = "Resta";
                        }
                        else if (opcion == "3")
                        {
                            MatrizFinal = MultiplicarMatrices(matriz1, matriz2);
                            OperacionFinal = "Multiplicación";
                        }

                        // Muestra el resultado y lo guarda en un archivo de texto 
                        MostrarMatriz(MatrizFinal, "Resultado");
                        GuardarMatrizEnArchivoDeTexto(MatrizFinal, "ResultadoMatriz.txt");
                        Console.WriteLine("Resultado guardado en 'ResultadoMatriz.txt'. Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                    }
                    else if (opcion == "4" && MatrizFinal != null)
                    {
                        // muestra la operacion ya hecha 
                        Console.WriteLine($"Última operación realizada: {OperacionFinal}");
                        MostrarMatriz(MatrizFinal, "Última Matriz Guardada");
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                    }
                    else if (opcion == "4")
                    {
                        // Si no hay ninguna operacion guardada te mostrara el mensaje (no hay operaciones guardadas)
                        Console.WriteLine("No hay operaciones guardadas.");
                        Console.WriteLine("Presiona cualquier tecla para reeintentar");
                        Console.ReadKey();
                    }


                Console.WriteLine("¡Gracias por hacer uso del programa! Adiós.");
            }
        }
            // Con esta funcion se obtiene las dimensiones de una matriz
            static (int, int) DimensionesAobtener()
            {
                int filas = 0, columnas = 0;

                while (true)
                {
                    try
                    {
                        Console.Write("Introduce el número de filas que desees: ");
                        filas = int.Parse(Console.ReadLine());
                        Console.Write("Introduce el número de columnas que desees: ");
                        columnas = int.Parse(Console.ReadLine());

                        if (filas > 0 && columnas > 0) break;
                        else Console.WriteLine("No puedes introducir dimensiones menores a 0.");
                    }
                    catch
                    {
                        Console.WriteLine("Por favor, introduce números enteros válidos.");
                    }
                }
                return (filas, columnas);
            }
        
        //  Con esta funcion se obtiene los valor de una matriz
        static double[,] ObtenerMatriz(int filas, int columnas, string nombre)
        {
            double[,] matriz = new double[filas, columnas];
            Console.WriteLine($"Introduce los elementos de la matriz {nombre}:");

            for (int i = 0; i < filas; i++)
            {
                for (int k = 0; k < columnas; k++)
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write($"Elemento ({i + 1},{k + 1}): ");
                            matriz[i, k] = double.Parse(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Introduce un número válido.");
                        }
                    }
                }
            }
            return matriz;
        }

        // con esta funcion vas a poder sumar las matrices
        static double[,] SumarMatrices(double[,] a, double[,] b)
        {
            int filas = a.GetLength(0);
            int columnas = a.GetLength(1);
            double[,] resultado = new double[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    resultado[i, j] = a[i, j] + b[i, j];
                }
            }

            return resultado;
        }

        // Con esta funcion vas a poder restar las matrices
        static double[,] RestarMatrices(double[,] a, double[,] b)
        {
            int filas = a.GetLength(0);
            int columnas = a.GetLength(1);
            double[,] resultado = new double[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int k = 0; k < columnas; k++)
                {
                    resultado[i, k] = a[i, k] - b[i, k];
                }
            }

            return resultado;
        }

        // Con esta funcion vas a poder multiplicar las matrices
        static double[,] MultiplicarMatrices(double[,] a, double[,] b)
        {
            int filas = a.GetLength(0);
            int columnas = b.GetLength(1);
            int n = a.GetLength(1);
            double[,] resultado = new double[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    resultado[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        resultado[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return resultado;
        }

        // Función para imprimir una matriz en pantalla
        static void MostrarMatriz(double[,] matriz, string titulo)
        {
            Console.WriteLine($"\n{titulo}:");
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Console.Write($"{matriz[i, j]} \t");
                }
                Console.WriteLine();
            }
        }

        // Función para guardar la matriz en un archivo de texto
        static void GuardarMatrizEnArchivoDeTexto(double[,] matriz, string nombreArchivo)
        {
            using (StreamWriter archivo = new StreamWriter(nombreArchivo))
            {
                int filas = matriz.GetLength(0);
                int columnas = matriz.GetLength(1);

                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        archivo.Write($"{matriz[i, j]}\t");
                    }
                    archivo.WriteLine();
                }
            }
        }

    }
}


