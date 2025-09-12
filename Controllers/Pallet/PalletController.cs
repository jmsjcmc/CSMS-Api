using AutoMapper;
using csms_backend.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csms_backend.Controllers
{
    public class PalletController : BaseController
    {
        public PalletController(Context context, IMapper mapper) : base(context)
        {

        }
    }
}
