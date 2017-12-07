using System.Web.Http;
using ClassLibrary;

namespace WebAPI.Controllers
{
    public abstract class BaseController : ApiController
    {
        public readonly IDataLayerAccess DataLayerAccess;

        public BaseController(IDataLayerAccess dataLayerAccess)
        {
            this.DataLayerAccess = dataLayerAccess;
        }
    }
}