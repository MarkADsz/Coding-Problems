using System;

namespace Split_array_problem 
{
    internal class Program
    {
        /// <summary>
        /// Verifies if the elements of the lists l1 and l2 are the same, no matter the order
        /// </summary>
        /// <param name="l1">First list</param>
        /// <param name="l2">Second list</param>
        /// <returns> True if the elements of the lists are the same; False otherwise. </returns>
            private bool verifySameELems(List<int> l1, List<int> l2)
        {
            return l1.ToHashSet().SetEquals(l2);
        }

        /// <summary>
        /// Verifies if there exists a list with the same elements of l in a lists of lists.
        /// </summary>
        /// <param name="visited">List of lists</param>
        /// <param name="l">A list</param>
        /// <returns>True if list there was a list that had the same elements of l in the visited list; False otherwise</returns>
        private bool verifylistIterated(List<List<int>> visited, List<int> l)
        {
            foreach (List<int> list in visited)
            {
                if (verifySameELems(list, l) == true)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="A">Our initial input Array</param>
        /// <param name="B">First list used for separation</param>
        /// <param name="C">First list used for separation</param>
        /// <param name="visited">A list containing all lists already visited</param>
        /// <param name="iterations">The number of iterations going through the elements of A</param>
        /// <returns>True if it is possible that we find B and C such as the average value of B is equal to the average value of C; False otherwise</returns>
        public bool separate(int[] A, List<int> B, List<int> C, List<List<int>> visited, int iterations)
        {
            //At the end of iterating list A, if B and C are empty or one of them has all the elements of A, we return false 
            if ((B.Count == 0 || C.Count == 0 || B.Count==iterations || C.Count==iterations ) && iterations == A.Length )
            {
                return false;
            }

            //At the end of iterating list A, if both B and C contain some elements, we check if their averages are the same
            if ((B.Count !=0 && C.Count !=0) && iterations == A.Length)
            {
                if (B.Average() == C.Average())
                    return true;
                else
                    return false;
            } 

            //We generate two auxiliary lists for B and C, in which we will add the next element of a (depending where we are in the iteration)
            List<int> auxB = new List<int>(B);
            auxB.Add(A[iterations]);
            List<int> auxC = new List<int>(C);
            auxC.Add(A[iterations]);

            //We verify if the previously generated lists have already been found before in our visited list (that their elements are equal with thos of a previous list)
            bool verifyAuxB = verifylistIterated(visited, auxB);
            bool verifyAuxC = verifylistIterated(visited, auxC);

            //If the lists containing the new value have been found in our visited lists, we are sure that our solution cannot be this one
            if (verifyAuxB == true && verifyAuxC == true)
            {
                return false;
            }
            //If list C couldn't be found in the visited list, we add it, and then we call our function for B and our C that contains the new element from A
            else if (verifyAuxB == true && verifyAuxC == false)
            {
                visited.Add(auxC);
                return separate(A, B, auxC,visited, iterations + 1);
            }
            //If list B couldn't be found in the visited list, we add it, and then we call our function for our B that contains the new element from A and C
            else if (verifyAuxB == false && verifyAuxC == true)
            {
                visited.Add(auxB);
                return separate(A, auxB, C, visited, iterations + 1);
            }
            else
            //If neither of our lists have been visited, we call our function for both lists containing the new element
            {
                return separate(A, auxB, C,visited, iterations + 1) || separate(A, B, auxC,visited, iterations + 1);
            }

        }
        static void Main(string[] args)
        {
            Program prog = new Program();

            //We initalize our array and lists
            int [] A = new int[30];
            List<int> B = new List<int>();
            List<int> C = new List<int>();
            List<List<int>> visited = new List<List<int>>();

            //We read the values from the console
            A = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32);
           

            int iterations = 0;//we iterate from 0 to A.len
            Console.WriteLine(prog.separate(A,B,C,visited,iterations));


        }
    }
}