namespace kin_kinit_mocker.Model
{
    public static class ModelExtensions
    {
        public static bool IsNullOrBlank(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
