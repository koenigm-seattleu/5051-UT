using System;
using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// Get Random Number Function helper functions
    /// </summary>
    public class RandomHelper
    {
        private static volatile RandomHelper instance;
        private static readonly object syncRoot = new Object();

        private RandomHelper() { }

        public static RandomHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new RandomHelper();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The instance of Random to use
        /// </summary>
        private static Random randObj;

        // The forced random number to use
        private static int ForcedRandomNumber;

        // track if set is on or off
        public static bool isSetForcedNumber;

        /// <summary>
        /// Init  and set default values.
        /// </summary>
        public void Initialize()
        {
            // Init the rand just onece.
            randObj = new Random();

            // Default Number 
            SetForcedNumber(-1);

            //Turn off forced number
            isSetForcedNumber = EnableForcedNumber(false);  
        }

        /// <summary>
        /// Sets the forced Number to return
        /// </summary>
        /// <param name="number"></param>
        public void SetForcedNumber(int number)
        {
            // generate random number
            ForcedRandomNumber = number;
            isSetForcedNumber = true;
        }

        /// <summary>
        /// Sets the Forced Number to True or false
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool EnableForcedNumber(bool state)
        {
            isSetForcedNumber = state;
            return state;
        }

        /// <summary>
        /// Get Random Number will return a number between 0 and Max
        /// If ForcedRandomNumber is true, it will return the forced number.
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetRandomNumber(int max)
        {
            int number;

            if (isSetForcedNumber)
            {
                return ForcedRandomNumber;
            }

            number = randObj.Next(0, max);
            return number;
        }

    }
}



