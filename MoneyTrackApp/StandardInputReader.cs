
using System;
using System.Collections;
using System.Collections.Generic;

namespace MoneyTrackApp
{
    /// <summary>
    /// Description of StandardInputReader.
    /// </summary>
    public class StandardInputReader : IEnumerable<string>
    {
        private StandardInputReader()
        {
        }

        public static StandardInputReader EndWithBlankLine()
        {
            return new StandardInputReader();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<string> GetEnumerator()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    yield break;
                }

                yield return input;
            }
        }
    }
}
