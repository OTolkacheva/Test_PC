using System;
using System.Collections.Generic;
using Test_PC.EF;
using Test_PC.Models;

namespace Test_PC.Db
{
    public interface IRepository : IDisposable
    {
        List<ServiceSetting> GetServiceSettings();

        string GetBaseAddressById(int Id);

        ServiceSetting GetServiceSettingById(int Id);

        void UpdateServiceSetting(ServiceSetting serviceSetting);

        void DeleteServiceSetting(int serviceSettingId);

        bool SaveRequst(string r);

        void SaveRequstData(Requests r, DateTime dt);

        void SaveUsers(List<User> us, string r);

        List<Requests> GetDoneRequst();

        List<Requests> GetDoneRequst(DateTime startdate, DateTime finaldate);

        List<Requests> GetNotDoneRequst();

        List<User> GetUserByRequest(int reqId);

        List<User> GetUserByRequest(int reqId, string filterName);

        List<User> GetAllUsers();

        List<User> GetAllUsers(string filterName);

        List<Requests> GetRequestByUser(int usId);

        List<Requests> GetRequestByUser(int usId, DateTime startdate, DateTime finaldate);

        void CreateService(string baseAddress);
    }
}
