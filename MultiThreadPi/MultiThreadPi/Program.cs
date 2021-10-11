using System;
using System.Threading;
using System.Diagnostics;


namespace MultiThreadPi
{
    class MainClass
    {

        static void Main(string[] args)
        {
            int numThreads = Environment.ProcessorCount; //variable to adjust number of threads to generate samples on
            long numberOfSamples = 10000000; //total number of samples to be divided into specified number of threads
            long numberOfSamplesPerThread = numberOfSamples / numThreads;
            long hits=0;

            Stopwatch myWatch = new Stopwatch();
            Thread[] threads = new Thread[numThreads]; //to keep track of all the threads that are being spun
            myWatch.Start();
            //spin 'numThreads' threads and pass in EstimatePI method to each thread
            for (int i = 0; i < numThreads; i++)
            {
                int j = i;
                threads[j] = new Thread(() => EstimatePI(numberOfSamplesPerThread, ref hits));
                threads[j].Start();
            }

            //join all the threads that were spun to ensure we wait for each to finish generating samples and counting hits
            for (int i = 0; i < numThreads; i++)
            {
                int k = i;
                threads[k].Join();
            }

            //the pi value can be estimated by finding the ratio of hits to total number of samples and multiplying by 4
            double pi = 4*((double)hits / (double)numberOfSamples);
            myWatch.Stop();
            TimeSpan calculatePItime = myWatch.Elapsed;
            Console.WriteLine(pi);
            Console.WriteLine(calculatePItime);
            return;


        }
        static void EstimatePI(long numberOfSamples, ref long hits)
        {
            //jagged array to store all the generated coordinates after a function call to GenerateSamples 
            double[][] samples = GenerateSamples(numberOfSamples);

            //iterate through all coordinates generated and determine if distance is less than or equal to 1. If so, increment hits
            for (int i = 0; i < numberOfSamples; i++)
            {
                double x = samples[i][0];
                double y = samples[i][1];
                if (Math.Sqrt(x * x + y * y) <= 1)
                {
                    Interlocked.Increment(ref hits);
                }
            }
            return;
        }

        static double[][] GenerateSamples(long numberOfSamples)
        {
            //defining the range of our x and y coordinates
            double min = -1.00;
            double max = 1.00;

            Random rand = new Random();
            double[][] samples = new double[numberOfSamples][];

            //generate the samples using the random generator
            for (int i = 0; i < numberOfSamples; i++)
            {
                double x = rand.NextDouble() * (max - min) + min;
                double y = rand.NextDouble() * (max - min) + min;
                samples[i] = new double[] { x, y };
            }
            return samples;
        }
    }
}

//ALGORITHM:
// 1) In main, set the size of our samples and the number of threads we'd like to use. Each thread will generate 'numberOfSamplesPerThread' samples
// 2) use for loops to start and Join the threads. Each thread will run the 'EstimatePI' method
// 2) use the generateSamples method strictly to generate the samples. An x and y coordinate, each between -1 and 1, will be generated 
// 3) return the samples back to the EstimatePI method. Iterate through each coordinate and calculate distance. If distance <= 1, we increment 'hits'
// 4) calculate the result in the main thread for PI. It will be 4*(hits/numerOfSamples)

// NOTE: when we increment our 'hits', multiple threads will be trying to increment it. Make sure to use mutual exclusion