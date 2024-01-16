using System;

namespace JoinningDataManager.Comparers
{
    public class JdmComparerString : IJdmComparer
    {
        private readonly bool _isCaseSensitive = false;
        public JdmComparerString()
        {

        }

        public JdmComparerString(bool isCaseSensitive)
        {
            _isCaseSensitive = isCaseSensitive;
        }
        public bool AreEqual(string value1, string value2)
        {
            if (string.IsNullOrEmpty(value1))
            {
                if (string.IsNullOrEmpty(value2))
                    return true;
                return false;
            }

            if (_isCaseSensitive)
                return value1.Equals(value2);

            return value1.Equals(value2, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}