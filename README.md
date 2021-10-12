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
summary of S.F vs # of threads (for sample size of 10^6)
|# of threads | Speed-up factor    | 
| ----------- | -------------------| 
| 2           | 1.36               | 
| 3           | 1.38               |  
| 4           | 1.28               | 
| 5           | 1.31               | 
| 6           | 1.05               | 
| 7           | 0.85               |

What I noticed is that for the sample size of 10^6, the speed-up factor was actually decreasing as more and more threads were spun. It can be seen that the ideal number of threads was 2, which is actually the number of cores in my machine, and then after increasing # of threads beyond that value I saw a drop in S.F. This is most likely due to the overhead and cost that goes along with spinning more threads and running computations in parallel.
