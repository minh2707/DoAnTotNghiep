using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class IndexController : Controller
    {
        // GET api/index
        [HttpGet]
        public String Get()
        {
            return "Server chạy thành công";
        }
    }
}