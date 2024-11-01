using System.IO;
using System.Linq;
using System.Collections.Generic;

public class MatrixOperations
{
    private int[,] matrix1;
    private int[,] matrix2;
    private int[,] matrix3;

    // Конструктор для инициализации матриц
    public MatrixOperations(int n, int m)
    {
        matrix1 = new int[n, m];
        Console.WriteLine("Введите элементы первого массива по столбцам:");

        for (int j = 0; j < m; j++)
        {
            for (int i = 0; i < n; i++)
            {
                while (true) // Цикл для проверки корректности ввода
                {
                    Console.Write($"Введите элемент [{i + 1}, {j + 1}]: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int value)) 
                    {
                        matrix1[i, j] = value;
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число."); 
                    }
                }
            }
        }

        matrix2 = new int[n, n];
        Random rand = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int num;
                do
                {
                    num = rand.Next(100, 1000);
                } while (!IsIncreasing(num));
                matrix2[i, j] = num;
            }
        }

        matrix3 = CreateMatrix3(n);
    }

    private bool IsIncreasing(int num)
    {
        string numStr = num.ToString();
        return numStr[0] < numStr[1] && numStr[1] < numStr[2];
    }

    private int[,] CreateMatrix3(int n)
    {
        int[,] matrix = new int[n, n];
        Random rand = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = rand.Next(1, 5);
            }
        }
        return matrix;
    }

    public override string ToString()
    {
        return $"Matrix1:\n{MatrixToString(matrix1)}\nMatrix2:\n{MatrixToString(matrix2)}\nMatrix3:\n{MatrixToString(matrix3)}";
    }

    public static string MatrixToString(int[,] matrix)
    {
        var result = "";
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result += matrix[i, j] + "\t";
            }
            result += "\n";
        }
        return result;
    }
}

public class FileOperations
{
    public static int[,] CreateTwoDimensionalArray(int[] oneDimensionalArray)
    {
        int n = oneDimensionalArray.Length;
        int[,] resultMatrix = new int[n, n];

        for (int j = 0; j < n; j++)
        {
            int sum = oneDimensionalArray[j];
            int currentSum = 0;

            for (int i = 0; i < n - 1; i++)
            {
                resultMatrix[i, j] = new Random().Next(1, sum - currentSum - (n - i - 1) + 1);
                currentSum += resultMatrix[i, j];
            }

            resultMatrix[n - 1, j] = sum - currentSum;

            if (resultMatrix[n - 1, j] <= 0)
            {
                for (int i = 0; i < n - 1; i++)
                {
                    resultMatrix[i, j]++;
                    if (resultMatrix[n - 1, j] > 0) break;
                }
            }
        }

        return resultMatrix;
    }

    public static void CreateFileFromSource(string sourceFile, string destinationFile)
    {
        var lines = File.ReadAllLines(sourceFile);
        using (var writer = new StreamWriter(destinationFile))
        {
            for (int i = 1; i <= lines.Length; i++)
            {
                writer.WriteLine(string.Join(" ", lines.Take(i)));
            }
        }
    }

    public static void CreateRandomNumbersFile(string filePath, int count)
    {
        Random rand = new Random();
        using (var writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(rand.Next(1, 101));
            }
        }
    }

    public static void FindMaxMinProduct(string filePath)
    {
        var numbers = File.ReadAllLines(filePath).Select(int.Parse).ToList();
        int max = numbers.Max();
        int min = numbers.Min();
        Console.WriteLine($"max: {max} min: {min}");
    }

    public static void CreateRandomOddNumbersFile(string filePath, int count)
    {
        Random rand = new Random();
        using (var writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                int oddNumber;
                do
                {
                    oddNumber = rand.Next(1, 100);
                } while (oddNumber % 2 == 0);

                writer.WriteLine(oddNumber);
            }
        }
    }

    public static void CountOddNumbers(string filePath)
    {
        var numbers = File.ReadAllLines(filePath).Select(int.Parse);
        int oddCount = numbers.Count(n => n % 2 != 0);
        Console.WriteLine($"Количество нечётных элементов: {oddCount}");
    }

    public static void CreateLineLengthFile(string sourceFile, string destinationFile)
    {
        var lines = File.ReadAllLines(sourceFile);
        using (var writer = new StreamWriter(destinationFile))
        {
            foreach (var line in lines)
            {
                writer.WriteLine(line.Length);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите задание (1, 2, 4, 6, 7, 8):");

        
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 8)
        {
            Console.WriteLine("Некорректный выбор задания. Пожалуйста, выберите число от 1 до 8.");
        }

        switch (choice)
        {
            case 1:
                Console.WriteLine("Введите размеры матрицы (n m):");
                int n1, m1;

               
                while (true)
                {
                    var sizes = Console.ReadLine().Split();
                    if (sizes.Length == 2 &&
                        int.TryParse(sizes[0], out n1) && n1 > 0 &&
                        int.TryParse(sizes[1], out m1) && m1 > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите два положительных числа.");
                }

                MatrixOperations matrixOps = new MatrixOperations(n1, m1);
                Console.WriteLine(matrixOps.ToString());
                break;

            case 2:
                Console.WriteLine("Введите количество элементов в одномерном массиве:");
                int n;

                
                while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное число.");
                }

                Console.WriteLine("Введите элементы одномерного массива (через пробел):");
                int[] oneDimArray = new int[n];

                
                while (true)
                {
                    var input = Console.ReadLine().Split();
                    if (input.Length == n && input.All(x => int.TryParse(x, out _)))
                    {
                        oneDimArray = Array.ConvertAll(input, int.Parse);
                        break;
                    }
                    Console.WriteLine($"Некорректный ввод. Пожалуйста, введите {n} целых чисел через пробел.");
                }

                int[,] resultMatrix = FileOperations.CreateTwoDimensionalArray(oneDimArray);
                Console.WriteLine($"Результирующий двумерный массив:\n{MatrixOperations.MatrixToString(resultMatrix)}");
                break;

            case 4:
                FileOperations.CreateFileFromSource("string.txt", "destination.txt");
                Console.WriteLine("Файл создан согласно заданию 4.");
                break;

            case 6:
                string randomNumbersFile = "numbers.txt";
                FileOperations.CreateRandomNumbersFile(randomNumbersFile, 10);
                FileOperations.FindMaxMinProduct(randomNumbersFile);
                break;

            case 7:
                string oddNumbersFile = "odd_numbers.txt";
                FileOperations.CreateRandomOddNumbersFile(oddNumbersFile, 10);
                FileOperations.CountOddNumbers(oddNumbersFile);
                break;

            case 8:
                FileOperations.CreateLineLengthFile("string.txt", "lengths.txt");
                Console.WriteLine("Файл длины строк создан.");
                break;

            default:
                Console.WriteLine("Некорректный выбор задания.");
                break;
        }
    }

    public static string MatrixToString(int[,] matrix)
    {
        var result = "";
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result += matrix[i, j] + "\t";
            }
            result += "\n";
        }
        return result;
    }
}
