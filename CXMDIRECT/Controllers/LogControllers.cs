using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Models;

namespace CXMDIRECT.Controllers
{
    internal class LogControllers : LogControllerAbstractClass
    {
        internal override ExceptionLog Add(System.Exception exception)
        {
            if(exception.GetType() == typeof(SecureException))
            {
                
            }


            throw new NotImplementedException();
        }
    }
}
