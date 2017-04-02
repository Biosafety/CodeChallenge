using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    public class HomeController : Controller
    {
        protected StackOverflowService stackService;

        public HomeController()
        {
            stackService = new StackOverflowService();
        }

        public ActionResult IndexAsync()
        {           
            return View();
        }

        [Route("questions")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult> CommentsAsync()
        {
            StackOverflow stackQs = await stackService.GetQuestionsAsync(1);
            return Json(stackQs);
        }
    }
}