using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exception)
        {
            //if (exception.Exception is NotImplementedException)
            //{
            //    // Extra Logic
            //}
            //else if (exception.Exception is ArgumentOutOfRangeException)
            //{
            //    // Extra Logic
            //}
            if (!exception.ExceptionHandled)
            {
                exception.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary()
                    {
                        { "ErrorMessage", exception.Exception.Message } // Pass the error message to the view
                    }
                };
                exception.ExceptionHandled = true;
            }
        }

    }
}
