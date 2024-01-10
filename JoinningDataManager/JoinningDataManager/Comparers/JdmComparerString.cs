using System;

namespace JoinningDataManager.Comparers
{
    public class JdmComparerString : IJdmComparer
    {
        private readonly bool _isCaseSensitive = true;
        public JdmComparerString()
        {

        }

        public JdmComparerString(bool isCaseSensitive)
        {
            _isCaseSensitive = isCaseSensitive;
        }
        public bool AreEqual(string value1, string value2)
        {
            if (value1 is null)
            {
                if (value2 is null)
                    return true;
                return false;
            }

            if (_isCaseSensitive)
                return value1.Equals(value2);

            return value1.Equals(value2, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}