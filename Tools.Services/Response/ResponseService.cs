namespace Tools.Services.Response
{
    public class ResponseService
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public static ResponseService Ok()
        {
            return new ResponseService()
            {
                IsError = false,
            };
        }

        public static ResponseService Error(string errorMessage)
        {
            return new ResponseService()
            {
                ErrorMessage = errorMessage,
                IsError = true,
            };
        }
    }

    public class ResponseService<T>
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public T Value { get; set; }

        public static ResponseService<T> Ok(T value)
        {
            return new ResponseService<T>()
            {
                Value = value,
                IsError = false,
            };
        }

        public static ResponseService<T> Error(string errorMessage)
        {
            return new ResponseService<T>()
            {
                ErrorMessage = errorMessage,
                IsError = true,
            };
        }
    }
}