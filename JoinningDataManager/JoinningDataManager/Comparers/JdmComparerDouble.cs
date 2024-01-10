using System;

namespace JoinningDataManager.Comparers
{
    public class JdmComparerDouble : IJdmComparer
    {
        private readonly double _maxError;

        public JdmComparerDouble(double maxError)
        {
            _maxError = maxError;
        }

        public bool AreEqual(string value1, string value2)
        {
            if (string.IsNullOrEmpty(value1))
            {
                if (string.IsNullOrEmpty(value2))
                    return true;
                return false;
            }

            if (!double.TryParse(value1, out var value1Double))
                throw new ArgumentOutOfRangeException(nameof(value1));


            if (!double.TryParse(value1, out var value2Double))
                throw new ArgumentOutOfRangeException(nameof(value2));


            return value1Double.EqualWithTolerance(value2Double, _maxError);
        }
    }
}