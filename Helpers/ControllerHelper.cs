using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CSMapi.Helpers
{
    [Route("")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;

        protected BaseApiController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;

        protected BaseService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
