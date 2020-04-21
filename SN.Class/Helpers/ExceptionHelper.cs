using System;

namespace SN.Class.Helpers
{
    public class ExceptionHelper
    {
        public static Exception CatchException(Action action)
        {
            try
            {
                action.Invoke();
                return null;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

    }
}
