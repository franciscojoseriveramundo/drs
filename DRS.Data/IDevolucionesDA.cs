using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DRS.Data
{
    public interface IDevolucionesDA
    {
        List<MotivoEnvio> GetMotivoEnvio();
        List<Contratos> GetContratos(Int64 clientId = 0);
        List<Clientes> GetClientes(Int64 employeeId = 0);
        List<Clientes> GetClientesAll();
        List<DevolucionesViewModel> GetDevoluciones();
        List<Productos> GetProductsByClientId(long clientId);
        List<EstatusDetalles> GetStatusDetails();
        Devolucion Create(Devolucion devolucion, string userId);
        Devolucion Modify(Devolucion devolucion, string userId);
        bool CreateDetails(DetallesDevolucion item, long claveDevolucion);
        bool isSerialCompatibleWithClientId(long tipoProducto, long clientId, string Serial);
        List<ProductosSerial> GetProductsByClientIdAndProductId(long productId, long clientId);
        List<EstatusDevolucion> GetStatus();
        List<Accesorios> GetAccessories(string ce_ui);
        List<Accesorios> GetAccessoriesAll();
        List<Productos> GetProducts();
        List<Carrier> GetCarriers();
        List<PlanData> GetPlanDatas();
        Devolucion GetReturnById(long returnId);
        Response Cancel(Devolucion devolucion, long userId);
        bool AddComment(Comentarios comentarios, long returnId);
        StockConfirmation GetStockConfirmation(long idDetallesDevolucion);
        void modifyStatusReturn(Devolucion returnExistent);
        void CreateConfirmStock(StockConfirmationDetails details, long productId, bool existConfirm, int confirmExists);
        EstatusDetalles getStatusReturnConfirmation(int statusId);
        bool updateStatusConfirmation(List<DetallesDevolucion> dev);
        long createConfirmStock(List<StockConfirmDetails> stockConfirmDetails, long returnId = 0);
        StockConfirm GetStockConfirm(long stockConfirmId);
        List<StockConfirm> GetAllStockConfirm();
        void UpdateConfirmStock(StockConfirm stockConfirm);
        void DeleteConfirmStock(long stockConfirmId);
        bool ExistsProductInAnotherReturn(long tipoProducto, string serie, long idReturn);
        List<StockConfirm> GetStockConfirmByReturnId(long returnId);
        void UpdateConfirmation(List<StockConfirmDetails> details);
        List<StockConfirm> GetConfirmsSap();
    }
}
