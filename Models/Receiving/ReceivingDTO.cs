using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace CSMapi.Models
{
    public class ReceivingRequest
    {
        public string Documentno { get; set; }
        public int Productid { get; set; }
        public DateTime Expirationdate { get; set; }
        public string Cvnumber { get; set; }
        public string Platenumber { get; set; }
        public DateTime Arrivaldate { get; set; }
        public string Unloadingstart { get; set; }
        public string Unloadingend { get; set; }
        public double Overallweight { get; set; }
        public string DetailJson { get; set; }
        [NotMapped]
        public List<ReceivingDetailRequest> ReceivingDetail
        {
            get
            {
                return string.IsNullOrEmpty(DetailJson)
                    ? new List<ReceivingDetailRequest>()
                    : JsonSerializer.Deserialize<List<ReceivingDetailRequest>>(DetailJson);
            }
        }
    }
    public class ReceivingDisplayResponse
    {
        public int Id { get; set; }
        public string Documentno { get; set; } // Document
        public string Companyname { get; set; } // Product -> Customer
        public string Productname { get; set; } // Product
        public double Overallweight { get; set; }
        public DateTime Expirationdate { get; set; }
        public string Firstname { get; set; } // Requestor 
        public string Lastname { get; set; } // Requestor
        public DateTime Createdon { get; set; } 
    }
    public class ReceivingResponse
    {
        public int Id { get; set; }
        public DateTime Expirationdate { get; set; }
        public string Cvnumber { get; set; }
        public string Platenumber { get; set; }
        public DateTime Arrivaldate { get; set; }
        public string Unloadingstart { get; set; }
        public string Unloadingend { get; set; }
        public double Overallweight { get; set; }
        public string Receivingform { get; set; }
        public string Note { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime Updatedon { get; set; }
        public DateTime? Datereceived { get; set; }
        public bool Pending { get; set; }
        public bool Received { get; set; }
        public bool Declined { get; set; }
        public bool Removed { get; set; }
        public ProductResponse Product { get; set; }
        public DocumentResponse Document { get; set; }
        public UserEsignResponse Requestor { get; set; }
        public UserEsignResponse Approver { get; set; }
        public List<ReceivingDetailResponse> ReceivingDetail { get; set; }
    }
    public class ReceivingDetailRequest
    {
        public int Palletid { get; set; }
        public int Positionid { get; set; }
        public DateTime Productiondate { get; set; }
        public int Quantityinapallet { get; set; }
        public int Duquantity { get; set; }
        public double Duweight { get; set; }
        public double Totalweight { get; set; }
    }
    public class ReceivingDetailResponse
    {
        public int Id { get; set; }
        public int Receivingid { get; set; }
        public DateTime Productiondate { get; set; }
        public int Quantityinapallet { get; set; }
        public int Duquantity { get; set; }
        public double Duweight { get; set; }
        public double Totalweight { get; set; }
        public bool Received { get; set; }
        public bool Partialdispatched { get; set; }
        public bool Fulldispatched { get; set; }
        public PalletResponse Pallet { get; set; }
        public PalletPositionResponse Position { get; set; }
    }
    public class RepalletizeDetailResponse
    {
        public int Id { get; set; }
        public int Quantityinapallet { get; set; }
    }
    public class ProductReceivingDetailResponse
    {
        public int Id { get; set; }
        public DateTime Productiondate { get; set; }
        public int Quantityinapallet { get; set; }
        public int Duquantity { get; set; }
        public double Duweight { get; set; }
        public double Totalweight { get; set; }
        public PalletResponse Pallet { get; set; }
        public PalletPositionResponse Position { get; set; }
        public int Remainingquantity { get; set; }
        public double Remainingweight { get; set; }
    }
    public class ProductBasedReceivingDetailResponse
    {
        public int Id { get; set; }
        public DateTime Productiondate { get; set; }
        public int Quantityinapallet { get; set; }
        public int Duquantity { get; set; }
        public double Duweight { get; set; }
        public double Totalweight { get; set; }
        public PalletResponse Pallet { get; set; }
        public PalletPositionResponse Position { get; set; }
    }
    public class DocumentNumberResponse
    {
        public string Documentno { get; set; }
    }
    public class DocumentRequest
    {
        public string Documentno { get; set; }
    }
    public class DocumentResponse
    {
        public int Id { get; set; }
        public string Documentno { get; set; }
    }
    public class DocumentNoSeries
    {
        public string Documentno { get; set; }
    }
    public class ProductCompanyInventoryReceivingDetailResponse
    {
        public PalletResponse Pallet { get; set; }
    }
    public class ProductCompanyInventoryReceivingResponse
    {
        public int Id { get; set; }
        public DocumentResponse Document { get; set; }
        public UserEsignResponse Requestor { get; set; }    
        public UserEsignResponse Approver { get; set; }
        public DateTime? Datereceived { get; set; }
        public int Quantityinapallet { get; set; } // Receiving Detail
        public double Totalweight { get; set; }
        public List<ProductCompanyInventoryReceivingDetailResponse> ReceivingDetail { get; set; }
    }
    public class ProductReceivingResponse
    {
        public int Id { get; set; }
        public DateTime Expirationdate { get; set; }
        public string Cvnumber { get; set; }
        public string Platenumber { get; set; }
        public DateTime Arrivaldate { get; set; }
        public string Unloadingstart { get; set; }
        public string Unloadingend { get; set; }
        public double Overallweight { get; set; }
        public DateTime Productiondate { get; set; }
        public string Receivingform { get; set; }
        public string Note { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime? Updatedon { get; set; }
        public DateTime? Datereceived { get; set; }
        public bool Pending { get; set; }
        public bool Received { get; set; }
        public bool Declined { get; set; }
        public bool Removed { get; set; }
        public DocumentResponse Document { get; set; }
        public UserEsignResponse Requestor { get; set; }
        public UserEsignResponse Approver { get; set; }
        public List<ReceivingDetailResponse> ReceivingDetail { get; set; }
    }
    public class ReceivingsCount
    {
        public int Total { get; set; }
        public int Pending { get; set; }
        public int Received { get; set; }
        public int Declined { get; set; }
    }
    public class ProductBasesPallet
    {
        public int Id { get; set; }
        public int Palletid { get; set; }
        public string Taggingnumber { get; set; }
        public int? Palletno { get; set; }
        public int Quantityinapallet { get; set; }
        public int Remainingquantity { get; set; }
        public double Remainingweight { get; set; }
        public string Csnumber { get; set; } // Pallet Position -> Cold Storage
        public string Wing { get; set; } // Pallet Position
        public string Floor { get; set; } // Pallet Position
        public string? Column { get; set; } // Pallet Position
        public string? Side { get; set; } // Pallet Position
        public double Totalweight { get; set; }
    }
}
