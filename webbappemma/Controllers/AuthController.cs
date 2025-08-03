using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using webbappemma.Models;

namespace WebApplicationMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LoginIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginIn(LoginViewModel Lvm)
        {
            var usuarios = _context.TblUsuario.ToList();
            if (usuarios.Count == 0)
            {
                Usuario U = new Usuario();
                U.Name = "Administrador";
                U.Email = "admin@academia.cl";
                U.Username = "admin";
                U.Rol = "Administrador";

                CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);

                U.PasswordHash = passwordHash;
                U.PasswordSalt = passwordSalt;
                _context.TblUsuario.Add(U);
                _context.SaveChanges();
            }

            var us = _context.TblUsuario.Where(u => u.Username.Equals(Lvm.Username)).FirstOrDefault();
            if (us != null)
            {
                if (VerificarPass(Lvm.Password, us.PasswordHash, us.PasswordSalt))
                {
                    var Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, us.Name),
                        new Claim(ClaimTypes.NameIdentifier, Lvm.Username),
                        new Claim(ClaimTypes.Role, us.Rol)
                    };

                    var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties { IsPersistent = true }
                    );

                    return RedirectToAction("Index", "Alumno");
                }
                else
                {
                    ModelState.AddModelError("", "Contraseña Incorrecta");
                    return View(Lvm);
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuario no Encontrado!");
                return View(Lvm);
            }
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Las contraseñas no coinciden.");
                    return View(model);
                }

                if (_context.TblUsuario.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "El nombre de usuario ya existe.");
                    return View(model);
                }

                CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                Usuario nuevoUsuario = new Usuario
                {
                    Name = model.Name,
                    Email = model.Email,
                    Username = model.Username,
                    Rol = model.Rol,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _context.TblUsuario.Add(nuevoUsuario);
                _context.SaveChanges();

                return RedirectToAction("LoginIn");
            }

            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(LoginIn));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerificarPass(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var pass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return pass.SequenceEqual(passwordHash);
            }
        }
    }
}