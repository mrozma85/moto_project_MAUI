namespace Moto_Phone.Models
{
    public class Ad
    {
        public int Id { get; set; }

        public int AdTypeId { get; set; }
        public AdType AdType { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public ICollection<Image> Image { get; set; } = new List<Image>();
        public ICollection<VehicleImages> ImageByte { get; set; } = new List<VehicleImages>();

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ApplicationUserId { get; set; }

        public string FullImageUrl => Image.FirstOrDefault().ImageUrl;
        public byte[] FullImageData => ImageByte.FirstOrDefault().FileData;
    }
}
