﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaFotografa.Presentation.Controllers
{
    public class FotoProdutoController : Controller
    {
        // GET: FotoProduto
        public ActionResult Index()
        {
            return View();
        }

        // GET: FotoProduto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FotoProduto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FotoProduto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FotoProduto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FotoProduto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FotoProduto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FotoProduto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}