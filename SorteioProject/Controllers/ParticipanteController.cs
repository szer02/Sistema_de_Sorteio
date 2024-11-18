using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Repositorio;
using Models.Repositorio.Entidades;
using Models.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly IParticipanteRepositorio _participanteRepositorio;

        public ParticipanteController(IParticipanteRepositorio participanteRepositorio)
        {
            _participanteRepositorio = participanteRepositorio;
        }

        // Lista todos os participantes
        public IActionResult Index()
        {
            return View();
        }

        // Método para fazer o upload de um arquivo CSV
        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var dados = line.Split(',');

                        // Validação do nome do participante
                        if (dados.Length > 0 && !string.IsNullOrWhiteSpace(dados[0]))
                        {
                            var participante = new Participante { Nome = dados[0] };
                            _participanteRepositorio.Adicionar(participante);
                        }
                    }
                }
                TempData["Mensagem"] = "Participantes carregados com sucesso!"; // Mensagem para upload bem-sucedido
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Selecione um arquivo válido.");
            return View("Index");
        }
    }
}
