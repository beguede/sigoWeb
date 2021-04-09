using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class NormaController : Controller
    {
        private readonly IRefitNormas _refitNorma;
        private readonly string _token;

        public NormaController(IHttpContextAccessor httpContextAccessor, IRefitNormas refitNorma)
        {
            _refitNorma = refitNorma;
            _token = $"Bearer {httpContextAccessor.HttpContext.Request.Cookies["token"]}";
        }

        public async Task<IActionResult> Index()
        {
            IList<NormaModel> normaModel = new List<NormaModel>();
            var normaResult = await _refitNorma.ObterNormasAsync(_token);
            if (normaResult.IsSuccessStatusCode)
            {
                var response = await normaResult.Content.ReadAsStringAsync();
                normaModel = JsonConvert.DeserializeObject<IList<NormaModel>>(response);
            }
            else if (normaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(normaModel.OrderBy(x => x.DataPublicacao));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            NormaModel normaModel = new NormaModel();

            var normaResult = await _refitNorma.ObterNormaPorIdAsync(_token, id);
            if (normaResult.IsSuccessStatusCode)
            {
                var response = await normaResult.Content.ReadAsStringAsync();
                normaModel = JsonConvert.DeserializeObject<NormaModel>(response);
            }
            else if (normaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(normaModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NormaModel normaModel)
        {
            if (ModelState.IsValid)
            {
                normaModel.Id = Guid.NewGuid();

                var normaResult = await _refitNorma.InserirNormaAsync(_token, normaModel);
                if (normaResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (normaResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("SemPermissao", "Auth");
                }
            }

            return View(normaModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            NormaModel normaModel = new NormaModel();

            var normaResult = await _refitNorma.ObterNormaPorIdAsync(_token, id);
            if (normaResult.IsSuccessStatusCode)
            {
                var response = await normaResult.Content.ReadAsStringAsync();
                normaModel = JsonConvert.DeserializeObject<NormaModel>(response);
            }
            else if (normaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(normaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NormaModel normaModel)
        {
            if (ModelState.IsValid)
            {
                var normaResult = await _refitNorma.AtualizarNormaAsync(_token, normaModel);
                if (normaResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (normaResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("SemPermissao", "Auth");
                }
            }

            return View(normaModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            NormaModel normaModel = new NormaModel();

            var normaResult = await _refitNorma.ObterNormaPorIdAsync(_token, id);
            if (normaResult.IsSuccessStatusCode)
            {
                var response = await normaResult.Content.ReadAsStringAsync();
                normaModel = JsonConvert.DeserializeObject<NormaModel>(response);
            }
            else if (normaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(normaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(NormaModel normaModel)
        {
            if (ModelState.IsValid)
            {
                var normaResult = await _refitNorma.ExluirNormaAsync(_token, normaModel.Id.Value);
                if (normaResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (normaResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("SemPermissao", "Auth");
                }
            }

            return View(normaModel);
        }
    }
}
