using System;

namespace StageOne
{
    sealed class UnrecoverableError
    {
        public static void ErrorReadFale(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }
}
