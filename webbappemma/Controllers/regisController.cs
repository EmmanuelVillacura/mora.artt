using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using webbappemma.Models;
namespace WebApplicationMVC.Controllers
{
    public class regisController : Controller
    {
        private readonly AppDbContext _context;

        public regisController(AppDbContext context)
        {
            _context = context;
        }

        // inicio registro login

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Registro(RegistroViewModel Rvm)
        {
            {
               


                if (ModelState.IsValid)
            {
        
                var usuarioExistente = _context.TblUsuario.FirstOrDefault(u => u.Username == Rvm.Username);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("", "El nombre de usuario ya está en uso.");
                    return View(Rvm);
                }

    
                Usuario nuevoUsuario = new Usuario
                {
                    Name = Rvm.Name,
                    Email = Rvm.Email,
                    Username = Rvm.Username,
                    Rol = Rvm.Rol
                };



 
                CreatePasswordHash(Rvm.Password, out byte[] passwordHash, out byte[] passwordSalt);
                nuevoUsuario.PasswordHash = passwordHash;
                nuevoUsuario.PasswordSalt = passwordSalt;


                _context.TblUsuario.Add(nuevoUsuario);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(Rvm);
        }

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }


}
}


}

// fin registro login