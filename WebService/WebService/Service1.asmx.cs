using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Data.Linq.Provider;
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
        //insert Place
        [WebMethod(Description = "Them Moi Place")]
        public bool insertPlace(byte[] image, Guid idCity)
        {
            bool kt = false;
            try
            {
                Place place = new Place();
                place.IdPlace = Guid.NewGuid();
                place.IdCity = idCity;
                place.ImagePlace = new Binary(image);
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
        public bool updatePlace(Guid idPlace, byte[] image, Guid idCity)
        {
            bool kt = false;
            try
            {
                var truyvan = from pl in db.Places
                              where pl.IdPlace == idPlace
                              select pl;
                foreach (Place p in truyvan)
                {
                    p.ImagePlace = new Binary(image);
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
    
        #region Table Location va Location_Lang
        //Thêm Location. Nếu thêm thành công thì sẽ trả về true, ngược lại trả về false
        //insert Location
        [WebMethod(Description = "Them Moi Location")]
        public bool insertLocation(Guid idStore)
        {
            bool kt = false;
            try
            {
                Location location = new Location();
                location.IdLocation = Guid.NewGuid();
                location.IdStore = idStore;
                db.Locations.InsertOnSubmit(location);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //update Location
        [WebMethod(Description = "Sua Location")]
        public bool updateLocation(Guid idLocation, Guid idStore)
        {
            bool kt = false;
            try
            {
                var truyvan = from lc in db.Locations
                              where lc.IdLocation == idLocation
                              select lc;
                foreach (Location l in truyvan)
                {
                    l.IdStore = idStore;
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

        //Xóa Location
        [WebMethod(Description = "Xoa Location")]
        public bool deleteLocation(Guid idLocation)
        {
            bool kt = false;
            try
            {
                var truyvan = from lc in db.Locations
                              where lc.IdLocation == idLocation
                              select lc;
                foreach (Location l in truyvan)
                {
                    db.Locations.DeleteOnSubmit(l);
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

        //insert Location_Lang
        [WebMethod(Description = "Them Moi Location_Lang")]
        public bool insertLocationLang(Guid idLocation, Guid idLang, string nameLocation)
        {
            bool kt = false;
            try
            {
                Location_Lang locationLang = new Location_Lang();
                locationLang.IdLocation = idLocation;
                locationLang.IdLanguage = idLang;
                locationLang.LocationName = nameLocation;
                db.Location_Langs.InsertOnSubmit(locationLang);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //delete Location_Lang
        [WebMethod(Description = "Xoa Location_Lang")]
        public bool deleteLocationLang(Guid idLocation, Guid idLang)
        {
            bool kt = false;
            try
            {
                var truyvan = from ll in db.Location_Langs
                              where ll.IdLanguage == idLang && ll.IdLocation == idLocation
                              select ll;
                foreach (Location_Lang lc in truyvan)
                {
                    db.Location_Langs.DeleteOnSubmit(lc);
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

        //update Location_Lang
        [WebMethod(Description = "Sua Location_Lang")]
        public bool updateLocationLang(Guid idLocation, Guid idLang, string nameLocation)
        {
            bool kt = false;
            try
            {
                var truyvan = from ll in db.Location_Langs
                              where ll.IdLanguage == idLang && ll.IdLocation == idLocation
                              select ll;
                foreach (Location_Lang lc in truyvan)
                {
                    lc.LocationName = nameLocation;
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
    
        #region Table Category va Category_Lang
        //Thêm Category. Nếu thêm thành công thì sẽ trả về true, ngược lại trả về false
        //insert Category
        [WebMethod(Description = "Them Moi Category")]
        public bool insertCategory()
        {
            bool kt = false;
            try
            {
                Category category = new Category();
                category.IdCategory = Guid.NewGuid();
                db.Categories.InsertOnSubmit(category);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //Xóa Category
        [WebMethod(Description = "Xoa Category")]
        public bool deleteCategory(Guid idCategory)
        {
            bool kt = false;
            try
            {
                var truyvan = from ct in db.Categories
                              where ct.IdCategory == idCategory
                              select ct;
                foreach (Category c in truyvan)
                {
                    db.Categories.DeleteOnSubmit(c);
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


        //insert Category_Lang
        [WebMethod(Description = "Them Moi Category_Lang")]
        public bool insertCategoryLang(Guid idCategory, Guid idLang, string nameCategory)
        {
            bool kt = false;
            try
            {
                Category_Lang categoryLang = new Category_Lang();
                categoryLang.IdCategory = idCategory;
                categoryLang.IdLanguage = idLang;
                categoryLang.CategoryName = nameCategory;
                db.Category_Langs.InsertOnSubmit(categoryLang);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //delete Category_Lang
        [WebMethod(Description = "Xoa Category_Lang")]
        public bool deleteCategoryLang(Guid idCategory, Guid idLang)
        {
            bool kt = false;
            try
            {
                var truyvan = from ctl in db.Category_Langs
                              where ctl.IdLanguage == idLang && ctl.IdCategory == idCategory
                              select ctl;
                foreach (Category_Lang ct in truyvan)
                {
                    db.Category_Langs.DeleteOnSubmit(ct);
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

        //update Category_Lang
        [WebMethod(Description = "Sua Category_Lang")]
        public bool updateCategoryLang(Guid idCategory, Guid idLang, string nameCategory)
        {
            bool kt = false;
            try
            {
                var truyvan = from ctl in db.Category_Langs
                              where ctl.IdLanguage == idLang && ctl.IdCategory == idCategory
                              select ctl;
                foreach (Category_Lang ct in truyvan)
                {
                    ct.CategoryName = nameCategory;
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

        #region Table Item, Item_Lang va Item_Category
        /*--- ITEM_CATEGORY ---*/
        //Insert ITEM_CATEGORY
        [WebMethod(Description = "Them Moi ITEM_CATEGORY")]
        public bool insertItemCategory(Guid idItem, Guid idCategory)
        {
            bool kt = false;
            try
            {
                Item_Category itemCategory = new Item_Category();
                itemCategory.IdItem = idItem;
                itemCategory.IdCategory = idCategory;
                db.Item_Categories.InsertOnSubmit(itemCategory);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }

        //Delete Item_Category
        [WebMethod(Description = "Xoa ITEM_CATEGORY")]
        public bool deleteItemCategory(Guid idItem, Guid idCategory)
        {
            bool kt = false;
            try
            {
                var truyvan = from itct in db.Item_Categories
                              where itct.IdItem == idItem && itct.IdCategory == idCategory
                              select itct;
                foreach (Item_Category ic in truyvan)
                {
                    db.Item_Categories.DeleteOnSubmit(ic);
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

        /*--- ITEM ---*/
        //Insert Item
        
        [WebMethod(Description = "Them Moi ITEM")]
        public bool insertItem(Guid idItem, byte[] imageItem ,Guid idLocation)
        {
            bool kt = false;
            try
            {
                Item item = new Item();
                item.IdItem = Guid.NewGuid();
                item.ImageItem = new Binary(imageItem);
                item.IdLocation = idLocation;
                db.Items.InsertOnSubmit(item);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }
        
        //Update Item
        [WebMethod(Description = "Sua ITEM")]
        public bool updateItem(Guid idItem, byte[] imageItem, Guid idLocation)
        {
            bool kt = false;
            try
            {
                var truyvan = from it in db.Items
                              where it.IdItem == idItem
                              select it;
                foreach (Item i in truyvan)
                {
                    Binary img = new Binary(imageItem);
                    i.ImageItem = img;
                    i.IdLocation = idLocation;
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
        
        //Delete Item
        [WebMethod(Description = "Xoa ITEM")]
        public bool deleteItem(Guid idItem)
        {
            bool kt = false;
            try
            {
                var truyvan = from it in db.Items
                              where it.IdItem == idItem
                              select it;
                foreach (Item i in truyvan)
                {
                    db.Items.DeleteOnSubmit(i);
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
        
        /*--- ITEM_LANG ---*/
        //Insert Item_Lang
        [WebMethod(Description = "Them Moi ITEM_LANG")]
        public bool insertItemLang(Guid idItem,Guid idLang, string nameItem, string information, string audio, string video)
        {
            bool kt = false;
            try
            {
                Item_Lang itemLang = new Item_Lang();
                itemLang.IdItem = idItem;
                itemLang.IdLanguage = idLang;
                itemLang.ItemName = nameItem;
                itemLang.Information = information;
                itemLang.Audio = audio;
                itemLang.Video = video;
                db.Item_Langs.InsertOnSubmit(itemLang);
                db.SubmitChanges();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            return kt;
        }
        //Update Item_Lang
        [WebMethod(Description = "Sua ITEM_LANG")]
        public bool updateItemLang(Guid idItem, Guid idLang, string nameItem, string information, string audio, string video)
        {
            bool kt = false;
            try
            {
                var truyvan = from itl in db.Item_Langs
                              where itl.IdItem == idItem && itl.IdLanguage == idLang
                              select itl;
                foreach (Item_Lang il in truyvan)
                {
                    il.ItemName = nameItem;
                    il.Information = information;
                    il.Audio = audio;
                    il.Video = video;
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
        //Delete Item_Lang
        [WebMethod(Description = "Xoa ITEM_LANG")]
        public bool deleteItemLang(Guid idItem, Guid idLang)
        {
            bool kt = false;
            try
            {
                var truyvan = from itl in db.Item_Langs
                              where itl.IdItem == idItem && itl.IdLanguage == idLang
                              select itl;
                foreach (Item_Lang il in truyvan)
                {
                    db.Item_Langs.DeleteOnSubmit(il);
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