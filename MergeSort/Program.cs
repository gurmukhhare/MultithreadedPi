using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {

            int ARRAY_SIZE = 1000000;
            int[] arraySingleThread = new int[ARRAY_SIZE];

            //using 'Random' class to generate values for the array and placing those randomly generated numbers
            Random rand = new Random();
            for(int i = 0; i < ARRAY_SIZE; i++)
            {
                arraySingleThread[i] = rand.Next(0,1000); //generating random number between 0 and 999
            }

            // copy array by value.. You can also use array.copy()
            int[] arrayMultiThreadnew = new int[ARRAY_SIZE];
            Array.Copy(arraySingleThread, arrayMultiThreadnew, ARRAY_SIZE);


            /*TODO : Use the  "Stopwatch" class to measure the duration of time that
               it takes to sort an array using one-thread merge sort and
               multi-thead merge sort
            */
            Stopwatch myWatch = new Stopwatch();

            ////TODO :start the stopwatch
            myWatch.Start();
            MergeSort(arraySingleThread);
            myWatch.Stop();
            TimeSpan singleThreadTime = myWatch.Elapsed;
            ////TODO :Stop the stopwatch


            ////TODO: Multi Threading Merge Sort
            ///
            ///ALGORITHM:
            //1) call method to split main unsorted array into 'N' subarrays. Store these subarrays in a separate array so we can iterate through and sort each
            //2) loop through N subarrays and spin a new thread for each subarray
            //3) in each new thread, send in the mergesort method as a param to sort the subarray
            //4) we must wait for all threads to execute functionality using Join()
            //5) merge N subarrays into one larger array and check if it is sorted -->  you have a queue of pairs of arrays that need to be merged, with each merged array also generating a new array that needs to be entered into the queue.


            Stopwatch myWatchMultithreaded = new Stopwatch();
            myWatchMultithreaded.Start();

            int THREAD_COUNT = 2; //number of threads to be spun, one for each subarray
            int[][] subarrays = GenerateSubarrays(THREAD_COUNT, arrayMultiThreadnew);
            Thread[] threads = new Thread[THREAD_COUNT]; //store references to all the threads we are spinning, will reference these using indexing

            for(int i = 0; i<THREAD_COUNT; i++) //spin 'THREAD_COUNT' threads and sort a subarray on each of these threads
            {
                int localIndex = i;
                threads[localIndex] = new Thread(() => MergeSort(subarrays[localIndex]));
                threads[localIndex].Start();
            }

            for(int i = 0; i < THREAD_COUNT; i++) //must Join each thread we spun so that we wait for all threads to finish sorting each subarray before merging
            {
                int localIndex = i;
                threads[localIndex].Join();
            }

            //initializing a queue with all the sorted subarrays
            Queue<int[]> queue = new Queue<int[]>(subarrays);

            //keep popping pairs of sorted arrays from the queue, merging them, and pushing the result back into queue until only one fully sorted and merged array remains
            while(queue.Count > 1)
            {
                int[] A = queue.Dequeue();
                int[] B = queue.Dequeue();
                int[] result = new int[A.Length + B.Length];

                //merge arrays A and B and push the result back onto queue to be merged with another sorted subarray
                Merge(A, B, result);
                queue.Enqueue(result);
            }

            int[] sortedResult = queue.Dequeue(); //the final resulting sorted array using multithreading

            myWatchMultithreaded.Stop();
            TimeSpan multiThreadTime = myWatchMultithreaded.Elapsed;

            bool isSortedSingle = IsSorted(arraySingleThread);
            bool isSortedMulti = IsSorted(sortedResult);
            Console.WriteLine(isSortedSingle);
            Console.WriteLine(isSortedMulti);
            Console.WriteLine(singleThreadTime);
            Console.WriteLine(multiThreadTime);


            /*Helper method to split original unsorted array into specified number of subarrays
             * */
            static int[][] GenerateSubarrays(int numThreads, int[] originalArray)
            {
                int[][] subarrays = new int[numThreads][];
                int remainingLength = originalArray.Length; //keep track of how many elements in original array need to be placed
                int remainingThreads = numThreads; // numThreads == number of subarrays we require

                int i = 0; //to track the index of the original array
                int k = 0; //to track index of the new 'subarrays' jagged array
                while(remainingThreads > 0) //keep generating new subarrays until we have created a subarray for each thread
                {
                    int currSize = remainingLength / remainingThreads; //calculate how many elements should be placed in current subarray
                    subarrays[k] = new int[currSize];
                    for (int j = 0; j < currSize; j++) //iterate 'currSize' times to place 'currSize' items into subarray
                    {
                        subarrays[k][j] = originalArray[i];
                        i++; //increment i so that the next element in the original array gets placed
                    }
                    k += 1; //increment k to set values for the next subarray in the next iteration
                    remainingLength -= currSize;
                    remainingThreads -= 1;
                }

                return subarrays;
            }
            /*********************** Methods **********************
             *****************************************************/
            /*
            implement Merge method. This method takes two sorted array and
            and constructs a sorted array in the size of combined arrays
            */
            static int[] Merge(int[] LA, int[] RA, int[] A)
            {

                int leftLength = LA.Length;
                int rightLength = RA.Length;

                int i = 0; //track index for left array
                int j = 0; //track index for right array
                int k = 0; //track index for array 'A' that will hold the resulting sorted elements

                //looping through left and right arrays until one or both are out of bounds
                while (i < leftLength && j < rightLength)
                {
                    if(LA[i] < RA[j])
                    {
                        A[k] = LA[i];
                        i++;
                    }
                    else
                    {
                        A[k] = RA[j];
                        j++;
                    }
                    k++;
                }

                //if either array still has items left to place in results array, loop through and place those elements
                while(i < leftLength)
                {
                    A[k] = LA[i];
                    i++;
                    k++;
                }

                while(j < rightLength)
                {
                    A[k] = RA[j];
                    j++;
                    k++;
                }

                //return the result, which is the two arrays merged and sorted in ascending order
                return A; 
               

            }


            // /*
            // implement MergeSort method: takes an integer array by reference
            // and makes some recursive calls to intself and then sorts the array
            // */
            static int[] MergeSort(int[] A)
            {
                //base case for the recursion
                if (A.Length <= 1)
                {
                    return A;
                }

                //find midpoint of array to split original array into 2 halves
                int length = A.Length;
                int mid = length / 2;
                int[] leftHalf = new int[mid];
                int[] rightHalf = new int[length-mid];

                //loop through the first 'mid' items in original array and store them into leftHalf array
                for(int i = 0; i < mid; i++)
                {
                    leftHalf[i] = A[i];

                }

                //store remaining elements of original array into rightHalf array
                int k = 0;
                for (int j=mid; j < length; j++)
                {
                    rightHalf[k] = A[j];
                    k++;
                }

                //sort each half with a recursive call 
                int[] left = MergeSort(leftHalf);
                int[] right = MergeSort(rightHalf);

                //once both halves recursively sorted, merge the two halves into one array with correct ascending order
                return Merge(left, right, A);


            }


            // a helper function to print your array
            static void PrintArray(int[] myArray)
            {
                Console.Write("[");
                for (int i = 0; i < myArray.Length; i++)
                {
                    Console.Write("{0} ", myArray[i]);

                }
                Console.Write("]");
                Console.WriteLine();

            }

            // a helper function to confirm your array is sorted
            // returns boolean True if the array is sorted
            static bool IsSorted(int[] a)
            {
                int j = a.Length - 1;
                if (j < 1) return true;
                int ai = a[0], i = 1;
                while (i <= j && ai <= (ai = a[i])) i++;
                return i > j;
            }


        }

       

    }
}
