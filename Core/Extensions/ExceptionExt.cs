namespace Core.Extensions
{
    public static class ExceptionExt
    {
        public static string GetMessage(this Exception ex)
        {
            var ret = ex.Message;

            if (ex.InnerException != null)
            {
                if (!string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    ret = ex.InnerException.Message;
                }
            }

            return ret;
        }
    }
}
