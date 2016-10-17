using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    
    public class Service1 : System.Web.Services.WebService
    {
        QuanLyDuLichDataContext db;
        public Service1()
        {
            db = new QuanLyDuLichDataContext();
        }

        #region Table Account
        //Kiểm tra việc đăng nhập. Trả về 1 nếu đăng nhập thành công, trả về -1 nếu dữ liệu không đúng
        [WebMethod]
        public int checkLogin(string userName, string passWord)
        {
            int kt = -1;
            var truyvan = from tk in db.Accounts
                          where tk.Username == userName && tk.Password == passWord
                          select tk;
            if (truyvan.Count() > 0)
                kt = 1;
            return kt;
        }
        
        //Lấy danh sách account
        [WebMethod]
        public List<Account> getAccount()
        {
            List<Account> list = db.Accounts.ToList();
            return list;
        }

        //Thêm Account. Nếu thêm thành công thì sẽ trả về true, ngược lại trả về false
        [WebMethod]
        public bool insertAccount(string name, string username, string password, int role)
        {
            bool kt = false;
            try
            {
                Account account = new Account();
                account.FullName = name;
                account.Username = username;
                account.Password = password;
                account.Role = role;
                db.Accounts.InsertOnSubmit(account);
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //Sửa Account
        [WebMethod]
        public bool updateAccount(int id, string name, string username, string password, int role)
        {
            bool kt = false;
            try
            {
                var truyvan = from tk in db.Accounts
                          where tk.IdAccount == id
                          select tk;
                foreach(Account acc in truyvan){
                    acc.FullName = name;
                    acc.Password = password;
                    acc.Username = username;
                    acc.Role = role;
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //Xóa Account
        [WebMethod]
        public bool deleteAccount(int id)
        {
            bool kt = false;
            try
            {
                var truyvan = from tk in db.Accounts
                              where tk.IdAccount == id
                              select tk;
                foreach (Account acc in truyvan)
                {
                    db.Accounts.DeleteOnSubmit(acc);
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        #endregion

        #region Table Language
        //Lấy danh sách Language
        [WebMethod(Description = "Lay ra danh sach Language")]
        public List<Language> getLanguage()
        {
            List<Language> listLang = db.Languages.ToList();
            return listLang;
        }

        //Thêm Language. Nếu thêm thành công thì sẽ trả về true, ngược lại trả về false
        [WebMethod(Description = "Them Moi Language")]
        public bool insertLanguage(string langName)
        {
            bool kt = false;
            try
            {
                Language lang = new Language();
                lang.LanguageName = langName;
                lang.IdLanguage = Guid.NewGuid();
                db.Languages.InsertOnSubmit(lang);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //Sửa Language
        [WebMethod(Description = "Sua Language")]
        public bool updateLanguage(Guid id, string langName)
        {
            bool kt = false;
            try
            {
                var truyvan = from lng in db.Languages
                              where lng.IdLanguage == id
                              select lng;
                foreach (Language lg in truyvan)
                {
                    lg.LanguageName = langName;
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //Xóa Language
        [WebMethod(Description="Xoa Language")]
        public bool deleteLanguage(Guid id)
        {
            bool kt = false;
            try
            {
                var truyvan = from lng in db.Languages
                              where lng.IdLanguage == id
                              select lng;
                foreach (Language lg in truyvan)
                {
                    db.Languages.DeleteOnSubmit(lg);
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }
        #endregion 

        #region Table City va City_Lang
        ////Lấy danh sách City
        //[WebMethod(Description = "Lay ra danh sach City")]
        //public List<City> getCity()
        //{
        //    List<City> listCity = db.Cities.ToList();
        //    return listCity;
        //}

        //Thêm Language. Nếu thêm thành công thì sẽ trả về true, ngược lại trả về false
        //insert City
        [WebMethod(Description = "Them Moi City")]
        public bool insertCity()
        {
            bool kt = false;
            try
            {
                City city = new City();
                city.IdCity = Guid.NewGuid();
                db.Cities.InsertOnSubmit(city);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //Xóa City
        [WebMethod(Description = "Xoa City")]
        public bool deleteCity(Guid id)
        {
            bool kt = false;
            try
            {
                var truyvan = from ct in db.Cities
                              where ct.IdCity == id
                              select ct;
                foreach (City c in truyvan)
                {
                    db.Cities.DeleteOnSubmit(c);
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //insert City_Lang
        [WebMethod(Description = "Them Moi City_Lang")]
        public bool insertCityLang(Guid idCity, Guid idLang, string nameCity)
        {
            bool kt = false;
            try
            {
                City_Lang cityLang = new City_Lang();
                cityLang.IdCity = idCity;
                cityLang.IdLanguage = idLang;
                cityLang.CityName = nameCity;
                db.City_Langs.InsertOnSubmit(cityLang);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //delete City_Lang
        [WebMethod(Description = "Xoa City_Lang")]
        public bool deleteCityLang(Guid idCity, Guid idLang)
        {
            bool kt = false;
            try
            {
                var truyvan = from ct in db.City_Langs
                              where ct.IdCity == idCity && ct.IdLanguage == idLang
                              select ct;
                foreach (City_Lang c in truyvan)
                {
                    db.City_Langs.DeleteOnSubmit(c);
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }


        //update City_Lang
        [WebMethod(Description = "Sua City_Lang")]
        public bool updateCityLang(Guid idCity, Guid idLang, string nameCity)
        {
            bool kt = false;
            try
            {
                var truyvan = from ct in db.City_Langs
                              where ct.IdCity == idCity &&  ct.IdLanguage == idLang
                              select ct;
                foreach (City_Lang c in truyvan)
                {
                    c.CityName = nameCity;
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }
        #endregion 
    
        #region Table Place va Place_Lang
        //Thêm Place. Nếu thêm thành công thì sẽ trả về true, ngược lại trả về false
        //insert Place
        [WebMethod(Description = "Them Moi Place")]
        public bool insertPlace(System.Data.Linq.Binary image, Guid idCity)
        {
            bool kt = false;
            try
            {
                Place place = new Place();
                place.IdPlace = Guid.NewGuid();
                place.IdCity = idCity;
                place.ImagePlace = image;
                db.Places.InsertOnSubmit(place);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //update Place
        [WebMethod(Description = "Sua Place")]
        public bool updatePlace(Guid idPlace, System.Data.Linq.Binary image, Guid idCity)
        {
            bool kt = false;
            try
            {
                var truyvan = from pl in db.Places
                              where pl.IdPlace == idPlace
                              select pl;
                foreach (Place p in truyvan)
                {
                    p.ImagePlace = image;
                    p.IdCity = idCity;
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }
        
        //Xóa Place
        [WebMethod(Description = "Xoa Place")]
        public bool deletePlace(Guid idPlace)
        {
            bool kt = false;
            try
            {
                var truyvan = from pl in db.Places
                              where pl.IdPlace == idPlace
                              select pl;
                foreach (Place p in truyvan)
                {
                    db.Places.DeleteOnSubmit(p);
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //insert Place_Lang
        [WebMethod(Description = "Them Moi Place_Lang")]
        public bool insertPlaceLang(Guid idPlace, Guid idLang, string namePlace, string address, string information)
        {
            bool kt = false;
            try
            {
                Place_Lang placeLang = new Place_Lang();
                placeLang.IdPlace = idPlace;
                placeLang.IdLanguage = idLang;
                placeLang.PlaceName = namePlace;
                placeLang.Address = address;
                placeLang.Information = information;
                db.Place_Langs.InsertOnSubmit(placeLang);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //delete Place_Lang
        [WebMethod(Description = "Xoa Place_Lang")]
        public bool deletePlaceLang(Guid idPlace, Guid idLang)
        {
            bool kt = false;
            try
            {
                var truyvan = from pll in db.Place_Langs
                              where pll.IdLanguage == idLang && pll.IdPlace == idPlace
                              select pll;
                foreach (Place_Lang pl in truyvan)
                {
                    db.Place_Langs.DeleteOnSubmit(pl);
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //update Place_Lang
        [WebMethod(Description = "Sua Place_Lang")]
        public bool updatePlaceLang(Guid idPlace, Guid idLang, string namePlace, string address, string information)
        {
            bool kt = false;
            try
            {
                var truyvan = from pll in db.Place_Langs
                              where pll.IdLanguage == idLang && pll.IdPlace == idPlace
                              select pll;
                foreach (Place_Lang pl in truyvan)
                {
                    pl.PlaceName = namePlace;
                    pl.Address = address;
                    pl.Information = information;
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }
        #endregion 
    
        #region Table Store va Store_Lang
        //Thêm Place. Nếu thêm thành công thì sẽ trả về true, ngược lại trả về false
        //insert Store
        [WebMethod(Description = "Them Moi Store")]
        public bool insertStore(Guid idPlace)
        {
            bool kt = false;
            try
            {
                Store store = new Store();
                store.IdStore = Guid.NewGuid();
                store.IdPlace = idPlace;
                db.Stores.InsertOnSubmit(store);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //update Store
        [WebMethod(Description = "Sua Store")]
        public bool updateStore(Guid idStore, Guid idPlace)
        {
            bool kt = false;
            try
            {
                var truyvan = from st in db.Stores
                              where st.IdStore == idStore
                              select st;
                foreach (Store s in truyvan)
                {
                    s.IdPlace = idPlace;
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //Xóa Store
        [WebMethod(Description = "Xoa Store")]
        public bool deleteStore(Guid idStore)
        {
            bool kt = false;
            try
            {
                var truyvan = from st in db.Stores
                              where st.IdStore == idStore
                              select st;
                foreach (Store s in truyvan)
                {
                    db.Stores.DeleteOnSubmit(s);
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //insert Store_Lang
        [WebMethod(Description = "Them Moi Store_Lang")]
        public bool insertStoreLang(Guid idStore, Guid idLang, string nameStore)
        {
            bool kt = false;
            try
            {
                Store_Lang storeLang = new Store_Lang();
                storeLang.IdStore = idStore;
                storeLang.IdLanguage = idLang;
                storeLang.StoreName = nameStore;
                db.Store_Langs.InsertOnSubmit(storeLang);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //delete Store_Lang
        [WebMethod(Description = "Xoa Store_Lang")]
        public bool deleteStoreLang(Guid idStore, Guid idLang)
        {
            bool kt = false;
            try
            {
                var truyvan = from stl in db.Store_Langs
                              where stl.IdLanguage == idLang && stl.IdStore == idStore
                              select stl;
                foreach (Store_Lang sl in truyvan)
                {
                    db.Store_Langs.DeleteOnSubmit(sl);
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //update Place_Lang
        [WebMethod(Description = "Sua Store_Lang")]
        public bool updateStoreLang(Guid idStore, Guid idLang, string nameStore)
        {
            bool kt = false;
            try
            {
                var truyvan = from stl in db.Store_Langs
                              where stl.IdLanguage == idLang && stl.IdStore == idStore
                              select stl;
                foreach (Store_Lang sl in truyvan)
                {
                    sl.StoreName = nameStore;
                }
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }
        #endregion 
    }
}