using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DRS.Data
{
    public interface IUserDA
    {
        Users GetUserForLogin(Login login);
        //GetPersonRelWithEmployee(int Person); 
        Users GetUserForLoginExternal(string email);
        List<Users> GetUsersAll();
        int EmployeeExist(string CBUSINESS_PARTNER_ID, string CEE_UUID, string TEE_ID);
        Empleados GetEmployee(string ClientCode);
        bool CreateEmployee(string CBUSINESS_PARTNER_ID, string CEE_UUID, string TEE_ID, string typeEMP);
        bool UpdateEmployee(string CBUSINESS_PARTNER_ID, string CEE_UUID, string TEE_ID, string typeEMP);
        int ClientExists(Int64 employeeId, string CROOT_UUID, string TEE_ID);
        Clientes GetCliente(string CROOT_UUID, string TEE_ID);
        List<Clientes> GetClienteAll();
        List<ClientAddress> GetClientAddressesAll();
        bool CreateCliente(string CROOT_UUID, string TEE_ID, int CLIFE_CYCLE_STATUS);
        int ExistsRelClientEmployee(Int64 ClientId, Int64 EmployeeId);
        bool CreateRelClientEmployee(Int64 ClientId, Int64 EmployeeId);
        bool UpdateClient(long idCliente, string tEE_ID);
        bool UpdateClientStatus(long idCliente, int CLIFE_CYCLE_STATUS);
        List<Clientes> GetClients(long personId);
        List<Clientes> GetClientsAll();
        List<Clientes> GetClientsByPersonId(long personId);
        int ProductExists(string productId, string productName, string typeTechnology = "");
        Productos GetProduct(string productId, string typeTechnology = "");
        bool UpdateProduct(long productoId, string productName, string productCode, string typeTechnology);
        bool CreateProducto(string productId, string productName, string typeTechnology);
        int ExistsRelClientProduct(long idCliente, int product, string serial = "", string imsi = "", string number = "", string sim = "");
        bool CreateRelClientProduct(long idCliente, int product, string serial = "", string imsi = "", string number = "", string sim = "");
        List<UsersViewModel> GetUsersForIndex();
        UserRole GetUsersForByPersonId(long personId);
        List<Empleados> GetEmployeeFromSAP();
        List<EmpleadosPersona> GetEmployeeByPersonId(long personId);
        Entities.Rest.ResultsAccesories AccessoryExists(string cMATR_UUID, string cSALESOMATIONC0CA0DF3B7723DDD);
        bool UpdateAccessory(string cMATR_UUID, string cSALESOMATIONC0CA0DF3B7723DDD, string cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377, string cs1ANsB5B1E594DE71377, string tMATR_UUID);
        List<TaxResidence> GetTaxResidenceTax();
        bool CreateAccessory(string cMATR_UUID, string cSALESOMATIONC0CA0DF3B7723DDD, string cSTATUSMATION08F89FA6D3D769C1Cs1ANsB5B1E594DE71377, string cs1ANsB5B1E594DE71377, string tMATR_UUID);
        List<StatusUser> GetStatusUser();
        bool UpdateRelClientProduct(List<ProductsClients> lstUpdtFinal);
        List<ProductsClients> GetProductsRegisteredForClientCode(string cROOT_UUID);
        int CreateProducts(List<ProductsClients> newProductsForClient);
        List<Location> GetLocation();
        bool DeleteRelClientProduct(List<ProductsClients> lstDelFinal);
        void AddAddress(List<ClientAddress> newclientsA);
        List<UserRole> GetUserRoles();
        List<ChannelDistribution> GetChannel();
        void ModifyAddress(List<ClientAddress> modifyclientsA);
        List<Empleados> GetEmpleadosByPersonId(long personId = 0);
        Response createRecoveryAccess(RecoveryRequest recoveryRequest);
        RecoveryRequest getRecoveryAccess(string key);
        Response ChangePassword(AccountRecovery accountRecoverycopy);
        bool passwordIsUsed(string password, long userId);
        Response Create(Entities.Person personCreate);
        List<PersonEmployee> GetEmployeesAll();
        List<UnitSales> GetUnitSales();
        List<DefaultServiceOrder> GetDefaultService();
        List<RAprofile> GetRaProfile();
        List<PersonEmployee> GetPersonEmployeesByPersonId(long personId);
        void Delete(Person person);
        Response Modify(Person person);
    }
}
