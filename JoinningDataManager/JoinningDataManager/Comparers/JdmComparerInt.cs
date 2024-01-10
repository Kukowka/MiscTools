using System;

namespace JoinningDataManager.Comparers
{
    public class JdmComparerInt : IJdmComparer
    {
        public bool AreEqual(string value1, string value2)
        {
            if (value1 is null)
            {
                if (value2 is null)
                    return true;
                return false;
            }

            if (!int.TryParse(value1, out var value1Int))
                throw new ArgumentOutOfRangeException(nameof(value1));


            if (!int.TryParse(value1, out var value2Int))
                throw new ArgumentOutOfRangeException(nameof(value2));

            return value1Int == value2Int;
        }
    }
}