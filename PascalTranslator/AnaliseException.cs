using System;

namespace PascalTranslator
{
    class AnaliseException : Exception
    {
        public AnaliseException(string expected, string was) : base($"Ожидалось: \"{expected}\" а встретилось \"{was}\"") { }
        public AnaliseException(string message) : base(message) { }
    }
}
