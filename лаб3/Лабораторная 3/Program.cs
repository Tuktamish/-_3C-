using System.IO;
using System.Linq;
using System.Collections.Generic;

public class MatrixOperations
{
    private int[,] matrix1;
    private int[,] matrix2;
    private int[,] matrix3;

    // Конструктор для задания 1
    public MatrixOperations(int n, int m)
    {
        matrix1 = new int[n, m];
        Console.WriteLine("Введите элементы первого массива по столбцам:");
        for (int j = 0; j < m; j++)
        {
            for (int i = 0; i < n; i++)
            {
                matrix1[i, j] = int.Parse(Console.ReadLine());
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
                matrix[i, j] = rand.Next(1, 5); // Заполняем случайными числами
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
    // Задание 2
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
                // Генерируем случайные числа от 1 до (sum - currentSum - (n - i - 1))
                resultMatrix[i, j] = new Random().Next(1, sum - currentSum - (n - i - 1) + 1);
                currentSum += resultMatrix[i, j];
            }

            // Последний элемент строки, который гарантированно будет положительным
            resultMatrix[n - 1, j] = sum - currentSum;

            // Если последний элемент равен нулю, перераспределяем значения
            if (resultMatrix[n - 1, j] <= 0)
            {
                // Увеличиваем предыдущие элементы, чтобы обеспечить положительность последнего
                for (int i = 0; i < n - 1; i++)
                {
                    resultMatrix[i, j]++;
                    if (resultMatrix[n - 1, j] > 0) break;
                }
            }
        }

        return resultMatrix;
    }

    // Задание 4
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

    // Задание 6
    public static void CreateRandomNumbersFile(string filePath, int count)
    {
        Random rand = new Random();
        using (var writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(rand.Next(1, 101)); // Случайные числа от 1 до 100
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

    // Задание 7
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
                    oddNumber = rand.Next(1, 100); // Генерируем случайное число от 1 до 99
                } while (oddNumber % 2 == 0); // Проверяем, что оно нечётное

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

    // Задание 8
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
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                // Задание 1
                Console.WriteLine("Введите размеры матрицы (n m):");
                var sizes = Console.ReadLine().Split();
                int n1 = int.Parse(sizes[0]);
                int m1 = int.Parse(sizes[1]);
                MatrixOperations matrixOps = new MatrixOperations(n1, m1);
                Console.WriteLine(matrixOps.ToString());
                break;

            case 2:
                // Задание 2
                Console.WriteLine("Введите количество элементов в одномерном массиве:");
                int n = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите элементы одномерного массива (через пробел):");
                int[] oneDimArray = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                // Создание двумерного массива
                int[,] resultMatrix = FileOperations.CreateTwoDimensionalArray(oneDimArray);
                Console.WriteLine($"Результирующий двумерный массив:\n{MatrixOperations.MatrixToString(resultMatrix)}");
                break;

            case 4:
                // Задание 4
                FileOperations.CreateFileFromSource("string.txt", "destination.txt");
                Console.WriteLine("Файл создан согласно заданию 4.");
                break;

            case 6:
                // Задание 6
                string randomNumbersFile = "numbers.txt";
                FileOperations.CreateRandomNumbersFile(randomNumbersFile, 10);
                FileOperations.FindMaxMinProduct(randomNumbersFile);
                break;

            case 7:
                // Задание 7
                string oddNumbersFile = "odd_numbers.txt";
                FileOperations.CreateRandomOddNumbersFile(oddNumbersFile, 10);
                FileOperations.CountOddNumbers(oddNumbersFile);
                break;

            case 8:
                // Задание 8
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
