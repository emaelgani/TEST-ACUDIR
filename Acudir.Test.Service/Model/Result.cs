namespace Acudir.Test.Service.Model
{
    namespace Application.Common.Models
    {
        public class Result
        {
            public Result() { }
            internal Result(bool succeeded, List<string> errors, List<string> messages)
            {
                Succeeded = succeeded;
                Errors = errors;
                Messages = messages;
            }

            public bool Succeeded { get; set; }

            public List<string> Errors { get; set; }

            public List<string> Messages { get; set; }

            public static Result Success()
            {
                return new Result(true, new List<string>(), new List<string>());
            }

            public static Result Success(string message)
            {
                List<string> messages = new List<string>();
                messages.Add(message);
                return new Result(true, new List<string>(), messages);
            }

            public static Result Success(List<string> messages)
            {
                return new Result(true, new List<string>(), messages);
            }

            public static Result Failure(List<string> errors)
            {
                return new Result(false, errors, new List<string>());
            }

            public static Result Failure(string error)
            {
                List<string> errors = new List<string>();
                errors.Add(error);
                return new Result(false, errors, new List<string>());
            }

            public static Result ExceptionFailure(Exception ex)
            {
                if (ex is null)
                {
                    return Result.Failure("No exception description was found.");
                }

                if (ex!.InnerException != null && ex.InnerException.Message != null)
                {
                    return Result.Failure(ex.Message + Environment.NewLine + ex.InnerException.Message);
                }

                return Result.Failure(ex.Message);
            }
        }
    }

}
