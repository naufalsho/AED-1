namespace Core.Helpers
{
    public static class ResponseStatusCode
    {
        public static string Success => "200";
        public static string NoContent => "204";
        public static string BadRequest => "400";
        public static string Unauthorized => "401";
        public static string PageNotFound => "404";
        public static string InternalServerError => "500";
    }

    public class ResponseHelper
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }

        public static ResponseHelper CreateFailResult(string response)
        {
            var ret = new ResponseHelper()
            {
                StatusCode = response.Substring(0, 3)
            };

            if (response.Length > 3)
            {
                ret.Message = response.Substring(4, response.Length - 4);
            }

            return ret;
        }
    }
}
