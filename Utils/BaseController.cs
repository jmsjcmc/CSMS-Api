using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace csms_backend.Utils
{
    [Route("")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly Context _context;
        protected BaseController(Context context)
        {
            _context = context;
        }
        protected ActionResult HandleException(Exception e)
        {
            return new ObjectResult(e.InnerException?.Message ?? e.Message)
            {
                StatusCode = 500
            };
        }
        protected ActionResult HandleLoginException(Exception e)
        {
            return new ObjectResult(e.InnerException?.Message ?? e.Message)
            {
                StatusCode = 401
            };
        }
    }

    public abstract class BaseService
    {
        protected readonly Context _context;
        protected readonly IMapper _mapper;
        protected BaseService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
