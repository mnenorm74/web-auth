using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhotosApp.Controllers
{
    public class PassportController : Controller
    {
        // NOTE: Неаутентифицированный пользователь будет отправляться на вход в DefaultChallengeScheme,
        // а затем возвращаться сюда и отсюда перенаправляться на главную страницу.
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
                return Challenge();

            return Redirect("/");
        }

        [Authorize]
        // NOTE: Сначала происходит выход из приложения, а затем на сервере авторизации
        public IActionResult Logout()
        {
            return SignOut("Cookie", "Passport");
        }
    }
}
