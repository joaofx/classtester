/********************************************************************************
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 ********************************************************************************/
using System;

namespace TheJoyOfCode.QualityTools
{
    public class ConstructorTestException : Exception
    {
        public ConstructorTestException()
        {
        }

        public ConstructorTestException(string message)
            : base(message)
        {
        }

        public ConstructorTestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}