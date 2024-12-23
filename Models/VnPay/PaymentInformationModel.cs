namespace websiteTUTHIEN.Models.VnPay
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; } // values:other
        public double Amount { get; set; } // field nhap so tien
        public string OrderDescription { get; set; } // values:tu thien vao web tu thien
        public string Name { get; set; } // field nguoi dung tu nhap
    }
}