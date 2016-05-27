# barrier-sync

https://msdn.microsoft.com/en-us/library/dd537615(v=vs.110).aspx

The purpose of the following program is to count how many iterations (or phases) are required 
for two threads to each find their half of the solution on the same phase by using a randomizing algorithm to reshuffle the words. 
After each thread has shuffled its words, the barrier post-phase operation 
compares the two results to see if the complete sentence has been rendered in correct word order.

A Barrier is an object that prevents individual tasks in a parallel operation from continuing until all tasks reach the barrier. 
It is useful when a parallel operation occurs in phases, and each phase requires synchronization between tasks. 
In this example, there are two phases to the operation. 
In the first phase, each task fills its section of the buffer with data. 
When each task finishes filling its section, the task signals the barrier that it is ready to continue, and then waits. 
When all tasks have signaled the barrier, they are unblocked and the second phase starts. 
The barrier is necessary because the second phase requires that each task have access to all the data that has been generated to this point. 
Without the barrier, the first tasks to complete might try to read from buffers that have not been filled in yet by other tasks. 
You can synchronize any number of phases in this manner.