using System;

class BinarySearchExample
{
    // �z�񂪃\�[�g����Ă���O��
    // �R���\�[���œ���
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

        return -1; // �v�f��������Ȃ�����
    }

    void Main()
    {
        // ��
        int[] array = { 2, 3, 4, 10, 40 };
        int target = 10;
        int result = BinarySearch(array, target);

        if (result == -1)
            Console.WriteLine("Element not present");
        else
            Console.WriteLine("Element found at index " + result);
    }
}