using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SigoWeb.Infrastructure.Requests;
using SigoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SigoWeb.Controllers
{
    [Authorize]
    public class ConsultoriaController : Controller
    {
        private readonly IRefitConsultorias _refitConsultoria;
        private readonly IRefitNormas _refitNormas;
        private readonly string _token;

        public ConsultoriaController(IHttpContextAccessor httpContextAccessor, IRefitConsultorias refitConsultoria, IRefitNormas refitNormas)
        {
            _refitConsultoria = refitConsultoria;
            _refitNormas = refitNormas;
            _token = $"Bearer {httpContextAccessor.HttpContext.Request.Cookies["token"]}";
        }

        public async Task<IActionResult> Index()
        {
            IList<ConsultoriaModel> consultoriasModel = new List<ConsultoriaModel>();
            var consultoriaResult = await _refitConsultoria.ObterConsultoriasAsync(_token);
            if (consultoriaResult.IsSuccessStatusCode)
            {
                var response = await consultoriaResult.Content.ReadAsStringAsync();
                consultoriasModel = JsonConvert.DeserializeObject<IList<ConsultoriaModel>>(response);

                foreach(var consultoriaModel in consultoriasModel)
                {
                    var empresaResult = await _refitConsultoria.ObterEmpresaPorIdAsync(_token, consultoriaModel.EmpresaId.Value);
                    if (empresaResult.IsSuccessStatusCode)
                    {
                        var responseEmpresa = await empresaResult.Content.ReadAsStringAsync();
                        consultoriaModel.Empresa = JsonConvert.DeserializeObject<EmpresaModel>(responseEmpresa);
                    }

                    var normaResult = await _refitNormas.ObterNormaPorIdAsync(_token, consultoriaModel.NormaId.Value);
                    if (normaResult.IsSuccessStatusCode)
                    {
                        var responseNorma = await normaResult.Content.ReadAsStringAsync();
                        consultoriaModel.Norma = JsonConvert.DeserializeObject<NormaModel>(responseNorma);
                    }
                }
            }
            else if (consultoriaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(consultoriasModel.OrderBy(x => x.DataInicio));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            ConsultoriaModel consultoriaModel = new ConsultoriaModel();

            var consultoriaResult = await _refitConsultoria.ObterConsultoriaPorIdAsync(_token, id);
            if (consultoriaResult.IsSuccessStatusCode)
            {
                var responseConsultoria = await consultoriaResult.Content.ReadAsStringAsync();
                consultoriaModel = JsonConvert.DeserializeObject<ConsultoriaModel>(responseConsultoria);

                var empresaResult = await _refitConsultoria.ObterEmpresasAsync(_token);
                if (empresaResult.IsSuccessStatusCode)
                {
                    var responseEmpresa = await empresaResult.Content.ReadAsStringAsync();
                    var empresasModel = JsonConvert.DeserializeObject<IList<EmpresaModel>>(responseEmpresa);
                    ViewBag.EmpresaId = new SelectList(empresasModel, "Id", "RazaoSocial", consultoriaModel.EmpresaId);
                }

                var normaResult = await _refitNormas.ObterNormasAsync(_token);
                if (normaResult.IsSuccessStatusCode)
                {
                    var responseNorma = await normaResult.Content.ReadAsStringAsync();
                    var normasModel = JsonConvert.DeserializeObject<IList<NormaModel>>(responseNorma);
                    ViewBag.NormaId = new SelectList(normasModel, "Id", "Codigo", consultoriaModel.NormaId);
                }
            }
            else if (consultoriaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(consultoriaModel);
        }

        public async Task<IActionResult> Create()
        {
            var empresaResult = await _refitConsultoria.ObterEmpresasAsync(_token);
            if (empresaResult.IsSuccessStatusCode)
            {
                var responseEmpresa = await empresaResult.Content.ReadAsStringAsync();
                var empresasModel = JsonConvert.DeserializeObject<IList<EmpresaModel>>(responseEmpresa);
                ViewBag.EmpresaId = new SelectList(empresasModel, "Id", "RazaoSocial");
            }

            var normaResult = await _refitNormas.ObterNormasAsync(_token);
            if (normaResult.IsSuccessStatusCode)
            {
                var responseNorma = await normaResult.Content.ReadAsStringAsync();
                var normasModel = JsonConvert.DeserializeObject<IList<NormaModel>>(responseNorma);
                ViewBag.NormaId = new SelectList(normasModel, "Id", "Codigo");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultoriaModel consultoriaModel)
        {
            if (ModelState.IsValid)
            {
                consultoriaModel.Id = Guid.NewGuid();

                var consltoriaResult = await _refitConsultoria.InserirConsultoriaAsync(_token, consultoriaModel);
                if (consltoriaResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (consltoriaResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("SemPermissao", "Auth");
                }
            }

            var empresaResult = await _refitConsultoria.ObterEmpresasAsync(_token);
            if (empresaResult.IsSuccessStatusCode)
            {
                var responseEmpresa = await empresaResult.Content.ReadAsStringAsync();
                var empresasModel = JsonConvert.DeserializeObject<IList<EmpresaModel>>(responseEmpresa);
                ViewBag.EmpresaId = new SelectList(empresasModel, "Id", "RazaoSocial");
            }

            var normaResult = await _refitNormas.ObterNormasAsync(_token);
            if (normaResult.IsSuccessStatusCode)
            {
                var responseNorma = await normaResult.Content.ReadAsStringAsync();
                var normasModel = JsonConvert.DeserializeObject<IList<NormaModel>>(responseNorma);
                ViewBag.NormaId = new SelectList(normasModel, "Id", "Codigo");
            }

            return View(consultoriaModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            ConsultoriaModel consultoriaModel = new ConsultoriaModel();

            var consultoriaResult = await _refitConsultoria.ObterConsultoriaPorIdAsync(_token, id);
            if (consultoriaResult.IsSuccessStatusCode)
            {
                var responseConsultoria = await consultoriaResult.Content.ReadAsStringAsync();
                consultoriaModel = JsonConvert.DeserializeObject<ConsultoriaModel>(responseConsultoria);

                var empresaResult = await _refitConsultoria.ObterEmpresasAsync(_token);
                if (empresaResult.IsSuccessStatusCode)
                {
                    var responseEmpresa = await empresaResult.Content.ReadAsStringAsync();
                    var empresasModel = JsonConvert.DeserializeObject<IList<EmpresaModel>>(responseEmpresa);
                    ViewBag.EmpresaId = new SelectList(empresasModel, "Id", "RazaoSocial", consultoriaModel.EmpresaId);
                }

                var normaResult = await _refitNormas.ObterNormasAsync(_token);
                if (normaResult.IsSuccessStatusCode)
                {
                    var responseNorma = await normaResult.Content.ReadAsStringAsync();
                    var normasModel = JsonConvert.DeserializeObject<IList<NormaModel>>(responseNorma);
                    ViewBag.NormaId = new SelectList(normasModel, "Id", "Codigo", consultoriaModel.NormaId);
                }
            }
            else if (consultoriaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(consultoriaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConsultoriaModel consultoriaModel)
        {
            if (ModelState.IsValid)
            {
                var consltoriaResult = await _refitConsultoria.AtualizarConsultoriaAsync(_token, consultoriaModel);
                if (consltoriaResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (consltoriaResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("SemPermissao", "Auth");
                }
            }

            var empresaResult = await _refitConsultoria.ObterEmpresasAsync(_token);
            if (empresaResult.IsSuccessStatusCode)
            {
                var responseEmpresa = await empresaResult.Content.ReadAsStringAsync();
                var empresasModel = JsonConvert.DeserializeObject<IList<EmpresaModel>>(responseEmpresa);
                ViewBag.EmpresaId = new SelectList(empresasModel, "Id", "RazaoSocial");
            }

            var normaResult = await _refitNormas.ObterNormasAsync(_token);
            if (normaResult.IsSuccessStatusCode)
            {
                var responseNorma = await normaResult.Content.ReadAsStringAsync();
                var normasModel = JsonConvert.DeserializeObject<IList<NormaModel>>(responseNorma);
                ViewBag.NormaId = new SelectList(normasModel, "Id", "Codigo");
            }

            return View(consultoriaModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            ConsultoriaModel consultoriaModel = new ConsultoriaModel();

            var consultoriaResult = await _refitConsultoria.ObterConsultoriaPorIdAsync(_token, id);
            if (consultoriaResult.IsSuccessStatusCode)
            {
                var responseConsultoria = await consultoriaResult.Content.ReadAsStringAsync();
                consultoriaModel = JsonConvert.DeserializeObject<ConsultoriaModel>(responseConsultoria);

                var empresaResult = await _refitConsultoria.ObterEmpresasAsync(_token);
                if (empresaResult.IsSuccessStatusCode)
                {
                    var responseEmpresa = await empresaResult.Content.ReadAsStringAsync();
                    var empresasModel = JsonConvert.DeserializeObject<IList<EmpresaModel>>(responseEmpresa);
                    ViewBag.EmpresaId = new SelectList(empresasModel, "Id", "RazaoSocial", consultoriaModel.EmpresaId);
                }

                var normaResult = await _refitNormas.ObterNormasAsync(_token);
                if (normaResult.IsSuccessStatusCode)
                {
                    var responseNorma = await normaResult.Content.ReadAsStringAsync();
                    var normasModel = JsonConvert.DeserializeObject<IList<NormaModel>>(responseNorma);
                    ViewBag.NormaId = new SelectList(normasModel, "Id", "Codigo", consultoriaModel.NormaId);
                }
            }
            else if (consultoriaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(consultoriaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ConsultoriaModel consultoriaModel)
        {
            if (ModelState.IsValid)
            {
                var consltoriaResult = await _refitConsultoria.ExluirConsultoriaAsync(_token, consultoriaModel.Id.Value);
                if (consltoriaResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (consltoriaResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("SemPermissao", "Auth");
                }
            }

            var empresaResult = await _refitConsultoria.ObterEmpresasAsync(_token);
            if (empresaResult.IsSuccessStatusCode)
            {
                var responseEmpresa = await empresaResult.Content.ReadAsStringAsync();
                var empresasModel = JsonConvert.DeserializeObject<IList<EmpresaModel>>(responseEmpresa);
                ViewBag.EmpresaId = new SelectList(empresasModel, "Id", "RazaoSocial");
            }

            var normaResult = await _refitNormas.ObterNormasAsync(_token);
            if (normaResult.IsSuccessStatusCode)
            {
                var responseNorma = await normaResult.Content.ReadAsStringAsync();
                var normasModel = JsonConvert.DeserializeObject<IList<NormaModel>>(responseNorma);
                ViewBag.NormaId = new SelectList(normasModel, "Id", "Codigo");
            }

            return View(consultoriaModel);
        }
    }
}
