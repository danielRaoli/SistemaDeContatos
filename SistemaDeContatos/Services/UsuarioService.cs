using SistemaDeContatos.Controllers;
using SistemaDeContatos.Data;
using SistemaDeContatos.Helper;
using SistemaDeContatos.Models.Entities;
using SistemaDeContatos.Models.ViewModels;

namespace SistemaDeContatos.Services
{
    public class UsuarioService
    {

        private readonly SistemaDeContatosContext _context;

        public UsuarioService(SistemaDeContatosContext context)
        {
            _context = context;
        }

        public List<Usuario> FindAll()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario FindById(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);

        }


        public void AddUsuario(Usuario usuario)
        {
            usuario.SetSenha();
            usuario.DataCriacao = DateTime.Now;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            bool hasAny = _context.Usuarios.Any(x => x.Id == usuario.Id);

            if (!hasAny)
            {
                throw new Exception("Id Not Found");
            }
            var usuarioDb = _context.Usuarios.FirstOrDefault(x => x.Id == usuario.Id);

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Email = usuario.Email;
            usuarioDb.DataAtualizacao = DateTime.Now;
            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();
        }

        public void DeletUsuario(Usuario usuario)
        {
            bool hasAny = _context.Usuarios.Any(x => x.Id == usuario.Id);
            if (!hasAny)
            {
                throw new Exception("Id not found");
            }
            _context.Remove(usuario);
            _context.SaveChanges();
        }

        public Usuario FindByLogin(string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login.Equals(login));
        }

        public Usuario FindByLoginEmail(string login, string email)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login.Equals(login) && x.Email.Equals(email));
        }

        public Usuario AlterarSenha(AlterSenhaViewModel alterSenhaView)
        {
            var usuarioDb = _context.Usuarios.FirstOrDefault(x => x.Id == alterSenhaView.Id);
            if (usuarioDb == null)
            {
                throw new Exception("Erro ao tentar encontrar usuário");
            }
            if (!usuarioDb.ValidaSenha(alterSenhaView.SenhaAtual)) throw new Exception("Senha Atual não confere");
            if (usuarioDb.ValidaSenha(alterSenhaView.NovaSenha)) throw new Exception("A senha atual não pode ser igual a passada");

            usuarioDb.SetNovaSenha(alterSenhaView.NovaSenha);
            usuarioDb.DataAtualizacao = DateTime.Now;
            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();
            return usuarioDb;

        }
    }
}

