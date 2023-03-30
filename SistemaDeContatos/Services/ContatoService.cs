using SistemaDeContatos.Data;
using SistemaDeContatos.Models.Entities;

namespace SistemaDeContatos.Services
{
    public class ContatoService
    {
        private readonly SistemaDeContatosContext _context;

        public ContatoService(SistemaDeContatosContext context)
        {
            _context = context;
        }

        public List<Contato> FindAll(int usuarioId)
        {
           return  _context.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }
        public Contato FindById(int id)
        {
           return _context.Contatos.FirstOrDefault(x => x.Id == id);

        }


        public void AddContato(Contato contato)
        {
            try
            {
                if (contato == null) throw new Exception("nem um dado encontrado para criação do contato");
                _context.Contatos.Add(contato);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void Update(Contato contato)
        {
            bool hasAny = _context.Contatos.Any(x => x.Id == contato.Id);
            if(!hasAny)
            {
                throw new Exception("Id Not Found");
            }
            _context.Update(contato);
            _context.SaveChanges();
        }

        public void DeletContato(Contato contato)
        {
            bool hasAny = _context.Contatos.Any(x => x.Id == contato.Id);
            if (!hasAny)
            {
                throw new Exception("Id not found");
            }
            _context.Remove(contato);
            _context.SaveChanges();
        }
    }
}
