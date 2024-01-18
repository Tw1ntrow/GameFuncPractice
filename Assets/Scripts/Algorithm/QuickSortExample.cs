using System;

// コンソールでうごく
// 計算量はO(n log n)
class QuickSortExample
{
    public void QuickSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high);
            QuickSort(array, low, pivotIndex - 1);  // 左側をソート
            QuickSort(array, pivotIndex + 1, high); // 右側をソート
        }
    }

    private int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }
        Swap(array, i + 1, high);
        return i + 1;
    }

    private void Swap(int[] array, int index1, int index2)
    {
        int temp = array[index1];
        array[index1] = array[index2];
        array[index2] = temp;
    }

    public void Main()
    {
        // 例
        int[] array = { 10, 7, 8, 9, 1, 5 };
        int n = array.Length;
        QuickSort(array, 0, n - 1);
        Console.WriteLine("Sorted array: ");
        foreach (int item in array)
        {
            Console.Write(item + " ");
        }
    }
}