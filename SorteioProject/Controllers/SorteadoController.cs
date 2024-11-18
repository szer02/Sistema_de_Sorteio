using Microsoft.AspNetCore.Mvc;
using Models.Repositorio;
using Models.ViewModel;

namespace Controllers
{
    public class SorteadoController : Controller
    {
        private readonly ISorteioRepositorio _sorteioRepositorio;
        private readonly IParticipanteRepositorio _participanteRepositorio;

        public SorteadoController(ISorteioRepositorio sorteioRepositorio, IParticipanteRepositorio participanteRepositorio)
        {
            _sorteioRepositorio = sorteioRepositorio;
            _participanteRepositorio = participanteRepositorio;
        }

        // Lista todos os participantes que já foram sorteados
        public IActionResult Index()
        {
            var sorteados = _sorteioRepositorio.ListarTodos();
            var viewModel = sorteados.Select(s => new SorteioViewModel
            {
                IdParticipante = s.IdParticipante,
                NomeParticipante = _participanteRepositorio.ObterPorId(s.IdParticipante).Nome,
                DataSorteio = s.DataSorteio,
                StatusPresenca = _participanteRepositorio.ObterPorId(s.IdParticipante).StatusPresenca
            });

            return View(viewModel);
        }
    }
}
