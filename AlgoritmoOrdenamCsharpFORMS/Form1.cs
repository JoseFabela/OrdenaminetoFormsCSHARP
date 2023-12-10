using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoritmoOrdenamCsharpFORMS
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            // Configuración de la ventana principal
            Text = "Army Management";
            Size = new System.Drawing.Size(400, 300);

            // Configuración del ComboBox para seleccionar el algoritmo de ordenamiento
            sortingAlgorithmComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Agrega los algoritmos de ordenamiento al ComboBox
            sortingAlgorithmComboBox.Items.AddRange(GetSortingAlgorithmNames());

            // Configuración de la ListBox para mostrar la lista de unidades
            armyListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 50),
                Size = new System.Drawing.Size(300, 200)
            };

            // Configuración del evento de cambio de selección en el ComboBox
            sortingAlgorithmComboBox.SelectedIndexChanged += (sender, e) => RunGame();

            // Agrega los controles a la ventana
            Controls.Add(sortingAlgorithmComboBox);
            Controls.Add(armyListBox);

            // Asigna el evento Load del formulario
            Load += Form1_Load;

        }

        private ComboBox sortingAlgorithmComboBox;
        private ListBox armyListBox;

        private void Form1_Load(object sender, EventArgs e)
        {
            RunGame();
        }
        private void RunGame()
        {
            List<Unit> army = GenerateArmy();

            // Obtiene el algoritmo de ordenamiento seleccionado
            ISortAlgorithm<Unit> sortingAlgorithm = GetSortingAlgorithm(sortingAlgorithmComboBox.SelectedIndex + 1);

            // Ordena el ejército si se seleccionó un algoritmo válido
            if (sortingAlgorithm != null)
            {
                army = sortingAlgorithm.Sort(army, Comparer<Unit>.Default.Compare);

                // Muestra el ejército ordenado en la ListBox
                armyListBox.Items.Clear();
                foreach (var unit in army)
                {
                    armyListBox.Items.Add(unit);
                }
            }
        }

        private string[] GetSortingAlgorithmNames()
        {
            // Retorna los nombres de los algoritmos de ordenamiento
            return new string[]
            {
            "Shell Sort", "Selection Sort", "HeapSort", "QuickSort", "BubbleSort",
            "CocktailSort", "InsertionSort", "BucketSort", "CountingSort", "MergeSort",
            "BinaryTreeSort", "RadixSort", "GnomeSort", "NaturalMergeSort", "StraightMergeSort"
            };
        }
        private ISortAlgorithm<Unit> GetSortingAlgorithm(int choice)
        {
            // Retorna la instancia del algoritmo de ordenamiento seleccionado
            switch (choice)
            {
                case 1:
                    return new ShellSort<Unit>();
                case 2:
                    return new SelectionSort<Unit>();
                case 3:
                    return new HeapSort<Unit>();
                case 4:
                    return new QuickSort<Unit>();
                case 5:
                    return new BubbleSort<Unit>();
                case 6:
                    return new CocktailSort<Unit>();
                case 7:
                    return new InsertionSort<Unit>();
                case 8:
                    return new BucketSort<Unit>();
                case 9:
                    return new CountingSort<Unit>();
                case 10:
                    return new MergeSort<Unit>();
                case 11:
                    return new BinaryTreeSort<Unit>();
                case 12:
                    return new RadixSort<Unit>();
                case 13:
                    return new GnomeSort<Unit>();
                case 14:
                    return new NaturalMergeSort<Unit>();
                    case 15:
                        return new StraightMergeSort<Unit>();
                default:
                    return null;
            }
        }



        private List<Unit> GenerateArmy()
        {
            // Genera una lista de unidades aleatorias
            Random random = new Random();
            List<Unit> army = new List<Unit>();

            for (int i = 0; i < 5; i++)
            {
                army.Add(new Unit
                {
                    Name = $"Unit{i + 1}",
                    Level = random.Next(1, 10),
                    AttackPower = random.Next(10, 30),
                    Speed = random.Next(5, 20)
                });
            }

            return army;
        }
    }

    interface ISortAlgorithm<T> where T : IComparable<T>
    {
        List<T> Sort(List<T> input, Comparison<T> comparison);
    }


    class ShellSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            int n = input.Count;
            int gap = n / 2;

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = input[i];
                    int j = i;

                    while (j >= gap && comparison(input[j - gap], temp) > 0)
                    {
                        input[j] = input[j - gap];
                        j -= gap;
                    }

                    input[j] = temp;
                }

                gap /= 2;
            }

            return input;
        }
    }

    class SelectionSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            int n = input.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (comparison(input[j], input[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                // Swap elements
                T temp = input[i];
                input[i] = input[minIndex];
                input[minIndex] = temp;
            }

            return input;
        }
    }

    // Implementa los demás algoritmos de ordenamiento de manera similar.
    class HeapSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            int n = input.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(input, n, i, comparison);
            }

            for (int i = n - 1; i >= 0; i--)
            {
                T temp = input[0];
                input[0] = input[i];
                input[i] = temp;

                Heapify(input, i, 0, comparison);
            }

            return input;
        }

        void Heapify(List<T> arr, int n, int i, Comparison<T> comparison)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && comparison(arr[left], arr[largest]) > 0)
            {
                largest = left;
            }

            if (right < n && comparison(arr[right], arr[largest]) > 0)
            {
                largest = right;
            }

            if (largest != i)
            {
                T swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                Heapify(arr, n, largest, comparison);
            }
        }
    }
    class QuickSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            QuickSortRecursive(input, 0, input.Count - 1, comparison);
            return input;
        }

        void QuickSortRecursive(List<T> arr, int low, int high, Comparison<T> comparison)
        {
            if (low < high)
            {
                int partitionIndex = Partition(arr, low, high, comparison);

                QuickSortRecursive(arr, low, partitionIndex - 1, comparison);
                QuickSortRecursive(arr, partitionIndex + 1, high, comparison);
            }
        }

        int Partition(List<T> arr, int low, int high, Comparison<T> comparison)
        {
            T pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (comparison(arr[j], pivot) < 0)
                {
                    i++;
                    T temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            T temp2 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp2;

            return i + 1;
        }
    }
    class BubbleSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            int n = input.Count;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (comparison(input[j], input[j + 1]) > 0)
                    {
                        T temp = input[j];
                        input[j] = input[j + 1];
                        input[j + 1] = temp;
                    }
                }
            }

            return input;
        }
    }
    class CocktailSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            int n = input.Count;
            bool swapped;

            do
            {
                swapped = false;

                for (int i = 0; i < n - 1; i++)
                {
                    if (comparison(input[i], input[i + 1]) > 0)
                    {
                        T temp = input[i];
                        input[i] = input[i + 1];
                        input[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;

                for (int i = n - 2; i >= 0; i--)
                {
                    if (comparison(input[i], input[i + 1]) > 0)
                    {
                        T temp = input[i];
                        input[i] = input[i + 1];
                        input[i + 1] = temp;
                        swapped = true;
                    }
                }

            } while (swapped);

            return input;
        }
    }
    class InsertionSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            int n = input.Count;

            for (int i = 1; i < n; i++)
            {
                T key = input[i];
                int j = i - 1;

                while (j >= 0 && comparison(input[j], key) > 0)
                {
                    input[j + 1] = input[j];
                    j = j - 1;
                }

                input[j + 1] = key;
            }

            return input;
        }
    }
    class BucketSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            if (input.Count == 0)
                return input;

            // Encuentra el valor máximo y mínimo en la lista
            T minValue = input[0];
            T maxValue = input[0];

            foreach (var item in input)
            {
                if (comparison(item, minValue) < 0)
                    minValue = item;

                if (comparison(item, maxValue) > 0)
                    maxValue = item;
            }

            // Inicializa los baldes
            List<List<T>> buckets = new List<List<T>>();

            // Crea los baldes
            for (int i = 0; i < input.Count; i++)
            {
                buckets.Add(new List<T>());
            }

            // Distribuye los elementos en los baldes
            foreach (var item in input)
            {
                int bucketIndex = input.IndexOf(item);
                buckets[bucketIndex].Add(item);
            }

            // Ordena cada balde e inserta los elementos ordenados en la lista final
            List<T> sortedList = new List<T>();
            foreach (var bucket in buckets)
            {
                if (bucket.Count > 0)
                {
                    bucket.Sort(comparison);
                    sortedList.AddRange(bucket);
                }
            }

            return sortedList;
        }
    }

    class CountingSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            // Encuentra el valor máximo en la lista
            T maxVal = input[0];
            foreach (var item in input)
            {
                if (comparison(item, maxVal) > 0)
                {
                    maxVal = item;
                }
            }

            // Crea un diccionario para contar la frecuencia de cada elemento
            Dictionary<T, int> count = new Dictionary<T, int>();

            // Llena el diccionario de conteo
            foreach (var item in input)
            {
                if (count.ContainsKey(item))
                {
                    count[item]++;
                }
                else
                {
                    count[item] = 1;
                }
            }

            // Reconstruye el array ordenado utilizando el diccionario de conteo
            List<T> sortedList = new List<T>();
            foreach (var kvp in count)
            {
                for (int j = 0; j < kvp.Value; j++)
                {
                    sortedList.Add(kvp.Key);
                }
            }

            return sortedList;
        }
    }

    class StraightMergeSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            return MergeSort(input, comparison);
        }

        private List<T> MergeSort(List<T> input, Comparison<T> comparison)
        {
            if (input.Count <= 1)
                return input;

            int middle = input.Count / 2;
            List<T> left = input.GetRange(0, middle);
            List<T> right = input.GetRange(middle, input.Count - middle);

            left = MergeSort(left, comparison);
            right = MergeSort(right, comparison);

            return Merge(left, right, comparison);
        }

        private List<T> Merge(List<T> left, List<T> right, Comparison<T> comparison)
        {
            List<T> result = new List<T>();
            int leftIndex = 0, rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (comparison(left[leftIndex], right[rightIndex]) <= 0)
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            while (leftIndex < left.Count)
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }

            while (rightIndex < right.Count)
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }

            return result;
        }
    }
    class NaturalMergeSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            List<List<T>> runs = IdentifyRuns(input, comparison);

            while (runs.Count > 1)
            {
                List<T> result = new List<T>();
                int i = 0;

                while (i < runs.Count - 1)
                {
                    result.AddRange(Merge(runs[i], runs[i + 1], comparison));
                    i += 2;
                }

                if (i == runs.Count - 1)
                {
                    result.AddRange(runs[i]);
                }

                runs = IdentifyRuns(result, comparison);
            }

            return runs[0];
        }

        private List<List<T>> IdentifyRuns(List<T> input, Comparison<T> comparison)
        {
            List<List<T>> runs = new List<List<T>>();
            List<T> currentRun = new List<T> { input[0] };

            for (int i = 1; i < input.Count; i++)
            {
                if (comparison(input[i], input[i - 1]) >= 0)
                {
                    currentRun.Add(input[i]);
                }
                else
                {
                    runs.Add(currentRun);
                    currentRun = new List<T> { input[i] };
                }
            }

            runs.Add(currentRun);
            return runs;
        }

        private List<T> Merge(List<T> left, List<T> right, Comparison<T> comparison)
        {
            List<T> result = new List<T>();
            int leftIndex = 0, rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (comparison(left[leftIndex], right[rightIndex]) <= 0)
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            while (leftIndex < left.Count)
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }

            while (rightIndex < right.Count)
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }

            return result;
        }
    }
    class MergeSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            if (input.Count <= 1)
                return input;

            int middle = input.Count / 2;
            List<T> left = input.GetRange(0, middle);
            List<T> right = input.GetRange(middle, input.Count - middle);

            left = Sort(left, comparison);
            right = Sort(right, comparison);

            return Merge(left, right, comparison);
        }

        private List<T> Merge(List<T> left, List<T> right, Comparison<T> comparison)
        {
            List<T> result = new List<T>();
            int leftIndex = 0, rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (comparison(left[leftIndex], right[rightIndex]) <= 0)
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            while (leftIndex < left.Count)
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }

            while (rightIndex < right.Count)
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }

            return result;
        }
    }
    class BinaryTreeSort<TNode> : ISortAlgorithm<TNode> where TNode : IComparable<TNode>
    {
        private Node root;

        public List<TNode> Sort(List<TNode> input, Comparison<TNode> comparison)
        {
            foreach (var item in input)
                root = InsertRec(root, item);

            List<TNode> sortedList = new List<TNode>();
            InOrderTraversalRec(root, sortedList.Add);

            return sortedList;
        }

        private Node InsertRec(Node root, TNode value)
        {
            if (root == null)
                return new Node(value);

            if (value.CompareTo(root.Value) < 0)
                root.Left = InsertRec(root.Left, value);
            else if (value.CompareTo(root.Value) > 0)
                root.Right = InsertRec(root.Right, value);

            return root;
        }

        private void InOrderTraversalRec(Node root, Action<TNode> action)
        {
            if (root != null)
            {
                InOrderTraversalRec(root.Left, action);
                action(root.Value);
                InOrderTraversalRec(root.Right, action);
            }
        }

        private class Node
        {
            public TNode Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(TNode value)
            {
                Value = value;
            }
        }
    }
    class RadixSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            // Obtén la máxima longitud del nivel para determinar la cantidad de pasadas
            int maxLevelLength = GetMaxLevelLength(input);

            // Aplica el Radix Sort por cada posición en el nivel
            for (int digitPlace = 1; digitPlace <= maxLevelLength; digitPlace++)
            {
                CountingSortByLevel(input, digitPlace, comparison);
            }

            return input;
        }

        private void CountingSortByLevel(List<T> input, int digitPlace, Comparison<T> comparison)
        {
            int n = input.Count;

            // Inicializa el array de conteo
            Dictionary<int, List<T>> count = new Dictionary<int, List<T>>();

            // Llena el array de conteo
            foreach (var item in input)
            {
                int level = GetLevel(item, digitPlace);
                if (!count.ContainsKey(level))
                {
                    count[level] = new List<T>();
                }
                count[level].Add(item);
            }

            // Reconstruye el array ordenado utilizando el array de conteo
            List<T> sortedList = new List<T>();
            foreach (var kvp in count)
            {
                kvp.Value.Sort(comparison);
                sortedList.AddRange(kvp.Value);
            }

            // Actualiza la lista original con la lista ordenada
            for (int i = 0; i < n; i++)
            {
                input[i] = sortedList[i];
            }
        }

        private int GetMaxLevelLength(List<T> input)
        {
            int maxLevelLength = 0;

            foreach (var item in input)
            {
                int levelLength = GetLevelLength(item);
                if (levelLength > maxLevelLength)
                {
                    maxLevelLength = levelLength;
                }
            }

            return maxLevelLength;
        }

        private int GetLevelLength(T item)
        {
            return item.ToString().Length;
        }

        private int GetLevel(T item, int digitPlace)
        {
            int level = 0;

            if (item is Unit unit)
            {
                // Asegúrate de que la propiedad que estás utilizando existe en la clase Unit
                level = unit.AttackPower;

                // Convierte el nivel a una cadena
                string levelString = level.ToString();

                // Asegúrate de que la posición en el nivel sea válida
                if (digitPlace <= levelString.Length)
                {
                    // Obtiene el dígito en la posición específica
                    return int.Parse(levelString[levelString.Length - digitPlace].ToString());
                }
            }

            return 0;
        }
    }

    class GnomeSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public List<T> Sort(List<T> input, Comparison<T> comparison)
        {
            int n = input.Count;
            int index = 0;

            while (index < n)
            {
                if (index == 0)
                    index++;

                if (comparison(input[index], input[index - 1]) >= 0)
                    index++;
                else
                {
                    Swap(input, index, index - 1);
                    index--;
                }
            }

            return input;
        }

        private void Swap(List<T> input, int i, int j)
        {
            T temp = input[i];
            input[i] = input[j];
            input[j] = temp;
        }
    }


    class Unit : IComparable<Unit>
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int AttackPower { get; set; }
        public int Speed { get; set; }

        public int CompareTo(Unit other)
        {
            // Implementa la lógica de comparación aquí
            // Puedes comparar por nivel, velocidad, poder de ataque, etc.
            // Ejemplo: return this.Level.CompareTo(other.Level);

            // Por ejemplo, comparar por nivel y luego por velocidad si los niveles son iguales
            int levelComparison = this.Level.CompareTo(other.Level);
            if (levelComparison != 0)
            {
                return levelComparison;
            }

            return this.Speed.CompareTo(other.Speed);
        }

        public override string ToString()
        {
            return $"{Name} (Level: {Level}, Attack: {AttackPower}, Speed: {Speed})";
        }
    }
}
