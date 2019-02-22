namespace AddMinion
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            string[] minionData = Console.ReadLine().Split();
            string[] vallainName = Console.ReadLine().Split();

            string minionName = minionData[1];
            int minionAge = int.Parse(minionData[2]);
            string townName = minionData[3];


        }
    }
}
