using Microsoft.AspNetCore.Mvc;
using Models.Repositorio;
using Models.Repositorio.Entidades;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controllers
{
    public class SorteioController : Controller
    {
        private readonly IParticipanteRepositorio _participanteRepositorio;
        private readonly ISorteioRepositorio _sorteioRepositorio;

        public SorteioController(IParticipanteRepositorio participanteRepositorio, ISorteioRepositorio sorteioRepositorio)
        {
            _participanteRepositorio = participanteRepositorio;
            _sorteioRepositorio = sorteioRepositorio;
        }

        // Lista todos os participantes para o sorteio
        public IActionResult Index()
        {
            var participantes = _participanteRepositorio.ListarTodos();
            var sorteadosIds = _sorteioRepositorio.ListarTodos().Select(s => s.IdParticipante).ToList();
            var participantesDisponiveis = participantes.Where(p => !sorteadosIds.Contains(p.Id)).ToList();

            return View(participantesDisponiveis.Select(p => new ParticipanteViewModel 
            {
                Id = p.Id,
                Nome = p.Nome
            }));
        }

        // Realiza o sorteio
        [HttpPost]
        public IActionResult Sortear()
        {
            var participantes = _participanteRepositorio.ListarTodos().ToList();
            if (participantes.Count == 0)
            {
                TempData["Mensagem"] = "Nenhum participante cadastrado para sortear.";
                return RedirectToAction("Index");
            }

            var sorteadosIds = _sorteioRepositorio.ListarTodos().Select(s => s.IdParticipante).ToList();
            var participantesDisponiveis = participantes.Where(p => !sorteadosIds.Contains(p.Id)).ToList();

            if (participantesDisponiveis.Count == 0)
            {
                TempData["Mensagem"] = "Todos os participantes já foram sorteados.";
                return RedirectToAction("Index");
            }

            var random = new Random();
            var sorteado = participantesDisponiveis[random.Next(participantesDisponiveis.Count)];

            // Adicionar o sorteio no banco de dados
            _sorteioRepositorio.Adicionar(new Sorteio { DataSorteio = DateTime.Now, IdParticipante = sorteado.Id });

            // Armazenar os dados do sorteio em ViewData
            ViewData["NomeParticipante"] = sorteado.Nome;
            ViewData["DataSorteio"] = DateTime.Now.ToString("g");
            ViewData["IdParticipante"] = sorteado.Id;

            return View("Index", participantesDisponiveis.Select(p => new ParticipanteViewModel 
            {
                Id = p.Id,
                Nome = p.Nome
            }));
        }

        // Ação para registrar presença
        [HttpPost]
        public IActionResult RegistrarPresenca(int idParticipante, bool presenca)
        {
            var participante = _participanteRepositorio.ObterPorId(idParticipante);

            if (participante != null)
            {
                participante.StatusPresenca = presenca;
                _participanteRepositorio.Atualizar(participante);
            }

            return RedirectToAction("Index");
        }
    }
}
