using System;

class BinarySearchExample
{
    // 配列がソートされている前提
    // コンソールで動く
    public int BinarySearch(int[] array, int target)
    {
        int low = 0;
        int high = array.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;

            if (array[mid] == target)
            {
                return mid; 
            }
            else if (array[mid] < target)
            {
                low = mid + 1; 
            }
            else
            {
                high = mid - 1;
            }
        }

        return -1; // 要素が見つからなかった
    }

    void Main()
    {
        // 例
        int[] array = { 2, 3, 4, 10, 40 };
        int target = 10;
        int result = BinarySearch(array, target);

        if (result == -1)
            Console.WriteLine("Element not present");
        else
            Console.WriteLine("Element found at index " + result);
    }
}