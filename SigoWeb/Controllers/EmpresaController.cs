﻿using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class EmpresaController : Controller
    {
        private readonly IRefitConsultorias _refitConsultoria;
        private readonly string _token;

        public EmpresaController(IHttpContextAccessor httpContextAccessor, IRefitConsultorias refitConsultoria)
        {
            _refitConsultoria = refitConsultoria;
            _token = $"Bearer {httpContextAccessor.HttpContext.Request.Cookies["token"]}";
        }

        public async Task<IActionResult> Index()
        {
            IList<EmpresaModel> empresaModel = new List<EmpresaModel>();
            var empresaResult = await _refitConsultoria.ObterEmpresasAsync(_token);
            if (empresaResult.IsSuccessStatusCode)
            {
                var response = await empresaResult.Content.ReadAsStringAsync();
                empresaModel = JsonConvert.DeserializeObject<IList<EmpresaModel>>(response);
            }
            else if (empresaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(empresaModel.OrderBy(x => x.NomeFantasia));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            EmpresaModel empresaModel = new EmpresaModel();

            var empresaResult = await _refitConsultoria.ObterEmpresaPorIdAsync(_token, id);
            if (empresaResult.IsSuccessStatusCode)
            {
                var response = await empresaResult.Content.ReadAsStringAsync();
                empresaModel = JsonConvert.DeserializeObject<EmpresaModel>(response);
            }
            else if (empresaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(empresaModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpresaModel empresaModel)
        {
            if (ModelState.IsValid)
            {
                empresaModel.Id = Guid.NewGuid();

                var empresaResult = await _refitConsultoria.InserirEmpresaAsync(_token, empresaModel);
                if (empresaResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (empresaResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("SemPermissao", "Auth");
                }
            }

            return View(empresaModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            EmpresaModel empresaModel = new EmpresaModel();

            var empresaResult = await _refitConsultoria.ObterEmpresaPorIdAsync(_token, id);
            if (empresaResult.IsSuccessStatusCode)
            {
                var response = await empresaResult.Content.ReadAsStringAsync();
                empresaModel = JsonConvert.DeserializeObject<EmpresaModel>(response);
            }
            else if (empresaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(empresaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmpresaModel empresaModel)
        {
            if (ModelState.IsValid)
            {
                var empresaResult = await _refitConsultoria.AtualizarEmpresaAsync(_token, empresaModel);
                if (empresaResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (empresaResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("SemPermissao", "Auth");
                }
            }

            return View(empresaModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            EmpresaModel empresaModel = new EmpresaModel();

            var empresaResult = await _refitConsultoria.ObterEmpresaPorIdAsync(_token, id);
            if (empresaResult.IsSuccessStatusCode)
            {
                var response = await empresaResult.Content.ReadAsStringAsync();
                empresaModel = JsonConvert.DeserializeObject<EmpresaModel>(response);
            }
            else if (empresaResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("SemPermissao", "Auth");
            }
            return View(empresaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmpresaModel empresaModel)
        {
            if (ModelState.IsValid)
            {
                var empresaResult = await _refitConsultoria.ExluirEmpresaAsync(_token, empresaModel.Id.Value);
                if (empresaResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (empresaResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("SemPermissao", "Auth");
                }
            }

            return View(empresaModel);
        }
    }
}
