using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;



namespace StringManipulation
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Title = "Практическсая работа по алгоритмам";
            Console.Clear();
            Clipboard.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" 1) Задача про расстановку N ферзей на шахматной доске, размером N*N (Backtracking);");
            Console.WriteLine(" 2) Задача о ходе коня по шахматной доске, размером N*N (Backtracking);");
            Console.WriteLine(" 3) Задача о размене монет (Жадный алгоритм);");
            Console.WriteLine(" 4) Задача о пересечении отрезков (Геометрия);");
            Console.WriteLine(" 5) Задача о пересечении окружностей (Геометрия);");
            Console.WriteLine(" 6) Задача о укладке плиток (Динамическое программирование);");
            Console.WriteLine(" 7) Задача коммивояжёра (Метод ветвей и границ) - недоделана;");
            Console.WriteLine(" 8) Задача про крысу в лабиринте (Backtracking);");
            Console.WriteLine(" 11) Выход.\n");
            Console.Write(" Выберите задачу: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;

            switch (Console.ReadLine())
            {
                case "0":
                    MainMenu();
                    return true;
                case "1":
                    Queens();
                    return true;
                case "2":
                    Horses();
                    return true;
                case "3":
                    Money();
                    return true;
                case "4":
                    Segments();
                    return true;
                case "5":
                    Circles();
                    return true;
                case "6":
                    Tiles();
                    return true;
                case "7":
                    Salesman();
                    return true;
                case "8":
                    Maze();
                    return true;
                case "11":
                    return false;
                default:
                    return true;
            }
        }

        // (backtracking) Расставить N ферзей на шахматной доске N*N
        private static void Queens()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Задача про расстановку N ферзей на шахматной доске, размером N*N.");
            Console.Write("\n Введите размерность шахматной доски: ");
            Clipboard.SetText("29");
            Console.ForegroundColor = ConsoleColor.Yellow;

            int N = int.Parse(Console.ReadLine());
            int[][] lst = new int[N][];
            for (int i = 0; i < N; i++)
            {
                lst[i] = new int[N];
            }
            List<int[]> queens = new List<int[]>();
            List<int[]> resQueens = new List<int[]>();

            Stopwatch clock = new Stopwatch();
            clock.Start();
            Backtracking(0, N, lst, queens); // Запуск алгоритма

            int[][] QPosition(int k, int n, int[][] BasArray, List<int[]> Qarray) // Поиск позиции для ферзя
            {
                List<int[]> array = new List<int[]>();
                for (int i = 0; i < n; i++)
                {
                    if (BasArray[i].Contains(1))
                    {
                        continue;
                    }
                    else
                    {
                        int x = 0;
                        for (int j = 0; j < Qarray.Count; j++)
                        {
                            if (Math.Abs(i - Qarray[j][0]) == Math.Abs(k - Qarray[j][1]))
                            {
                                x = 1;
                                break;
                            }
                        }
                        if (x == 0)
                        {
                            array.Add(new int[] { i, k });
                        }
                    }
                }
                return array.ToArray();
            }

            void Backtracking(int k, int n, int[][] BaseArray, List<int[]> QueArray) // Поиск с возвратом позиций
            {

                if (k == n)
                {
                    Console.WriteLine();
                    for (int i = 0; i < BaseArray.Length; i++)
                    {
                        for (int j = 0; j < BaseArray[i].Length; j++)
                        {
                            Console.BackgroundColor = (i + j) % 2 != 0 ? ConsoleColor.DarkGray : ConsoleColor.White;
                            if (Console.BackgroundColor == ConsoleColor.White)
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            if (BaseArray[i][j] == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write(" " + BaseArray[i][j] + " ");
                            }
                            else
                            {
                                Console.Write(" " + BaseArray[i][j] + " ");
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    clock.Stop();
                    TimeSpan ts = clock.Elapsed;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n Время рассчетов: {0:00}:{1:00}.{2}",
                        ts.Minutes, ts.Seconds, ts.Milliseconds);
                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Console.WriteLine("\n Количество возможных вариантов расстановки ферзей: " + counter);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\r\nВведите \"0\", чтобы вернуться в меню или нажмите Enter чтобы повторить ");
                    if (Console.ReadLine() != "0")
                    {
                        Queens();
                    }
                    else
                    {
                        MainMenu();
                    }
                }
                else
                {
                    int[][] move = QPosition(k, n, BaseArray, QueArray);
                    for (int i = 0; i < move.Length; i++)
                    {
                        BaseArray[move[i][0]][move[i][1]] = 1;
                        QueArray.Add(new int[] { move[i][0], move[i][1] });
                        Backtracking(k + 1, n, BaseArray, QueArray);

                        BaseArray[QueArray[QueArray.Count - 1][0]][QueArray[QueArray.Count - 1][1]] = 0;
                        QueArray.RemoveAt(QueArray.Count - 1);
                    }
                }
            }
        }

        // (Backtracking) Обойти конём все шахматное поле N*N
        private static void Horses()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Задача о ходе коня по шахматной доске, размером N*N.");
            Console.Write("\n Введите размерность шахматной доски: ");
            Clipboard.SetText("8");
            Console.ForegroundColor = ConsoleColor.Yellow;

            int n = int.Parse(Console.ReadLine());
            int[] row = { 2, 1, -1, -2, -2, -1, 1, 2, 2 };
            int[] col = { 1, 2, 2, 1, -1, -2, -2, -1, 1 };
            int[][] checkedCells = new int[n][];
            for (int i = 0; i < n; i++)
            {
                checkedCells[i] = new int[n];
            }
            int startPos = 1;
            Stopwatch clock = new Stopwatch();
            clock.Start();

            kTour(checkedCells, 0, 0, startPos); // Запуск алгоритма

            bool isValid(int x, int y) // Проверяем, являются ли (x, y) действительными координатами шахматной доски
            {
                if (x < 0 || y < 0 || x >= n || y >= n)
                {
                    return false;
                }
                return true;
            }

            void kTour(int[][] visited, int x, int y, int pos) // Функция поиска пути
            {
                visited[x][y] = pos;

                // если все квадраты посещены, выводим решение
                if (pos >= n * n)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.BackgroundColor = (i + j) % 2 != 0 ? ConsoleColor.DarkGray : ConsoleColor.White;
                            if (Console.BackgroundColor == ConsoleColor.White)
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            if (visited[i][j] < 10)
                            {
                                Console.Write("  " + visited[i][j] + "  ");
                            }
                            else if (visited[i][j] < 100)
                            {
                                Console.Write("  " + visited[i][j] + " ");
                            }
                            else
                            {
                                Console.Write(" " + visited[i][j] + " ");
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    visited[x][y] = 0;
                    clock.Stop();
                    TimeSpan ts = clock.Elapsed;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n Время рассчетов: {0:00}:{1:00}.{2}",
                        ts.Minutes, ts.Seconds, ts.Milliseconds);
                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Console.WriteLine("\n Количество возможных вариантов: " + counter);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\r\nВведите \"0\", чтобы вернуться в меню или нажмите Enter чтобы повторить ");
                    if (Console.ReadLine() != "0")
                    {
                        Horses();
                    }
                    else
                    {
                        MainMenu();
                    }
                }

                for (int k = 0; k < 8; k++)
                {
                    int newX = x + row[k];
                    int newY = y + col[k];

                    if (isValid(newX, newY) && visited[newX][newY] == 0)
                    {
                        kTour(visited, newX, newY, pos + 1);
                    }
                }
                visited[x][y] = 0;
            }
        }

        // (Жадный алгоритм) Размен монет
        private static void Money()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Монетная система некоторого государства состоит из монет достоинством a1 = 1 < a2 < a3 < … < an, " +
                "причём каждый следующий номинал делится на предыдущий. Требуется выдать сумму S наименьшим возможным количеством монет.");
            Console.Write("\n Введите необходимую сумму размена: ");
            Clipboard.SetText("17271");
            Console.ForegroundColor = ConsoleColor.Yellow;

            int S = int.Parse(Console.ReadLine());
            int[] a = { 1, 2, 5, 10, 50, 100, 500, 1000, 2000, 5000 };

            int[] c = Exchange(S); // Запуск алгоритма

            Dictionary<int, int> Count = new Dictionary<int, int>();
            for (int i = 0; i < c.Length; i++)
            {
                for (int j = i; j < c.Length; j++)
                {
                    if (c[i] != c[j])
                    {
                        Count.Add(c[i], j - i);
                        i = j - 1;
                        break;
                    }
                    if (j == c.Length - 1)
                    {
                        Count.Add(c[i], j - i + 1);
                        i = j;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            foreach (var pair in Count)
            {
                Console.WriteLine($" Номинал монеты: {pair.Key}, количество: {pair.Value}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\r\nВведите \"0\", чтобы вернуться в меню или нажмите Enter чтобы повторить ");
            if (Console.ReadLine() != "0")
            {
                Money();
            }
            else
            {
                MainMenu();
            }

            int[] Exchange(int s) // Жадный алгоритм
            {
                List<int> coins = new List<int>();
                for (int i = a.Length - 1; i >= 0; i--)
                {
                    coins.AddRange(Enumerable.Repeat(a[i], s / a[i]));
                    s %= a[i];
                }
                return coins.ToArray();
            }
        }

        // (Геометрия) Узнать, пересекаются ли заданные отрезки
        private static void Segments()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Заданы два отрезка координатами своих концов. Определите, пересекаются ли эти отрезки.");
            Console.Write("\n Введите координаты отрезка \"AB\" в формате \"(x1 y1 x2 y2)\", например - (-1 3 2 2,52)/ (2 2 7 3)/ (-1 2 -1 5): ");
            Clipboard.SetText("(-1 3 2 2,52)");
            Console.ForegroundColor = ConsoleColor.Yellow;

            string coords = Console.ReadLine();
            coords = coords.Substring(1, coords.Length - 2);
            coords = coords.Replace('.', ',');
            double[] A = { double.Parse(coords.Split(' ')[0]), double.Parse(coords.Split(' ')[1]) };
            double[] B = { double.Parse(coords.Split(' ')[2]), double.Parse(coords.Split(' ')[3]) };

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n Введите координаты отрезка \"CD\" в формате \"(x1, y1, x2, y2)\", например - (2, 1, -1, 0)/ (4, 1, 5, 6)/ (2, 2, 2, 7): ");
            Clipboard.SetText("(2, 1, -1, 0)");
            Console.ForegroundColor = ConsoleColor.Yellow;

            coords = Console.ReadLine();
            coords = coords.Substring(1, coords.Length - 2);
            coords = coords.Replace('.', ',');

            double[] C = { double.Parse(coords.Split(' ')[0]), double.Parse(coords.Split(' ')[1]) };
            double[] D = { double.Parse(coords.Split(' ')[2]), double.Parse(coords.Split(' ')[3]) };
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();

            intersectionsPoint(A, B, C, D); // Запуск алгоритма

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\r\nВведите \"0\", чтобы вернуться в меню или нажмите Enter чтобы повторить ");
            if (Console.ReadLine() != "0")
            {
                Segments();
            }
            else
            {
                MainMenu();
            }

            void intersectionsPoint(double[] a, double[] b, double[] c, double[] d) // Алгоритм поиска пересечений
            {
                double accuracy = 0.000001;

                // Координаты направления отрезков
                double v1 = b[0] - a[0];
                double w1 = b[1] - a[1];
                double v2 = d[0] - c[0];
                double w2 = d[1] - c[1];

                // Длины Отрезков
                double len1 = Math.Sqrt(v1 * v1 + w1 * w1);
                double len2 = Math.Sqrt(v2 * v2 + w2 * w2);

                // Нормализация векторов (создание единичного вектора направления)
                double x1 = v1 / len1;
                double y1 = w1 / len1;
                double x2 = v2 / len2;
                double y2 = w2 / len2;

                if (a[0] == c[0] && a[1] == c[1] && b[0] == d[0] && b[1] == d[1])
                {
                    Console.WriteLine(" Отрезки AB и CD совпадают.");
                    return;
                }

                if (Math.Abs(x1 - x2) < accuracy && Math.Abs(y1 - y2) < accuracy)
                {
                    Console.WriteLine(" Отрезки AB и СD параллельны.");
                    return;
                }

                double t2 = (-w1 * c[0] + w1 * a[0] + v1 * c[1] - v1 * a[1]) / (w1 * v2 - v1 * w2);
                double t = (c[0] - a[0] + v2 * t2) / v1;
                if (t < 0 || t > 1 || t2 < 0 || t2 > 1)
                {
                    Console.WriteLine(" Отрезки AB и CD не имеют точки пересечения.");
                    return;
                }
                double PointX = Math.Round(c[0] + v2 * t2, 4);
                double PointY = Math.Round(c[1] + w2 * t2, 4);
                Console.WriteLine($" Отрезки AB и CD пересекаются в точке: ({PointX}, {PointY})");
            }
        }

        // (Геометрия) Определить точки пересечения окружностей
        private static void Circles()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Найти точки персечения двух заданных окружностей.");
            Console.Write("\n Введите данные окружности \"O\" в формате \"(x y r)\", например - (2 0 0,5) (0 0 2): ");
            Clipboard.SetText("(2 0 0,5)");
            Console.ForegroundColor = ConsoleColor.Yellow;

            string circleData = Console.ReadLine();
            circleData = circleData.Substring(1, circleData.Length - 2);
            circleData = circleData.Replace('.', ',');
            double[] O = { double.Parse(circleData.Split(' ')[0]), double.Parse(circleData.Split(' ')[1]), double.Parse(circleData.Split(' ')[2]) };

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n Введите данные окружности \"O1\" в формате \"(x y r)\", например - (2 1 0,5) (4 0 1): ");
            Clipboard.SetText("(2 1 0,5)");
            Console.ForegroundColor = ConsoleColor.Yellow;

            circleData = Console.ReadLine();
            circleData = circleData.Substring(1, circleData.Length - 2);
            circleData = circleData.Replace('.', ',');
            double[] O1 = { double.Parse(circleData.Split(' ')[0]), double.Parse(circleData.Split(' ')[1]), double.Parse(circleData.Split(' ')[2]) };

            List<List<double>> answer = new List<List<double>>();
            bool isIntersect = circleIntersections(O[0], O[1], O[2], O1[0], O1[1], O1[2], answer); // Запуск алгоритма

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            if (isIntersect)
            {
                for (int i = 0; i < answer.Count; i++)
                {
                    Console.Write(" Точка пересечения " + (i + 1) + ": (" + answer[i][0] + ", " + answer[i][1] + ")");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine(" Окружности не имеют точек пересечения, или совпадают!");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\r\nВведите \"0\", чтобы вернуться в меню или нажмите Enter чтобы повторить ");
            if (Console.ReadLine() != "0")
            {
                Circles();
            }
            else
            {
                MainMenu();
            }

            bool circleIntersections(double x0, double y0, double r0, double x1, double y1, double r1, List<List<double>> coords) // Алгоритм поиска пересечений окружности
            {
                double d = Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));

                if (d > r0 + r1 || d < Math.Abs(r0 - r1) || (d == 0 && r0 == r1))
                {
                    return false;
                }
                else
                {
                    double a = (Math.Pow(r0, 2) - Math.Pow(r1, 2) + Math.Pow(d, 2)) / (2 * d);
                    double h = Math.Sqrt(Math.Pow(r0, 2) - Math.Pow(a, 2));
                    double x2 = Math.Round(x0 + a * (x1 - x0) / d, 4);
                    double y2 = Math.Round(y0 + a * (y1 - y0) / d, 4);
                    double x3 = Math.Round(x2 + h * (y1 - y0) / d, 4);
                    double y3 = Math.Round(y2 - h * (x1 - x0) / d, 4);
                    double x4 = Math.Round(x2 - h * (y1 - y0) / d, 4);
                    double y4 = Math.Round(y2 + h * (x1 - x0) / d, 4);
                    coords.Add(new List<double> { x3, y3 });
                    coords.Add(new List<double> { x4, y4 });
                    return true;
                }
            }
        }

        // (Динамическое программирование) Плитки
        private static void Tiles()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Есть поле из клеток, размеры которого [n * m]. Необходимо покрыть это поле паркетом размера [1 * 2] (их можно поворачивать) таким образом, чтоб все клетки поля были накрыты, и чтоб плитки не вылазили за границы поля. Задача состоит в том, чтоб найти количество таких покрытий.");
            Console.Write("\n Введите размерность поля n: ");
            Clipboard.SetText("2");
            Console.ForegroundColor = ConsoleColor.Yellow;
            int N = int.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n Введите размерность поля m: ");
            Clipboard.SetText("3");
            Console.ForegroundColor = ConsoleColor.Yellow;
            int M = int.Parse(Console.ReadLine());

            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("\n Количество покрытий поля " + N + "x" + M + " = " + CountTileCoverings(M, N));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\r\nВведите \"0\", чтобы вернуться в меню или нажмите Enter чтобы повторить ");
            if (Console.ReadLine() != "0")
            {
                Tiles();
            }
            else
            {
                MainMenu();
            }

            int CountTileCoverings(int m, int n)
            {
                int[][] board = new int[n][];
                for (int i = 0; i < n; i++)
                {
                    board[i] = new int[m];
                    for (int j = 0; j < m; j++)
                    {
                        board[i][j] = 0;
                    }
                }
                int[] count = new int[1];
                TileBoard(0, 0);
                return count[0];

                void TileBoard(int i, int j)
                {
                    if (i == n)
                    {
                        count[0] += 1;
                        return;
                    }
                    else if (j == m)
                    {
                        TileBoard(i + 1, 0);
                    }
                    else if (board[i][j] == 1)
                    {
                        TileBoard(i, j + 1);
                    }
                    else
                    {
                        if (j < m - 1 && board[i][j + 1] == 0)
                        {
                            board[i][j] = 1;
                            board[i][j + 1] = 1;
                            TileBoard(i, j + 2);
                            board[i][j] = 0;
                            board[i][j + 1] = 0;
                        }
                        if (i < n - 1 && board[i + 1][j] == 0)
                        {
                            board[i][j] = 1;
                            board[i + 1][j] = 1;
                            TileBoard(i, j + 1);
                            board[i][j] = 0;
                            board[i + 1][j] = 0;
                        }
                    }
                }
            }
        }

        // (Метод ветвей и границ) Задача коммивояжёра 
        private static void Salesman()
        {
            // https://github.com/Clever-Shadow/python-salesman
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Условие.");
            Console.Write("\n Введите размерность n: ");
            Clipboard.SetText("4");
            Console.ForegroundColor = ConsoleColor.Yellow;

            int n = int.Parse(Console.ReadLine());

            // Массивы для сохранения индексов
            List<int> Str = new List<int>();
            List<int> Stb = new List<int>();
            for (int i = 0; i < n; i++)
            {
                Str.Add(i);
                Stb.Add(i);
            }

            // Главная матрица
            List<List<int>> StartMatrix = new List<List<int>>();
            Console.WriteLine();
            for (int r = 0; r < n; r++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Введите " + (r + 1) + " строку матрицы: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                StartMatrix.Add(Console.ReadLine().Split().Select(int.Parse).ToList());
            }

            // Копия матрицы (рабочая)
            List<List<int>> matrix = new List<List<int>>();
            for (int rc = 0; rc < n; rc++)
            {
                matrix.Add(new List<int>(StartMatrix[rc]));
            }
            for (int rc = 0; rc < n; rc++)
            {
                matrix[rc][rc] = int.MaxValue;
            }

            List<int> res = new List<int>();
            List<int> result = new List<int>();
            int H = 0;

            pathFinder();

            void pathFinder()
            {
                while (true)
                {
                    for (int i = 0; i < matrix.Count; i++)
                    {
                        int temp = matrix[i].Min();
                        H += temp;
                        for (int j = 0; j < matrix.Count; j++)
                        {
                            matrix[i][j] -= temp;
                        }
                    }

                    for (int i = 0; i < matrix.Count; i++)
                    {
                        int temp = matrix.Min(row => row[i]);
                        H += temp;
                        for (int j = 0; j < matrix.Count; j++)
                        {
                            matrix[j][i] -= temp;
                        }
                    }

                    int NullMax = 0;
                    int index1 = 0;
                    int index2 = 0;
                    int tmp = 0;
                    for (int r = 0; r < matrix.Count; r++)
                    {
                        for (int c = 0; c < matrix[0].Count; c++)
                        {
                            if (matrix[r][c] == 0)
                            {
                                tmp = minFind(matrix[r], c) + minFind(matrix[r], r);
                                if (tmp >= NullMax)
                                {
                                    NullMax = tmp;
                                    index1 = r;
                                    index2 = c;
                                }
                            }
                        }
                    }

                    res.Add(Str[index1] + 1);
                    res.Add(Stb[index2] + 1);
                    int oldIndex1 = Str[index1];
                    int oldIndex2 = Stb[index2];
                    if (Str.Contains(oldIndex2) && Stb.Contains(oldIndex1))
                    {
                        int NewIndex1 = Str.IndexOf(oldIndex2);
                        int NewIndex2 = Stb.IndexOf(oldIndex1);
                        matrix[NewIndex1][NewIndex2] = int.MaxValue;
                    }
                    Str.RemoveAt(index1);
                    Stb.RemoveAt(index2);
                    matrix = Delete(matrix, index1, index2);
                    if (matrix.Count == 1) { break; }
                }
            }


            // Формирование порядка пути
            for (int с = 0; с < res.Count - 1; с += 2)
            {
                if (res.Count(x => x == res[с]) < 2)
                {
                    result.Add(res[с]);
                    result.Add(res[с + 1]);
                }
            }
            for (int i = 0; i < res.Count - 1; i += 2)
            {
                for (int j = 0; j < res.Count - 1; j += 2)
                {
                    if (result[result.Count - 1] == res[j])
                    {
                        result.Add(res[j]);
                        result.Add(res[j + 1]);
                    }
                }
            }
            Console.WriteLine("----------------------------------");
            Console.WriteLine(string.Join(" ", result));

            // Рассчёт длины пути
            int PathLenght = 0;
            for (int i = 0; i < result.Count - 1; i += 2)
            {
                if (i == result.Count - 2)
                {
                    PathLenght += StartMatrix[result[i] - 1][result[i + 1] - 1];
                    PathLenght += StartMatrix[result[i + 1] - 1][result[0] - 1];
                }
                else
                {
                    PathLenght += StartMatrix[result[i] - 1][result[i + 1] - 1];
                }
            }
            Console.WriteLine(PathLenght);
            Console.WriteLine("----------------------------------");


            // Функция нахождения минимального элемента, исключая текущий элемент
            int minFind(List<int> lst, int myindex)
            {
                return lst.Where((x, idx) => idx != myindex).Min();
            }

            // Функция удаления нужной строки и столбца
            List<List<int>> Delete(List<List<int>> deletedMatrix, int index1, int index2)
            {
                deletedMatrix.RemoveAt(index1);
                foreach (var i in deletedMatrix)
                {
                    i.RemoveAt(index2);
                }
                return deletedMatrix;
            }

            // Функция вывода матрицы
            void PrintMatrix(List<List<int>> printMatrix)
            {
                Console.WriteLine("---------------");
                foreach (var i in printMatrix)
                {
                    Console.WriteLine(string.Join(" ", i));
                }
                Console.WriteLine("---------------");
            }
            Console.ReadLine();
        }

        // (Backtracking) Крыса в лабиринте
        private static void Maze()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Рассмотрим крысу, помещенную в (0, 0) в квадратной матрице порядка N * N. Она должна добраться до пункта назначения (N – 1, N – 1). " +
                " Значение 0 в ячейке матрицы означает, что она заблокирована и крыса не может перейти к ней, в то время как значение 1 в ячейке матрицы означает, что крыса может пройти через неё.");
            //Console.Write("\n Размерность лабиринта n: ");
            //Clipboard.SetText("5");
            Console.ForegroundColor = ConsoleColor.Yellow;

            //int[,] baseMaze = { { 1, 0, 0, 0 },
            //                    { 1, 1, 0, 1 },
            //                    { 0, 1, 0, 0 },
            //                    { 1, 1, 1, 1 } };

            int[,] baseMaze = { { 1, 0, 0, 0, 1 },
                                { 1, 1, 0, 0, 1 },
                                { 0, 1, 0, 0, 1 },
                                { 0, 1, 0, 1, 1 },
                                { 0, 1, 1, 1, 1 }};

            int n = baseMaze.GetLength(0);
            printSolution(baseMaze, false);
            solveMaze(baseMaze);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\r\nВведите \"0\", чтобы вернуться в меню или нажмите Enter чтобы повторить ");
            if (Console.ReadLine() != "0")
            {
                Maze();
            }
            else
            {
                MainMenu();
            }

            void printSolution(int[,] sol, bool isAnsw)
            {
                if (isAnsw)
                {
                    Console.WriteLine();
                    Console.WriteLine(" Решение: ");
                    for (int r = 0; r < n; r++)
                    {
                        for (int c = 0; c < n; c++)
                        {
                            if (sol[r, c] == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(" " + sol[r, c] + " ");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write(" " + sol[r, c] + " ");
                            }
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n Входные данные: ");
                    for (int r = 0; r < n; r++)
                    {
                        for (int c = 0; c < n; c++)
                        {
                            Console.Write(" " + sol[r, c] + " ");
                        }
                        Console.WriteLine();
                    }
                }
            }

            // Проверка того, являются ли x и y - частью поля
            bool isSafe(int[,] maze, int x, int y)
            {

                return (x >= 0 && x < n && y >= 0 &&
                        y < n && maze[x, y] == 1);
            }

            bool solveMaze(int[,] maze) // Алгоритм поиска пути
            {
                int[,] sol = new int[n, n];

                if (solveMazeUtil(maze, 0, 0, sol) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Решения не существует!");
                    return false;
                }

                printSolution(sol, true);
                return true;
            }

            bool solveMazeUtil(int[,] maze, int x, int y, int[,] sol)
            {
                if (x == n - 1 && y == n - 1 &&
                    maze[x, y] == 1)
                {
                    sol[x, y] = 1;
                    return true;
                }

                if (isSafe(maze, x, y) == true)
                { 
                    if (sol[x, y] == 1)
                    { return false; }
                    sol[x, y] = 1;
                    if (solveMazeUtil(maze, x + 1, y, sol) || solveMazeUtil(maze, x, y + 1, sol) || solveMazeUtil(maze, x - 1, y, sol) || solveMazeUtil(maze, x, y - 1, sol))
                    {
                        return true;
                    }
                    sol[x, y] = 0;
                    return false;
                }
                return false;
            }
        }

        // Наибольшая подпоследовательность
        private static void Subsequence()
        {

        }
    }
}

/**
 
3) задача о музыкантах (методом ветвей и границ)(в прикрепленных фото) -сам ее скорее всего делать не буду
4) задача метода ветвей и границ - задача коммивояжера
5) задача на динамическое программирование (распределение глав по томам или любая другая)
6) задача на жадные алгоритмы (любая)
7) Задача на разбиение множества чисел на равные (с возможной равной суммой) -не делал и скорее всего не буду
8) Есть поле из клеток, размеры которого [n * m]. 
Необходимо покрыть это поле паркетом размера [1 * 2] (их можно поворачивать) таким образом, чтоб все клетки поля были накрыты, и чтоб плитки не вылазили за границы поля. 
Задача состоит в том, чтоб найти количество таких покрытий.
9) Имеется несколько романов одного писателя. 
Для каждого из них известен объем (число) страниц. Для издания сочинений романы надо сгруппировать в тройки. 
Каждая тройка произведений будет печататься в одном томе. Если число романов не кратно трем, остаток печатается в отдельном томе. 
Требуется найти такую группировку, при которой объем самого толстого тома минимален.
*/
