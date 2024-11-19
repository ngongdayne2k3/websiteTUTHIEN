using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers
{
    public class DuAnController : Controller
    {
        WebsiteTuthienContext db = new WebsiteTuthienContext();
        public IActionResult Index()
        {
            return View();
        }

        // DanhMuc
        public IActionResult ListDanhMuc()
        {
            var listDM = db.TableDanhMucDuAns.ToList();
            return View(listDM);
        }

        public async Task<IActionResult> ThemDanhMuc()
        {
            var danhmucDA = new TableDanhMucDuAn();
            return View(danhmucDA);
        }

        [HttpPost]
        public async Task<IActionResult> ThemDanhMuc(TableDanhMucDuAn danhMucDuAn)
        {
            if (danhMucDuAn != null)
            {
                if (ModelState.IsValid)
                {
                    db.TableDanhMucDuAns.Add(danhMucDuAn);
                    db.SaveChanges();
                    return RedirectToAction("ListDanhMuc");
                }
            }
            return View(danhMucDuAn);
        }
        public IActionResult ChitietDanhMuc(int Id)
        {
            TableDanhMucDuAn danhmuc = db.TableDanhMucDuAns.SingleOrDefault(x => x.MaDanhMucDa == Id);
            return View(danhmuc);
        }

        public async Task<IActionResult> ChinhSuaDanhMuc(int id)
        {
            var danhmuc = db.TableDanhMucDuAns.Find(id);
            return View(danhmuc);
        }

        [HttpPost]
        public async Task<IActionResult> ChinhSuaDanhMuc(TableDanhMucDuAn danhmuc)
        {
            if(ModelState.IsValid)
            {
                db.TableDanhMucDuAns.Update(danhmuc);
                db.SaveChanges();
                return RedirectToAction("ListDanhMuc");
            }
            return View(danhmuc);
        }

        public IActionResult XoaDanhMuc(int id){
            TableDanhMucDuAn danhmuc = db.TableDanhMucDuAns.SingleOrDefault(x=>x.MaDanhMucDa==id);
            if(danhmuc == null){
                Response.StatusCode=404;
                return null;
            }
            return View(danhmuc);
        }

        [HttpPost, ActionName("XoaDanhMuc")]
        public IActionResult confirmXoaDanhMuc(int id){
            TableDanhMucDuAn danhmuc = db.TableDanhMucDuAns.SingleOrDefault(x => x.MaDanhMucDa == id);
            if(danhmuc == null){
                Response.StatusCode =  404;
                return null;
            }
            db.TableDanhMucDuAns.Remove(db.TableDanhMucDuAns.Find(id));
            db.SaveChanges();
            return RedirectToAction("ListDanhMuc");
        }

        // VungMien
        public IActionResult ListVungMien()
        {
            var list = db.TableVungMiens.ToList();
            return View(list);
        }

        public async Task<IActionResult> ThemVungMien()
        {
            var danhmucDA = new TableVungMien();
            return View(danhmucDA);
        }

        [HttpPost]
        public async Task<IActionResult> ThemVungMien(TableVungMien vm)
        {
            if (vm != null)
            {
                if (ModelState.IsValid)
                {
                    db.TableVungMiens.Add(vm);
                    db.SaveChanges();
                    return RedirectToAction("ListVungMien");
                }
            }
            return View(vm);
        }
        public IActionResult ChitietVungMien(int Id)
        {
            TableVungMien vm = db.TableVungMiens.SingleOrDefault(x => x.MaVungMien == Id);
            return View(vm);
        }

        public async Task<IActionResult> ChinhSuaVungMien(int id)
        {
            var vm = db.TableVungMiens.Find(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChinhSuaVungMien(TableVungMien vm)
        {
            if(ModelState.IsValid)
            {
                db.TableVungMiens.Update(vm);
                db.SaveChanges();
                return RedirectToAction("ListVungMien");
            }
            return View(vm);
        }

        public IActionResult XoaVungMien(int id){
            TableVungMien vungMien = db.TableVungMiens.SingleOrDefault(x=>x.MaVungMien==id);
            if(vungMien == null){
                Response.StatusCode=404;
                return null;
            }
            return View(vungMien);
        }

        [HttpPost, ActionName("XoaVungMien")]
        public IActionResult confirmXoaVungMien(int id){
            TableVungMien vungMien = db.TableVungMiens.SingleOrDefault(x=>x.MaVungMien==id);
            if(vungMien == null){
                Response.StatusCode =  404;
                return null;
            }
            db.TableVungMiens.Remove(db.TableVungMiens.Find(id));
            db.SaveChanges();
            return RedirectToAction("ListVungMien");
        }

        // TrangThai
        public IActionResult listTrangThai(){
            var list = db.TableTrangThais.ToList();
            return View(list);
        }
        public async Task<IActionResult> ThemTrangThai()
        {
            var trangthai = new TableTrangThai();
            return View(trangthai);
        }

        [HttpPost]
        public async Task<IActionResult> ThemTrangThai(TableTrangThai tt)
        {
            if (tt != null)
            {
                if (ModelState.IsValid)
                {
                    db.TableTrangThais.Add(tt);
                    db.SaveChanges();
                    return RedirectToAction("listTrangThai");
                }
            }
            return View(tt);
        }

        public async Task<IActionResult> ChinhSuaTrangThai(int id)
        {
            var tt = db.TableTrangThais.Find(id);
            return View(tt);
        }

        [HttpPost]
        public async Task<IActionResult> ChinhSuaTrangThai(TableTrangThai tt)
        {
            if(ModelState.IsValid)
            {
                db.TableTrangThais.Update(tt);
                db.SaveChanges();
                return RedirectToAction("listTrangThai");
            }
            return View(tt);
        }

        public IActionResult XoaTrangThai(int id){
            TableTrangThai tt = db.TableTrangThais.SingleOrDefault(x=>x.MaTrangThai==id);
            if(tt == null){
                Response.StatusCode=404;
                return null;
            }
            return View(tt);
        }

        [HttpPost, ActionName("XoaTrangThai")]
        public IActionResult confirmXoaTrangThai(int id){
            TableTrangThai tt = db.TableTrangThais.SingleOrDefault(x=>x.MaTrangThai==id);
            if(tt == null){
                Response.StatusCode =  404;
                return null;
            }
            db.TableTrangThais.Remove(db.TableTrangThais.Find(id));
            db.SaveChanges();
            return RedirectToAction("listTrangThai");
        }
        //Muc Do
        public IActionResult listMucDo(){
            var list = db.TableMucDoDuAns.ToList();
            return View(list);
        }
        public async Task<IActionResult> themMucDo()
        {
            var mucdo = new TableMucDoDuAn();
            return View(mucdo);
        }

        [HttpPost]
        public async Task<IActionResult> themMucDo(TableMucDoDuAn mucDoDuAn)
        {
            if (mucDoDuAn != null)
            {
                if (ModelState.IsValid)
                {
                    db.TableMucDoDuAns.Add(mucDoDuAn);
                    db.SaveChanges();
                    return RedirectToAction("listMucDo");
                }
            }
            return View(mucDoDuAn);
        }

        public async Task<IActionResult> chinhsuaMucDo(int id)
        {
            var tt = db.TableMucDoDuAns.Find(id);
            return View(tt);
        }

        [HttpPost]
        public async Task<IActionResult> chinhsuaMucDo(TableMucDoDuAn tt)
        {
            if(ModelState.IsValid)
            {
                db.TableMucDoDuAns.Update(tt);
                db.SaveChanges();
                return RedirectToAction("listMucDo");
            }
            return View(tt);
        }

        public IActionResult xoaMucDo(int id){
            TableMucDoDuAn mucDoDuAn = db.TableMucDoDuAns.SingleOrDefault(x=>x.MaMucDoDuAn==id);
            if(mucDoDuAn == null){
                Response.StatusCode=404;
                return null;
            }
            return View(mucDoDuAn);
        }

        [HttpPost, ActionName("xoaMucDo")]
        public IActionResult confirmxoaMucDo(int id){
            TableMucDoDuAn tt = db.TableMucDoDuAns.SingleOrDefault(x=>x.MaMucDoDuAn==id);
            if(tt == null){
                Response.StatusCode =  404;
                return null;
            }
            db.TableMucDoDuAns.Remove(db.TableMucDoDuAns.Find(id));
            db.SaveChanges();
            return RedirectToAction("listMucDo");
        }        
    }
}
