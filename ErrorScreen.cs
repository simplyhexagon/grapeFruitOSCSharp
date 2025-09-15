using System;

namespace grapeFruitRebuild
{
    public class ErrorScreen
    {
        //This is for when an exception happens that can be caught
        //Real bugchecks are handled by Cosmos, those cannot be overwritten
        public static void GeneralError()
        {
            Logger.Log(4, "General Critical Failure. \nSystem integrity might be compromised. Press a key to continue");
            Console.ReadKey();
        }

        public static void SpecifiedError(Exception e)
        {
            Console.WriteLine();
            Logger.Log(4, "Critical Stop");
            Console.WriteLine("A critical stop was triggered during execution!");
            Console.WriteLine(e.ToString());

            Logger.Debug("Critical stop: " + e.ToString());

            Console.WriteLine("\nSystem integrity might be compromised.\nPress a key to continue");
            Console.ReadKey();
        }

        public static void CustomError(string errormessage)
        {
            Logger.Log(4, errormessage + "\nSystem integrity might be compromised. Press a key to continue");
            Console.ReadKey();
        }
    }
}
