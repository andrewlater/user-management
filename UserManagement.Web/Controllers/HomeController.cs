using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Web.Models;
using Paramore.Darker;
using UserManagement.Core.Queries;
using UserManagement.Core.Models;
using Paramore.Brighter;
using UserManagement.Core.Commands;

namespace UserManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAmACommandProcessor _commandProcessor;

        public HomeController(IQueryProcessor queryProcessor, IAmACommandProcessor commandProcessor)
        {
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
        }

        public async Task<ViewResult> Index()
        {
            var result = await _queryProcessor.ExecuteAsync(new GetUsersQuery());

            return View(result.Users);
        }

        public async Task<ViewResult> Details(int id)
        {
            var result = await _queryProcessor.ExecuteAsync(new GetUserQuery(id));

            return View(new UserViewModel { User = result.User, Roles = result.Roles });
        }

        [HttpGet]
        public async Task<ViewResult> Edit(int id)
        {
            var result = await _queryProcessor.ExecuteAsync(new GetUserQuery(id));

            return View(new UserViewModel { User = result.User, Roles = result.Roles });
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Edit(User user)
        {
            await _commandProcessor.SendAsync(new SaveUserCommand(user));

            return RedirectToAction("Edit", new { id = user.UserId });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
