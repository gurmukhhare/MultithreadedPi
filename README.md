PART 1 QUESTION ANSWERS

1.) [summary of duration for single thread and multi threaded sort on various array sizes]
| Array Size | Single-threaded       | Multi-threaded      |
| ---------- | ----------------------| --------------------|
| 10^1       | 00.00042              | 00.0101             |
| 10^2       | 00.00044              | 00.0550             |
| 10^3       | 00.00064              | 00.0341             |
| 10^4       | 00.00310              | 00.0632             |
| 10^5       | 00.02711              | 00.0514             |
| 10^6       | 00.28441              | 00.2383             |
| 10^7       | 03.09749              | 01.8600             |

2.)
I am expecting a speed up of 2x, my machine is a dual core

3.)
my observed S.F = 3.09749 / 1.8600 = 1.6653 (observed for array size of 10^7)
I did not achieve the speed up factor but I did get close. The reason why my observed S.F may be slightly less than expected is because of other background processes and applications running on my machine. This would affect the machine's ability to allocate resources required for the computation on multiple threads.

4.)
[summary of S.F vs # of threads (for sample size of 10^6)]
|# of threads | Speed-up factor    | 
| ----------- | -------------------| 
| 2           | 1.36               | 
| 3           | 1.38               |  
| 4           | 1.28               | 
| 5           | 1.31               | 
| 6           | 1.05               | 
| 7           | 0.85               |

What I noticed is that for the sample size of 10^6, the speed-up factor was actually decreasing as more and more threads were spun. It can be seen that the ideal number of threads was 2, which is actually the number of cores in my machine, and then after increasing # of threads beyond that value I saw a drop in S.F. This is most likely due to the overhead and cost that goes along with spinning more threads and running computations in parallel.

PART 2 QUESTION ANSWERS

[summary of S.F vs sample size (S.F calculated for 2 threads vs 1 thread)]
|Sample size     | Speed-up factor    | 
| ---------------| -------------------| 
| 10^3           | 1.31               | 
| 10^4           | 0.96               | 
| 10^5           | 1.17               | 
| 10^6           | 1.17               | 
| 10^7           | 1.23               | 
| 10^8           | 1.39               | 

1.)
I have learned that threading is essentially a technique of divide and conquer. Splitting up work between threads can make computations more efficient since the work is divided up and done in parallel. However, trying to do too many computations in parallel with too many threads can also worsen performance due to the extra overhead and resources required from your machine.

2.)
When designing concurrent code, it is essential to take into consideration the machine on which the code will be running since that will directly influence what the optimal amount of concurrency is. The machine's specs are what allow multithreading to be more efficient within a certain threshold and this will tend to vary between machines. It is important to optimize the number of threads and computations in parallel because although concurrency can lead to more efficient solutions, sometimes less is more. 

3.)
The output of the monte carlo method will only be as good as the inputs. Since the estimation is made based on what the randomly generated seed is, the estimation will be more accurate if the randomly generated values are more accurate/precise. From part 2, what I noticed is that I was able to get fairly accurate estimations of PI up to 3 decimal places as the number of samples was increased to 10^6. Anything below this size, my estimations were not as precise. The disadvantage of this method is that a very large number of samples will be required to get an estimation accurate to more than 3 decimal places and some machines may not be able to compute such a large value. I believe that a sample size of 10^10 or more would be required for an accuracy of 7 decimal places. Whether this is a good method or not depends on the level of accuracy required.
