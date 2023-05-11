using System;
using System.Collections.Generic;

namespace MonteNegRo.Common
{
    public static class ExceptionHandler
    {
        public static List<string> PackErrorsToList(Exception ex)
        {
            var errors = new List<string>
                {
                    ex.Message
                };
            if (ex.InnerException != null)
            {
                ex = ex.InnerException;
                errors.Add(ex.Message);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    errors.Add(ex.Message);
                }
            }
            return errors;
        }
    }
}
