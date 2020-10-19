using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Test_PC.EF;
using Test_PC.Models;

namespace Test_PC.Db
{
    public class DbReposytory : IRepository
    {
        TestContext db;

        public DbReposytory()
        {
            this.db = new TestContext();
        }

        public List<ServiceSetting> GetServiceSettings()
        {
            return db.ServiceSettings.ToList();
        }

        public string GetBaseAddressById(int Id)
        {
            return db.ServiceSettings.Where(x => x.Id == Id).Select(x => x.BaseAddress).FirstOrDefault();
        }

        public ServiceSetting GetServiceSettingById(int Id)
        {
            return db.ServiceSettings.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void UpdateServiceSetting(ServiceSetting serviceSetting)
        {
            db.Entry(serviceSetting).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteServiceSetting(int serviceSettingId)
        {
            var serviceSetting = db.ServiceSettings.Where(x => x.Id == serviceSettingId).FirstOrDefault();
            db.ServiceSettings.Remove(serviceSetting);
            db.SaveChanges();
        }

        public bool SaveRequst(string r)
        {
            if (!db.Requests.Where(x => x.Request == r).Any())
            {
                var e = new Requests { Request = r };
                db.Requests.Add(e);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void SaveRequstData(Requests r, DateTime dt)
        {
            var request = r;
            request.executAt = dt;
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SaveUsers(List<User> us, string r)
        {
            var idreq = db.Requests.Where(x => x.Request == r).Select(x => x.Id).Single();
            foreach(var u in us)
            {
                var id = u.id;
                //проверяем существует ли уже такой пользователь
                if (!db.Users.Where(x => x.Id_User == u.id && x.Name == u.name && DbFunctions.DiffSeconds(x.createAt, u.createdAt) == 0).Any())
                {
                    var d = new Users
                    {
                        Id_User = u.id,
                        Name = u.name,
                        createAt = u.createdAt
                    };
                    db.Users.Add(d);
                }
                db.SaveChanges();
                var usId = db.Users.Where(x => x.Id_User == u.id && x.Name == u.name && DbFunctions.DiffSeconds(x.createAt, u.createdAt) == 0).Select(x => x.Id).FirstOrDefault();
                //проверяем существует ли такая связь запрос-пользователь
                if (!db.RequestToUser.Where(x => x.RequestId == idreq && x.UserId == usId).Any())
                {
                    var ur = new RequestToUser
                    {
                        RequestId = idreq,
                        UserId = usId
                    };
                    db.RequestToUser.Add(ur);
                }
                db.SaveChanges();
            }
        }

        public List<Requests> GetDoneRequst()
        {
            return db.Requests.Where(x => x.executAt != null).ToList();
        }

        public List<Requests> GetDoneRequst(DateTime startdate, DateTime finaldate)
        {
            finaldate = finaldate.AddDays(1);
            return db.Requests.Where(x => x.executAt != null && x.executAt >= startdate && x.executAt <= finaldate).ToList();
        }

        public List<Requests> GetNotDoneRequst()
        {
            return db.Requests.Where(x => x.executAt == null).ToList();
        }

        public List<User> GetUserByRequest(int reqId)
        {
            var LUs = db.RequestToUser.Where(x => x.RequestId == reqId).Select(x => new User { id = x.Users.Id, name = x.Users.Name, createdAt = x.Users.createAt}).ToList();
            return LUs;
        }

        public List<User> GetUserByRequest(int reqId, string filterName)
        {
            var LUs = db.RequestToUser.Where(x => x.RequestId == reqId && x.Users.Name.ToLower().Contains(filterName.ToLower())).Select(x => new User { id = x.Users.Id, name = x.Users.Name, createdAt = x.Users.createAt }).ToList();
            return LUs;
        }

        public List<User> GetAllUsers()
        {
            return db.Users.Select(x => new User { id = x.Id, name = x.Name, createdAt = x.createAt}).ToList();
        }

        public List<User> GetAllUsers(string filterName)
        {   
            return db.Users.Where(x => x.Name.ToLower().Contains(filterName.ToLower())).Select(x => new User { id = x.Id, name = x.Name, createdAt = x.createAt }).ToList();
        }

        public List<Requests> GetRequestByUser(int usId)
        {
            var LReq = db.RequestToUser.Where(x => x.UserId == usId).Select(x => x.Requests).ToList();
            return LReq;
        }

        public List<Requests> GetRequestByUser(int usId, DateTime startdate, DateTime finaldate)
        {
            finaldate = finaldate.AddDays(1);
            var LReq = db.RequestToUser.Where(x => x.UserId == usId && x.Requests.executAt >= startdate && x.Requests.executAt <= finaldate).Select(x => x.Requests).ToList();
            return LReq;
        }

        public void CreateService(string baseAddress)
        {
            var newService = new ServiceSetting
            {
                BaseAddress = baseAddress
            };
            db.ServiceSettings.Add(newService);
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}