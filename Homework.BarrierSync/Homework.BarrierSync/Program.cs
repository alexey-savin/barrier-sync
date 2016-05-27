using System;
using System.Text;
using System.Threading;

namespace Homework.BarrierSync
{
    class Program
    {
        static bool success = false;

        static string[] words1 = new[] { "brown", "jumped", "the", "fox", "quick" };
        static string[] words2 = new[] { "dog", "lazy", "the", "over" };
        static string solution = "the quick brown fox jumped over the lazy dog.";

        static Barrier barrier = new Barrier(2, (b) =>
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < words1.Length; i++)
            {
                sb.Append(words1[i]);
                sb.Append(" ");
            }

            for (int i = 0; i < words2.Length; i++)
            {
                sb.Append(words2[i]);

                if (i < words2.Length - 1)
                {
                    sb.Append(" ");
                }
            }
            sb.Append(".");

#if TRACE
            System.Diagnostics.Trace.WriteLine(sb.ToString());
#endif

            Console.CursorLeft = 0;
            Console.Write("Current phase: {0}", barrier.CurrentPhaseNumber);

            if (string.CompareOrdinal(solution, sb.ToString()) == 0)
            {
                success = true;
                Console.WriteLine("\r\nThe solution was found in {0} attempts", barrier.CurrentPhaseNumber);
            }
        });

        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => Solve(words1));
            Thread t2 = new Thread(() => Solve(words2));

            t1.Start();
            t2.Start();

            Console.ReadLine();
        }

        // Use Knuth-Fisher-Yates shuffle to randomly reorder each array.
        // For simplicity, we require that both wordArrays be solved in the same phase.
        // Success of right or left side only is not stored and does not count.
        static void Solve(string[] wordArray)
        {
            while (!success)
            {
                Random rnd = new Random();
                for (int i = wordArray.Length - 1; i > 0; i--)
                {
                    int swapIndex = rnd.Next(i + 1);
                    string temp = wordArray[i];
                    wordArray[i] = wordArray[swapIndex];
                    wordArray[swapIndex] = temp;
                }

                // We need to stop here to examine results
                // of all thread activity. This is done in the post-phase
                // delegate that is defined in the Barrier constructor.
                barrier.SignalAndWait();
            }
        }
    }
}
